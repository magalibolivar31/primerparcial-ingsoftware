-- ============================================================
-- LA ALMONEDA NACIONAL — Script de creacion de base de datos
-- Ingeniera de Software UAI 2026
-- ============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'AlmonedaNacionalDB')
    DROP DATABASE AlmonedaNacionalDB;
GO

CREATE DATABASE AlmonedaNacionalDB;
GO

USE AlmonedaNacionalDB;
GO

-- ============================================================
-- TABLA: UnidadDeVenta (Patron Composite — Componente raiz)
-- Almacena tanto ArticulosIndividuales como Lotes.
-- La columna Tipo actua como discriminador de subtipo.
-- IdLotePadre implementa la relacion jerarquica (un padre por unidad).
-- ============================================================
CREATE TABLE UnidadDeVenta (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      VARCHAR(200)    NOT NULL,
    Descripcion TEXT            NULL,
    Tipo        VARCHAR(10)     NOT NULL CHECK (Tipo IN ('ARTICULO', 'LOTE')),
    PrecioBase  DECIMAL(12,2)   NOT NULL DEFAULT 0,
    FechaAlta   DATETIME        NOT NULL DEFAULT GETDATE(),
    Activo      BIT             NOT NULL DEFAULT 1,
    IdLotePadre INT             NULL REFERENCES UnidadDeVenta(Id)
);
GO

-- ============================================================
-- TABLA: ArticuloIndividual (Patron Composite — Hoja/Leaf)
-- Hereda el Id de UnidadDeVenta (relacion 1:1, clave primaria compartida).
-- ============================================================
CREATE TABLE ArticuloIndividual (
    Id             INT           PRIMARY KEY REFERENCES UnidadDeVenta(Id),
    ValorDeclarado DECIMAL(12,2) NOT NULL,
    Categoria      VARCHAR(100)  NULL,
    EstadoFisico   VARCHAR(50)   NULL,
    Ubicacion      VARCHAR(200)  NULL
);
GO

-- ============================================================
-- TABLA: Lote (Patron Composite — Nodo compuesto)
-- Los hijos del lote se gestionan mediante UnidadDeVenta.IdLotePadre.
-- ============================================================
CREATE TABLE Lote (
    Id                    INT  PRIMARY KEY REFERENCES UnidadDeVenta(Id),
    CantidadComponentes   INT  NOT NULL DEFAULT 0
);
GO

-- ============================================================
-- TABLA: Usuario (Personal interno del sistema)
-- ============================================================
CREATE TABLE Usuario (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    Nombre           VARCHAR(100)  NOT NULL,
    Apellido         VARCHAR(100)  NOT NULL,
    Email            VARCHAR(200)  NOT NULL UNIQUE,
    PasswordHash     VARCHAR(512)  NOT NULL,
    Rol              VARCHAR(20)   NOT NULL CHECK (Rol IN ('Administrador','Martillero','Operador','Supervisor')),
    Activo           BIT           NOT NULL DEFAULT 1,
    IntentosFallidos INT           NOT NULL DEFAULT 0,
    Bloqueado        BIT           NOT NULL DEFAULT 0,
    FechaAlta        DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLA: Postor (Interesado externo — Observer concreto a nivel de datos)
-- ============================================================
CREATE TABLE Postor (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Nombre    VARCHAR(200)  NOT NULL,
    DniCuit   VARCHAR(20)   NOT NULL UNIQUE,
    Email     VARCHAR(200)  NOT NULL,
    Telefono  VARCHAR(30)   NULL,
    Canal     VARCHAR(10)   NOT NULL DEFAULT 'SALA' CHECK (Canal IN ('WEB','MOVIL','SALA')),
    FechaAlta DATETIME      NOT NULL DEFAULT GETDATE(),
    Activo    BIT           NOT NULL DEFAULT 1
);
GO

-- ============================================================
-- TABLA: Subasta (Sujeto del Patron Observer a nivel de datos)
-- Registra el ciclo de vida de una subasta sobre una UnidadDeVenta.
-- ============================================================
CREATE TABLE Subasta (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdUnidad      INT             NOT NULL REFERENCES UnidadDeVenta(Id),
    IdMartillero  INT             NOT NULL REFERENCES Usuario(Id),
    Estado        VARCHAR(10)     NOT NULL DEFAULT 'ACTIVA' CHECK (Estado IN ('PENDIENTE','ACTIVA','CERRADA')),
    PrecioInicial DECIMAL(12,2)   NOT NULL,
    PrecioVigente DECIMAL(12,2)   NOT NULL,
    FechaApertura DATETIME        NOT NULL DEFAULT GETDATE(),
    FechaCierre   DATETIME        NULL,
    IdGanador     INT             NULL REFERENCES Postor(Id),
    PrecioFinal   DECIMAL(12,2)   NULL
);
GO

-- ============================================================
-- TABLA: Puja (Registro inmutable de cada oferta procesada)
-- ============================================================
CREATE TABLE Puja (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdSubasta     INT             NOT NULL REFERENCES Subasta(Id),
    IdPostor      INT             NOT NULL REFERENCES Postor(Id),
    Monto         DECIMAL(12,2)   NOT NULL,
    FechaHora     DATETIME        NOT NULL DEFAULT GETDATE(),
    Estado        VARCHAR(10)     NOT NULL CHECK (Estado IN ('ACEPTADA','RECHAZADA')),
    MotivoRechazo VARCHAR(200)    NULL
);
GO

-- ============================================================
-- TABLA: Suscripcion (Lista de observadores del Patron Observer en BD)
-- Relaciona Postor <-> Subasta para suscripciones activas.
-- ============================================================
CREATE TABLE Suscripcion (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdPostor         INT      NOT NULL REFERENCES Postor(Id),
    IdSubasta        INT      NOT NULL REFERENCES Subasta(Id),
    FechaSuscripcion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaBaja        DATETIME NULL,
    Activa           BIT      NOT NULL DEFAULT 1
);
GO

-- ============================================================
-- TABLA: Adjudicacion (Resultado definitivo de una subasta cerrada)
-- RF-13: base para el reporte consolidado de jornada.
-- ============================================================
CREATE TABLE Adjudicacion (
    Id                    INT IDENTITY(1,1) PRIMARY KEY,
    IdSubasta             INT             NOT NULL REFERENCES Subasta(Id),
    IdUnidad              INT             NOT NULL REFERENCES UnidadDeVenta(Id),
    IdGanador             INT             NOT NULL REFERENCES Postor(Id),
    PrecioFinal           DECIMAL(12,2)   NOT NULL,
    FechaHoraAdjudicacion DATETIME        NOT NULL DEFAULT GETDATE(),
    IdMartillero          INT             NOT NULL REFERENCES Usuario(Id),
    Observaciones         TEXT            NULL
);
GO

-- ============================================================
-- TABLA: Bitacora (Registro de auditoria del sistema)
-- ============================================================
CREATE TABLE Bitacora (
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    Formulario VARCHAR(200)  NULL,
    Accion     VARCHAR(500)  NOT NULL,
    Criticidad VARCHAR(10)   NOT NULL CHECK (Criticidad IN ('Baja','Media','Alta')),
    FechaHora  DATETIME      NOT NULL DEFAULT GETDATE(),
    IdUsuario  INT           NULL REFERENCES Usuario(Id)
);
GO

-- ============================================================
-- INDICES para mejorar el rendimiento en consultas frecuentes
-- ============================================================
CREATE INDEX IX_Subasta_IdUnidad  ON Subasta(IdUnidad);
CREATE INDEX IX_Subasta_Estado    ON Subasta(Estado);
CREATE INDEX IX_Puja_IdSubasta    ON Puja(IdSubasta);
CREATE INDEX IX_Suscripcion_Clave ON Suscripcion(IdPostor, IdSubasta, Activa);
CREATE INDEX IX_UnidadDeVenta_Padre ON UnidadDeVenta(IdLotePadre);
GO

-- ============================================================
-- DATOS INICIALES — Usuarios del sistema
-- Password de todos: admin123 (hash SHA-256)
-- ============================================================
INSERT INTO Usuario (Nombre, Apellido, Email, PasswordHash, Rol, Activo, FechaAlta) VALUES
('Admin',    'Sistema',  'admin',       '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Administrador', 1, '2026-01-10 08:00:00'),
('Carlos',   'Mendez',   'martillero',  '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Martillero',    1, '2026-01-10 08:00:00'),
('Laura',    'Torres',   'operador',    '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Operador',      1, '2026-01-10 08:00:00'),
('Roberto',  'Silva',    'supervisor',  '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Supervisor',    1, '2026-01-10 08:00:00');
GO

-- ============================================================
-- POSTORES — Compradores registrados con distintos canales
-- ============================================================
INSERT INTO Postor (Nombre, DniCuit, Email, Telefono, Canal, FechaAlta) VALUES
('Juan Perez',          '28456789',      'juan.perez@mail.com',      '011-4523-7890', 'SALA',  '2026-01-15 09:00:00'),
('Maria Garcia',        '32100456',      'maria.garcia@mail.com',    '011-4789-1234', 'WEB',   '2026-01-16 10:30:00'),
('Tech Solutions S.A.', '30-71234567-9', 'compras@techsol.com.ar',   '011-5522-8800', 'MOVIL', '2026-01-18 11:00:00'),
('Ana Kovalenko',       '35678900',      'ana.kovalenko@gmail.com',  '221-4500-2211', 'WEB',   '2026-02-01 14:00:00'),
('Ricardo Fontana',     '20987654',      'rfontana@outlook.com',     '261-4800-3345', 'SALA',  '2026-02-03 09:15:00'),
('Coleccionistas Arg.', '30-68901234-5', 'info@colecarg.com.ar',     '011-4312-9900', 'WEB',   '2026-02-10 16:00:00'),
('Valentina Ruiz',      '38120045',      'vruiz_arte@hotmail.com',   '351-5600-7788', 'MOVIL', '2026-03-05 10:00:00'),
('Horacio Blanco',      '25334411',      'hblanco.inversiones@gmail.com', '011-4966-5544', 'SALA', '2026-03-12 08:30:00');
GO

-- ============================================================
-- CATALOGO — Patron Composite en accion
--
-- Jerarquia resultante:
--
--  [LOTE]  Lote Bellas Artes — Siglo XIX (Id=3)
--    ├── [ART] Oleo sobre tela "Atardecer en el Rio" (Id=1)
--    └── [ART] Acuarela "Vista del Puerto de Buenos Aires" (Id=2)
--
--  [LOTE]  Lote Antiguedades Europeas (Id=7)
--    ├── [ART] Reloj de pie aleman — c.1890 (Id=4)
--    ├── [ART] Candelabros de plata maciza x6 (Id=5)
--    └── [LOTE] Porcelanas Meissen (Id=6)  <-- Lote anidado
--          ├── [ART] Juego de te Meissen — 12 piezas (Id dentro del lote anidado)
--          └── [ART] Florero Meissen — motivo floral
--
--  [ART]  Collar de brillantes Art Deco (suelto, sin lote)
--  [ART]  Anillo solitario — diamante 2.1ct (suelto)
--  [ART]  Escritorio victorianaoo en roble macizo (suelto)
--  [ART]  Torno CNC Modelo X200 (suelto, maquinaria)
-- ============================================================

-- ---- Articulos sueltos que luego van al Lote Bellas Artes ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Oleo sobre tela "Atardecer en el Rio"',
        'Autor: Ramon Gomez Cornet, 1923. Oleo sobre tela, 80x60 cm. Marco original dorado. Certificado de autenticidad incluido.',
        'ARTICULO', 180000.00, '2026-03-01 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 180000.00, 'Pintura', 'Muy bueno', 'Sala Principal — Vitrina 1');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Acuarela "Vista del Puerto de Buenos Aires"',
        'Autor: Prilidiano Pueyrredon (atribuida), c.1860. Acuarela sobre papel, 45x30 cm. Enmarcada.',
        'ARTICULO', 95000.00, '2026-03-01 09:10:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 95000.00, 'Pintura', 'Bueno', 'Sala Principal — Vitrina 1');

-- ---- Lote Bellas Artes (padre de los dos anteriores) ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Bellas Artes — Siglo XIX',
        'Lote compuesto por dos obras de arte argentinas del siglo XIX. Precio base = suma de componentes.',
        'LOTE', 275000.00, '2026-03-01 09:20:00', 1);
DECLARE @idLoteBellasArtes INT = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteBellasArtes, 2);
-- Asignar hijos al lote Bellas Artes (los articulos de Id 1 y 2)
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteBellasArtes WHERE Id IN (1, 2);
GO

-- ---- Articulos para Lote Antiguedades Europeas ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Reloj de pie aleman — c.1890',
        'Fabricante: Junghans, Alemania, circa 1890. Caja en roble tallado, altura 195 cm. Mecanismo original en funcionamiento.',
        'ARTICULO', 320000.00, '2026-03-05 10:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 320000.00, 'Antiguedad', 'Excelente', 'Deposito B — Seccion Relojes');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Candelabros de plata maciza x6',
        'Juego de seis candelabros, plata 925, punzonados Londres 1875. Altura 42 cm c/u. Peso total 4.8 kg.',
        'ARTICULO', 210000.00, '2026-03-05 10:15:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 210000.00, 'Plata', 'Muy bueno', 'Deposito B — Caja Fuerte 2');

-- ---- Lote anidado: Porcelanas Meissen (va DENTRO de Antiguedades Europeas) ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Porcelanas Meissen',
        'Dos piezas de porcelana Meissen, Alemania, siglo XIX. Marca de espada cruzada verificada.',
        'LOTE', 155000.00, '2026-03-05 10:30:00', 1);
DECLARE @idLoteMeissen INT = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteMeissen, 2);

-- Articulos que van DENTRO del Lote Meissen
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo, IdLotePadre)
VALUES ('Juego de te Meissen — 12 piezas',
        'Tetera, azucarera, lechera y 9 tazas con platos. Decoracion floral policromada, c.1870.',
        'ARTICULO', 90000.00, '2026-03-05 10:35:00', 1, @idLoteMeissen);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 90000.00, 'Porcelana', 'Bueno', 'Deposito B — Vitrina 3');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo, IdLotePadre)
VALUES ('Florero Meissen — motivo floral',
        'Florero de porcelana Meissen, altura 35 cm, decoracion de rosas y pajaros, c.1860.',
        'ARTICULO', 65000.00, '2026-03-05 10:40:00', 1, @idLoteMeissen);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 65000.00, 'Porcelana', 'Excelente', 'Deposito B — Vitrina 3');

-- ---- Lote Antiguedades Europeas (padre del Reloj, Candelabros y Lote Meissen) ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Antiguedades Europeas',
        'Lote premium: reloj de pie Junghans + candelabros de plata + porcelanas Meissen. Precio base = suma de todos los componentes.',
        'LOTE', 685000.00, '2026-03-05 11:00:00', 1);
DECLARE @idLoteAntiguedades INT = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteAntiguedades, 3);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteAntiguedades WHERE Id IN (4, 5);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteAntiguedades WHERE Id = @idLoteMeissen;
GO

-- ---- Articulos sueltos (sin lote padre) ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Collar de brillantes Art Deco',
        'Collar de oro blanco 18k con 47 brillantes talla baguette. Peso total: 2.3 ct. Certificado GIA incluido. Caja original.',
        'ARTICULO', 850000.00, '2026-03-10 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 850000.00, 'Joyeria', 'Excelente', 'Deposito A — Caja Fuerte 1');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Anillo solitario — diamante 2.1ct',
        'Oro amarillo 18k, diamante central 2.1 ct, talla brillante, color G, claridad VS1. Certificado IGI. Aro con pavé lateral.',
        'ARTICULO', 1200000.00, '2026-03-10 09:30:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 1200000.00, 'Joyeria', 'Excelente', 'Deposito A — Caja Fuerte 1');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Escritorio victoriano en roble macizo',
        'Escritorio ingles estilo victoriano, roble macizo, c.1880. Tapete de cuero verde. 9 cajones con tiradores de bronce. 155x80x78 cm.',
        'ARTICULO', 145000.00, '2026-03-15 10:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 145000.00, 'Muebles', 'Muy bueno', 'Salon de Exhibicion — Piso 2');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Torno CNC Modelo X200',
        'Torno de control numerico marca Mazak, modelo X200, año 2019. Buen estado de funcionamiento. Incluye juego de herramientas.',
        'ARTICULO', 45000.00, '2026-03-18 08:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 45000.00, 'Maquinaria', 'Bueno', 'Deposito C — Sector Industrial');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Coleccion de vinos — Reserva Especial',
        'Caja de 12 botellas: 6x Catena Zapata Adrianna 2018 + 3x Achaval Ferrer Quimera 2019 + 3x Clos de los Siete 2020.',
        'ARTICULO', 38000.00, '2026-03-20 11:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 38000.00, 'Vinos', 'Excelente', 'Deposito A — Bodega Climatizada');
GO

-- ============================================================
-- SUBASTAS
-- IdMartillero = 2 (Carlos Mendez)
--
-- Subastas CERRADAS (ya adjudicadas):
--   Sub 1: Collar de brillantes (Id unidad = 9)   → ganador: Coleccionistas Arg. (postor 6)
--   Sub 2: Lote Bellas Artes (Id unidad = 3)      → ganador: Ana Kovalenko (postor 4)
--   Sub 3: Reloj de pie aleman (Id unidad = 4)    → ganador: Horacio Blanco (postor 8)
--
-- Subastas ACTIVAS (en curso):
--   Sub 4: Anillo solitario (Id unidad = 10)      → en disputa
--   Sub 5: Lote Antiguedades Europeas (Id = 8)    → recien abierta
-- ============================================================

-- Subasta 1 — Collar de brillantes (CERRADA)
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (9, 2, 'CERRADA', 850000.00, 1050000.00, '2026-04-10 10:00:00', '2026-04-10 11:45:00', 6, 1050000.00);

-- Subasta 2 — Lote Bellas Artes (CERRADA)
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (3, 2, 'CERRADA', 275000.00, 340000.00, '2026-04-15 14:00:00', '2026-04-15 15:30:00', 4, 340000.00);

-- Subasta 3 — Reloj de pie aleman (CERRADA)
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (4, 2, 'CERRADA', 320000.00, 415000.00, '2026-04-22 10:00:00', '2026-04-22 12:00:00', 8, 415000.00);

-- Subasta 4 — Anillo solitario (ACTIVA — en disputa)
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (10, 2, 'ACTIVA', 1200000.00, 1380000.00, '2026-05-16 09:00:00');

-- Subasta 5 — Lote Antiguedades Europeas (ACTIVA — recien abierta)
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (8, 2, 'ACTIVA', 685000.00, 685000.00, '2026-05-16 10:30:00');
GO

-- ============================================================
-- PUJAS — Historico completo
-- Muestra: pujas aceptadas encadenadas + pujas rechazadas (monto bajo)
-- ============================================================

-- === Subasta 1: Collar de brillantes ===
-- Ronda 1
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (1, 1, 870000.00, '2026-04-10 10:08:00', 'ACEPTADA');   -- Juan Perez abre
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (1, 6, 900000.00, '2026-04-10 10:15:00', 'ACEPTADA');   -- Coleccionistas sube
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado, MotivoRechazo)
VALUES (1, 3, 880000.00, '2026-04-10 10:16:00', 'RECHAZADA', 'Monto inferior al precio vigente'); -- Tech Solutions rechazada
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (1, 5, 950000.00, '2026-04-10 10:22:00', 'ACEPTADA');   -- Ricardo Fontana
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (1, 6, 1000000.00, '2026-04-10 10:35:00', 'ACEPTADA');  -- Coleccionistas retoma
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado, MotivoRechazo)
VALUES (1, 5, 990000.00, '2026-04-10 10:36:00', 'RECHAZADA', 'Monto inferior al precio vigente'); -- Ricardo rechazado
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (1, 6, 1050000.00, '2026-04-10 11:44:00', 'ACEPTADA');  -- Coleccionistas gana
GO

-- === Subasta 2: Lote Bellas Artes ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (2, 7, 285000.00, '2026-04-15 14:07:00', 'ACEPTADA');   -- Valentina Ruiz abre
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (2, 4, 300000.00, '2026-04-15 14:15:00', 'ACEPTADA');   -- Ana Kovalenko
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado, MotivoRechazo)
VALUES (2, 7, 295000.00, '2026-04-15 14:16:00', 'RECHAZADA', 'Monto inferior al precio vigente');
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (2, 2, 315000.00, '2026-04-15 14:28:00', 'ACEPTADA');   -- Maria Garcia
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (2, 4, 340000.00, '2026-04-15 15:10:00', 'ACEPTADA');   -- Ana Kovalenko gana
GO

-- === Subasta 3: Reloj de pie aleman ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (3, 8, 335000.00, '2026-04-22 10:10:00', 'ACEPTADA');   -- Horacio Blanco
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (3, 1, 355000.00, '2026-04-22 10:22:00', 'ACEPTADA');   -- Juan Perez
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado, MotivoRechazo)
VALUES (3, 3, 340000.00, '2026-04-22 10:23:00', 'RECHAZADA', 'Monto inferior al precio vigente');
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (3, 8, 380000.00, '2026-04-22 10:45:00', 'ACEPTADA');   -- Horacio sube
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado, MotivoRechazo)
VALUES (3, 1, 370000.00, '2026-04-22 10:46:00', 'RECHAZADA', 'Monto inferior al precio vigente');
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (3, 8, 415000.00, '2026-04-22 11:58:00', 'ACEPTADA');   -- Horacio Blanco gana
GO

-- === Subasta 4: Anillo solitario (ACTIVA) ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (4, 2, 1230000.00, '2026-05-16 09:08:00', 'ACEPTADA');  -- Maria Garcia abre
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (4, 6, 1280000.00, '2026-05-16 09:20:00', 'ACEPTADA');  -- Coleccionistas
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado, MotivoRechazo)
VALUES (4, 2, 1270000.00, '2026-05-16 09:21:00', 'RECHAZADA', 'Monto inferior al precio vigente');
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (4, 7, 1350000.00, '2026-05-16 09:35:00', 'ACEPTADA');  -- Valentina Ruiz toma la delantera
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado)
VALUES (4, 6, 1380000.00, '2026-05-16 09:50:00', 'ACEPTADA');  -- Coleccionistas recupera liderazgo
GO

-- === Subasta 5: Lote Antiguedades Europeas (ACTIVA — recien iniciada, sin pujas aun) ===
-- (sin pujas por el momento — se muestra la subasta recien abierta)
GO

-- ============================================================
-- ADJUDICACIONES — Resultado de las subastas cerradas
-- ============================================================
INSERT INTO Adjudicacion (IdSubasta, IdUnidad, IdGanador, PrecioFinal, FechaHoraAdjudicacion, IdMartillero, Observaciones) VALUES
(1, 9,  6, 1050000.00, '2026-04-10 11:46:00', 2, 'Collar adjudicado a Coleccionistas Argentinos. Pago contra entrega.'),
(2, 3,  4,  340000.00, '2026-04-15 15:32:00', 2, 'Lote Bellas Artes adjudicado a Ana Kovalenko. Retiro coordinado.'),
(3, 4,  8,  415000.00, '2026-04-22 12:02:00', 2, 'Reloj adjudicado a Horacio Blanco. Traslado a cargo del comprador.');
GO

-- ============================================================
-- SUSCRIPCIONES — Observadores registrados (Patron Observer)
-- Subastas activas: 4 (Anillo) y 5 (Lote Antiguedades)
-- ============================================================
INSERT INTO Suscripcion (IdPostor, IdSubasta, FechaSuscripcion, Activa) VALUES
-- Suscriptos a Subasta 4 (Anillo solitario)
(2, 4, '2026-05-16 08:55:00', 1),   -- Maria Garcia
(6, 4, '2026-05-16 09:00:00', 1),   -- Coleccionistas Arg.
(7, 4, '2026-05-16 09:30:00', 1),   -- Valentina Ruiz
(1, 4, '2026-05-16 09:05:00', 1),   -- Juan Perez (observa sin pujar aun)
-- Suscriptos a Subasta 5 (Lote Antiguedades)
(8, 5, '2026-05-16 10:35:00', 1),   -- Horacio Blanco
(3, 5, '2026-05-16 10:40:00', 1),   -- Tech Solutions
(4, 5, '2026-05-16 10:45:00', 1);   -- Ana Kovalenko
GO

-- ============================================================
-- BITACORA — Registro de auditoria de ejemplo
-- ============================================================
INSERT INTO Bitacora (Formulario, Accion, Criticidad, FechaHora, IdUsuario) VALUES
('Login',           'Inicio de sesion: admin',                                      'Baja',  '2026-04-10 09:50:00', 1),
('Subastas',        'Subasta abierta — Collar de brillantes (Sub ID 1)',             'Alta',  '2026-04-10 10:00:00', 2),
('Subastas',        'Subasta cerrada — Collar adjudicado a Coleccionistas (Sub ID 1)','Alta', '2026-04-10 11:46:00', 2),
('Subastas',        'Subasta abierta — Lote Bellas Artes (Sub ID 2)',                'Alta',  '2026-04-15 14:00:00', 2),
('Subastas',        'Subasta cerrada — Lote Bellas Artes adjudicado (Sub ID 2)',     'Alta',  '2026-04-15 15:32:00', 2),
('Subastas',        'Subasta abierta — Reloj de pie Junghans (Sub ID 3)',            'Alta',  '2026-04-22 10:00:00', 2),
('Subastas',        'Subasta cerrada — Reloj adjudicado a H. Blanco (Sub ID 3)',     'Alta',  '2026-04-22 12:02:00', 2),
('Subastas',        'Subasta abierta — Anillo solitario 2.1ct (Sub ID 4)',           'Alta',  '2026-05-16 09:00:00', 2),
('Subastas',        'Subasta abierta — Lote Antiguedades Europeas (Sub ID 5)',       'Alta',  '2026-05-16 10:30:00', 2),
('GestionUsuarios', 'Alta de usuario: operador',                                     'Media', '2026-01-10 08:05:00', 1),
('GestionUsuarios', 'Alta de usuario: martillero',                                   'Media', '2026-01-10 08:05:00', 1);
GO

PRINT '============================================================';
PRINT 'Base de datos AlmonedaNacionalDB creada correctamente.';
PRINT '';
PRINT 'Usuarios: admin / martillero / operador / supervisor (pass: admin123)';
PRINT 'Articulos: 10 | Lotes: 3 (con jerarquia anidada)';
PRINT 'Postores: 8  | Subastas: 5 (3 cerradas + 2 activas)';
PRINT 'Pujas: 21    | Adjudicaciones: 3 | Suscripciones: 7';
PRINT '============================================================';
GO

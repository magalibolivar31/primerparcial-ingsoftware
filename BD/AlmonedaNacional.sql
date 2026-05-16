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
-- ============================================================
CREATE TABLE ArticuloIndividual (
    Id             INT           PRIMARY KEY REFERENCES UnidadDeVenta(Id),
    ValorDeclarado DECIMAL(12,2) NOT NULL
);
GO

-- ============================================================
-- TABLA: Lote (Patron Composite — Nodo compuesto)
-- ============================================================
CREATE TABLE Lote (
    Id                    INT  PRIMARY KEY REFERENCES UnidadDeVenta(Id),
    CantidadComponentes   INT  NOT NULL DEFAULT 0
);
GO

-- ============================================================
-- TABLA: Usuario
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
-- TABLA: Postor
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
-- TABLA: Subasta
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
-- TABLA: Puja
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
-- TABLA: Suscripcion
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
-- TABLA: Adjudicacion
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
-- TABLA: Bitacora
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
-- INDICES
-- ============================================================
CREATE INDEX IX_Subasta_IdUnidad    ON Subasta(IdUnidad);
CREATE INDEX IX_Subasta_Estado      ON Subasta(Estado);
CREATE INDEX IX_Puja_IdSubasta      ON Puja(IdSubasta);
CREATE INDEX IX_Suscripcion_Clave   ON Suscripcion(IdPostor, IdSubasta, Activa);
CREATE INDEX IX_UnidadDeVenta_Padre ON UnidadDeVenta(IdLotePadre);
GO

-- ============================================================
-- ============================================================
--   D A T O S   D E   P R U E B A
--   (Todo en un unico batch para usar variables DECLARE)
-- ============================================================
-- ============================================================
DECLARE @hash VARCHAR(512) = '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9';

-- ============================================================
-- USUARIOS (10) — password de todos: admin123
-- ============================================================
INSERT INTO Usuario (Nombre, Apellido, Email, PasswordHash, Rol, Activo, FechaAlta) VALUES
('Admin',      'Sistema',   'admin',        @hash, 'Administrador', 1, '2026-01-10 08:00:00'),
('Carlos',     'Mendez',    'martillero',   @hash, 'Martillero',    1, '2026-01-10 08:05:00'),
('Laura',      'Torres',    'operador',     @hash, 'Operador',      1, '2026-01-10 08:10:00'),
('Roberto',    'Silva',     'supervisor',   @hash, 'Supervisor',    1, '2026-01-10 08:15:00'),
('Diego',      'Suarez',    'martillero2',  @hash, 'Martillero',    1, '2026-02-01 09:00:00'),
('Fernanda',   'Castro',    'operador2',    @hash, 'Operador',      1, '2026-02-01 09:10:00'),
('Miguel',     'Romero',    'operador3',    @hash, 'Operador',      1, '2026-02-15 10:00:00'),
('Patricia',   'Leal',      'supervisor2',  @hash, 'Supervisor',    1, '2026-02-15 10:05:00'),
('Sebastian',  'Rojas',     'martillero3',  @hash, 'Martillero',    1, '2026-03-01 08:30:00'),
('Claudia',    'Vega',      'admin2',       @hash, 'Administrador', 1, '2026-03-01 08:35:00');

-- ============================================================
-- POSTORES (30)
-- ============================================================
INSERT INTO Postor (Nombre, DniCuit, Email, Telefono, Canal, FechaAlta) VALUES
('Juan Perez',             '28456789',      'juan.perez@mail.com',           '011-4523-7890', 'SALA',  '2026-01-15 09:00:00'),
('Maria Garcia',           '32100456',      'maria.garcia@mail.com',         '011-4789-1234', 'WEB',   '2026-01-16 10:30:00'),
('Tech Solutions S.A.',    '30-71234567-9', 'compras@techsol.com.ar',        '011-5522-8800', 'MOVIL', '2026-01-18 11:00:00'),
('Ana Kovalenko',          '35678900',      'ana.kovalenko@gmail.com',       '221-4500-2211', 'WEB',   '2026-02-01 14:00:00'),
('Ricardo Fontana',        '20987654',      'rfontana@outlook.com',          '261-4800-3345', 'SALA',  '2026-02-03 09:15:00'),
('Coleccionistas Arg.',    '30-68901234-5', 'info@colecarg.com.ar',          '011-4312-9900', 'WEB',   '2026-02-10 16:00:00'),
('Valentina Ruiz',         '38120045',      'vruiz_arte@hotmail.com',        '351-5600-7788', 'MOVIL', '2026-03-05 10:00:00'),
('Horacio Blanco',         '25334411',      'hblanco.inversiones@gmail.com', '011-4966-5544', 'SALA',  '2026-03-12 08:30:00'),
('Ignacio Pereyra',        '30221899',      'ipereyra@inversiones.com.ar',   '011-4701-2233', 'WEB',   '2026-03-18 09:00:00'),
('Susana Villalba',        '27443210',      'svilalba@gmail.com',            '0299-4455-667', 'SALA',  '2026-03-20 11:00:00'),
('Importadora del Sur',    '30-65432100-1', 'compras@impsur.com.ar',         '011-4900-1122', 'MOVIL', '2026-03-22 14:00:00'),
('Matias Benegas',         '36500121',      'mbenegas@hotmail.com',          '261-4601-8899', 'WEB',   '2026-03-25 10:00:00'),
('Lorena Casas',           '29887600',      'lcasas.arte@gmail.com',         '351-4822-5566', 'WEB',   '2026-04-01 09:30:00'),
('Pablo Molinari',         '22341567',      'pmolinari@molinarigroup.com',   '011-5100-3344', 'SALA',  '2026-04-02 08:00:00'),
('Alejandro Niro',         '34100789',      'aniro@aniro.com.ar',            '011-4455-6677', 'WEB',   '2026-04-03 10:15:00'),
('Familia Etcheverry',     '27654321',      'etcheverry.familia@gmail.com',  '221-4300-9988', 'SALA',  '2026-04-05 09:00:00'),
('Beatriz Cardozo',        '31200456',      'bcardozo@telecom.net.ar',       '011-4811-2200', 'MOVIL', '2026-04-08 11:00:00'),
('Grupo Inversores BA',    '30-70123456-8', 'contacto@grupoinvba.com.ar',    '011-5200-7788', 'WEB',   '2026-04-09 15:00:00'),
('Ernesto Salguero',       '18990033',      'esalguero_colec@gmail.com',     '011-4722-0011', 'SALA',  '2026-04-10 09:00:00'),
('Nicolas Arce',           '33214500',      'narce@arceautos.com.ar',        '011-4344-5566', 'MOVIL', '2026-04-11 10:00:00'),
('Claudia Ferraro',        '30456789',      'cferraro@gmail.com',            '351-5700-1122', 'WEB',   '2026-04-12 08:30:00'),
('Industrias Pampa',       '30-80200300-5', 'licitaciones@inpampa.com.ar',   '011-4600-8800', 'MOVIL', '2026-04-14 09:30:00'),
('Hernan Quiroga',         '25789012',      'hquiroga.repuestos@gmail.com',  '261-4500-7733', 'SALA',  '2026-04-15 11:00:00'),
('Diana Montes',           '37001234',      'dmontes_arte@hotmail.com',      '011-4788-4455', 'WEB',   '2026-04-16 09:00:00'),
('Federico Zamponi',       '23678900',      'fzamponi@zamponipianos.com.ar', '011-4301-9977', 'WEB',   '2026-04-17 10:00:00'),
('Armando Rios',           '20100200',      'arios@rioslogistica.com.ar',    '011-5500-6611', 'MOVIL', '2026-04-18 08:00:00'),
('Cecilia Prado',          '35900100',      'cprado_joyeria@gmail.com',      '011-4455-0033', 'WEB',   '2026-04-19 09:30:00'),
('Flavia Mendoza',         '32890011',      'fmendoza@mendozaarte.com.ar',   '221-4200-5544', 'SALA',  '2026-04-20 10:00:00'),
('Constructora Pampero',   '30-66700800-2', 'equipos@pampero.com.ar',        '011-4899-2200', 'MOVIL', '2026-04-21 11:00:00'),
('Gustavo Barros',         '28001122',      'gbarros_inversiones@gmail.com', '011-4677-3311', 'WEB',   '2026-04-22 09:00:00');

-- ============================================================
-- CATALOGO — Patron Composite
--
-- Jerarquia:
--   [3]  Lote Bellas Artes S.XIX  → hijos: [1] Oleo, [2] Acuarela
--   [6]  Lote Porcelanas Meissen  → hijos: [7] Juego te, [8] Florero
--   [9]  Lote Antiguedades EU     → hijos: [4] Reloj, [5] Candelabros, [6] Lote Meissen
--   [27] Lote Vehiculos Premium   → hijos: [15] BMW, [16] Ford F-150
--   [28] Lote Maquinaria Indust.  → hijos: [13] Torno, [18] Generador, [19] Fresadora, [20] Compresor
--   [29] Lote Herrami. y Repuest. → hijos: [21] Snap-on, [22] Torquimetro, [23] Repuestos, [24] Kit susp.
--   [30] Lote Musical Clasico     → hijos: [25] Piano Steinway, [26] Violin
--
--   Articulos sueltos (sin lote): [10] Collar, [11] Anillo, [12] Escritorio,
--                                  [14] Vinos, [17] Moto Harley,
--                                  [31] Tapiz, [32] Silla, [33] Camara Leica
-- ============================================================
DECLARE @idLoteBellasArtes  INT,
        @idLoteMeissen      INT,
        @idLoteAntiguedades INT,
        @idLoteVehiculos    INT,
        @idLoteMaquinaria   INT,
        @idLoteHerramientas INT,
        @idLoteMusical      INT;

-- ---- Id=1: Oleo ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Oleo "Atardecer en el Rio"',
        'Ramon Gomez Cornet, 1923. Oleo sobre tela, 80x60 cm. Marco dorado original. Certificado de autenticidad.',
        'ARTICULO', 180000.00, '2026-03-01 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 180000.00);

-- ---- Id=2: Acuarela ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Acuarela "Vista del Puerto de Buenos Aires"',
        'Prilidiano Pueyrredon (atribuida), c.1860. Acuarela sobre papel, 45x30 cm. Enmarcada con paspartú.',
        'ARTICULO', 95000.00, '2026-03-01 09:10:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 95000.00);

-- ---- Id=3: Lote Bellas Artes ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Bellas Artes — Siglo XIX',
        'Dos obras argentinas del s.XIX: oleo Gomez Cornet + acuarela atribuida a Pueyrredon.',
        'LOTE', 275000.00, '2026-03-01 09:20:00', 1);
SET @idLoteBellasArtes = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteBellasArtes, 2);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteBellasArtes WHERE Id IN (1, 2);

-- ---- Id=4: Reloj ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Reloj de pie aleman — c.1890',
        'Junghans, circa 1890. Caja roble tallado, 195 cm. Mecanismo original en funcionamiento. Restaurado 2019.',
        'ARTICULO', 320000.00, '2026-03-05 10:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 320000.00);

-- ---- Id=5: Candelabros ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Candelabros de plata maciza x6',
        'Plata 925, punzonados Londres 1875. Altura 42 cm c/u. Peso total 4.8 kg. Estuche original.',
        'ARTICULO', 210000.00, '2026-03-05 10:15:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 210000.00);

-- ---- Id=6: Lote Meissen ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Porcelanas Meissen',
        'Dos piezas Meissen s.XIX. Marca espada cruzada verificada por perito. Incluye certificados.',
        'LOTE', 155000.00, '2026-03-05 10:30:00', 1);
SET @idLoteMeissen = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteMeissen, 2);

-- ---- Id=7: Juego te (hijo de Meissen) ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo, IdLotePadre)
VALUES ('Juego de te Meissen — 12 piezas',
        'Tetera, azucarera, lechera y 9 tazas con platos. Decoracion floral policromada, c.1870.',
        'ARTICULO', 90000.00, '2026-03-05 10:35:00', 1, @idLoteMeissen);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 90000.00);

-- ---- Id=8: Florero (hijo de Meissen) ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo, IdLotePadre)
VALUES ('Florero Meissen — motivo floral',
        'Porcelana Meissen, altura 35 cm, decoracion rosas y pajaros pintada a mano, c.1860.',
        'ARTICULO', 65000.00, '2026-03-05 10:40:00', 1, @idLoteMeissen);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 65000.00);

-- ---- Id=9: Lote Antiguedades Europeas ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Antiguedades Europeas',
        'Reloj Junghans + candelabros plata + Lote Porcelanas Meissen. Premio al mejor lote — Feria 2025.',
        'LOTE', 685000.00, '2026-03-05 11:00:00', 1);
SET @idLoteAntiguedades = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteAntiguedades, 3);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteAntiguedades WHERE Id IN (4, 5);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteAntiguedades WHERE Id = @idLoteMeissen;

-- ---- Id=10: Collar ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Collar de brillantes Art Deco',
        'Oro blanco 18k, 47 brillantes talla baguette, total 2.3 ct. Certificado GIA. Caja Cartier original.',
        'ARTICULO', 850000.00, '2026-03-10 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 850000.00);

-- ---- Id=11: Anillo ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Anillo solitario — diamante 2.1 ct',
        'Oro amarillo 18k. Diamante 2.1 ct, talla brillante, color G, claridad VS1. Certificado IGI. Aro pavé.',
        'ARTICULO', 1200000.00, '2026-03-10 09:30:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 1200000.00);

-- ---- Id=12: Escritorio ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Escritorio victoriano en roble macizo',
        'Ingles, c.1880. Tapete cuero verde, 9 cajones, tiradores bronce. 155x80x78 cm. Firma del ebanista.',
        'ARTICULO', 145000.00, '2026-03-15 10:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 145000.00);

-- ---- Id=13: Torno CNC ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Torno CNC Mazak Modelo X200',
        'Torno control numerico Mazak X200, 2019. Control Mazatrol SmoothC. Incluye herramientas y documentacion.',
        'ARTICULO', 45000.00, '2026-03-18 08:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 45000.00);

-- ---- Id=14: Vinos ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Coleccion de vinos — Reserva Especial',
        '12 botellas: 6x Catena Zapata Adrianna 2018 + 3x Achaval Ferrer Quimera 2019 + 3x Clos de los Siete 2020.',
        'ARTICULO', 38000.00, '2026-03-20 11:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 38000.00);

-- ---- Id=15: BMW ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('BMW Serie 7 730d xDrive 2020',
        'Sedan de lujo, diesel, 265 CV, traccion integral. Color negro zafiro, interior cuero beige. 48.000 km.',
        'ARTICULO', 8500000.00, '2026-04-01 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 8500000.00);

-- ---- Id=16: Ford F-150 ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Ford F-150 Raptor 2021',
        'Pick-up alta performance, V6 EcoBoost biturbo 450 CV. Color gris Leadfoot. 22.500 km. Pack Raptor R.',
        'ARTICULO', 6200000.00, '2026-04-01 09:30:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 6200000.00);

-- ---- Id=17: Harley ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Motocicleta Harley-Davidson Road King Special 2022',
        'Motor Milwaukee-Eight 114, 1868 cc. Color Vivid Black. 9.800 km. Accesorios originales: alforjas + sissy bar.',
        'ARTICULO', 1850000.00, '2026-04-02 10:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 1850000.00);

-- ---- Id=18: Generador ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Generador Diesel Stamford 150 kVA',
        'Grupo electrogeno Stamford, motor Perkins 150 kVA / 120 kW. Tablero ATS. 850 hs de uso. Incluye transferencia.',
        'ARTICULO', 380000.00, '2026-04-05 08:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 380000.00);

-- ---- Id=19: Fresadora ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Fresadora CNC Haas VF-2 2018',
        'Centro de mecanizado vertical Haas VF-2, mesa 914x356 mm, 30 posiciones de herramientas, 2018.',
        'ARTICULO', 220000.00, '2026-04-05 08:30:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 220000.00);

-- ---- Id=20: Compresor ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Compresor industrial Atlas Copco GA30 200L',
        'Compresor tornillo rotativo Atlas Copco GA30, 30 kW, deposito 200L, presion max 13 bar. 2020. Poco uso.',
        'ARTICULO', 95000.00, '2026-04-05 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 95000.00);

-- ---- Id=21: Set herramientas Snap-on ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Set herramientas Snap-on 312 piezas',
        'Kit profesional Snap-on 312 piezas: llaves combinadas, torx, allen, dados 1/4 1/2 y 3/4. Carro rodante incluido.',
        'ARTICULO', 45000.00, '2026-04-08 10:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 45000.00);

-- ---- Id=22: Torquimetro ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Llave torquimetro digital Snap-on TECHANGLE',
        'Torquimetro digital Snap-on TECHANGLE, rango 3-200 Nm, precision ±1%. Calibrado y certificado 2025.',
        'ARTICULO', 12000.00, '2026-04-08 10:15:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 12000.00);

-- ---- Id=23: Repuestos motor ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Bolsa repuestos motor Ford Ranger 2.5 Duratec',
        'Kit completo: juntas, bieletas, pistones, anillos y valvulas para motor Ford Duratec 2.5. Originales Ford.',
        'ARTICULO', 28000.00, '2026-04-08 10:30:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 28000.00);

-- ---- Id=24: Kit suspension ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Kit suspension Bilstein B8 Performance Plus',
        'Amortiguadores delanteros y traseros Bilstein B8 + resortes Eibach para VW Golf VII. Nuevos en caja.',
        'ARTICULO', 35000.00, '2026-04-08 10:45:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 35000.00);

-- ---- Id=25: Piano Steinway ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Piano de cola Steinway & Sons Modelo B 1948',
        'Piano de cola Steinway Modelo B, Hamburg 1948, 211 cm. Lacado negro. Revisado y afinado. 88 teclas, cuerdas nuevas.',
        'ARTICULO', 2800000.00, '2026-04-10 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 2800000.00);

-- ---- Id=26: Violin ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Violin solista italiano — Francesco Ruggieri c.1685',
        'Violin italiano Francesco Ruggieri, Cremona c.1685. Certificado Beare & Olofsson. Estuche cuero y arco Pernambuco.',
        'ARTICULO', 480000.00, '2026-04-10 09:30:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 480000.00);

-- ---- Id=27: Lote Vehiculos Premium ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Vehiculos Premium',
        'BMW 730d 2020 + Ford F-150 Raptor 2021. Ambos en excelente estado, documentacion al dia.',
        'LOTE', 14700000.00, '2026-04-01 10:00:00', 1);
SET @idLoteVehiculos = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteVehiculos, 2);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteVehiculos WHERE Id IN (15, 16);

-- ---- Id=28: Lote Maquinaria Industrial ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Maquinaria Industrial',
        'Torno CNC Mazak + Generador Stamford 150kVA + Fresadora Haas VF-2 + Compresor Atlas Copco. Ideal planta productiva.',
        'LOTE', 740000.00, '2026-04-05 10:00:00', 1);
SET @idLoteMaquinaria = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteMaquinaria, 4);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteMaquinaria WHERE Id IN (13, 18, 19, 20);

-- ---- Id=29: Lote Herramientas y Repuestos ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Herramientas y Repuestos',
        'Set Snap-on 312 pz + Torquimetro digital + Kit repuestos Ford Duratec + Kit suspension Bilstein.',
        'LOTE', 120000.00, '2026-04-08 11:00:00', 1);
SET @idLoteHerramientas = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteHerramientas, 4);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteHerramientas WHERE Id IN (21, 22, 23, 24);

-- ---- Id=30: Lote Musical Clasico ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Musical Clasico',
        'Piano de cola Steinway Modelo B 1948 + Violin solista italiano Ruggieri c.1685. Conjunto excepcional.',
        'LOTE', 3280000.00, '2026-04-10 10:00:00', 1);
SET @idLoteMusical = SCOPE_IDENTITY();
INSERT INTO Lote (Id, CantidadComponentes) VALUES (@idLoteMusical, 2);
UPDATE UnidadDeVenta SET IdLotePadre = @idLoteMusical WHERE Id IN (25, 26);

-- ---- Id=31: Tapiz flamenco ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Tapiz flamenco — Sevilla s.XVII',
        'Tapiz lana y seda, tejido a mano, motivos geometricos andaluces. 280x190 cm. Restaurado 2021. Certificado perito.',
        'ARTICULO', 175000.00, '2026-04-12 09:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 175000.00);

-- ---- Id=32: Silla Luis XV ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Silla Luis XV — nogal tallado c.1760',
        'Silla estilo Louis XV, nogal tallado, tapizado terciopelo azul cobalto. Francia, c.1760. Certificado perito Paris.',
        'ARTICULO', 110000.00, '2026-04-12 09:30:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 110000.00);

-- ---- Id=33: Camara Leica ----
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Camara Leica M3 — cuerpo + lente Summicron 50mm',
        'Leica M3 Double Stroke, 1954. Shutter funciona correctamente. Lente Summicron 50mm f/2. Estuche de cuero original.',
        'ARTICULO', 320000.00, '2026-04-14 10:00:00', 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado)
VALUES (SCOPE_IDENTITY(), 320000.00);

-- ============================================================
-- SUBASTAS (21)
--   CERRADAS: 1-12
--   ACTIVAS:  13-18
--   PENDIENTES: 19-21
-- IdMartillero 2 = Carlos Mendez, 5 = Diego Suarez, 9 = Sebastian Rojas
-- ============================================================
DECLARE @s1  INT, @s2  INT, @s3  INT, @s4  INT, @s5  INT, @s6  INT,
        @s7  INT, @s8  INT, @s9  INT, @s10 INT, @s11 INT, @s12 INT,
        @s13 INT, @s14 INT, @s15 INT, @s16 INT, @s17 INT, @s18 INT,
        @s19 INT, @s20 INT, @s21 INT;

-- ---- Sub 1: Collar (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (10, 2, 'CERRADA', 850000.00, 1050000.00, '2026-04-10 10:00:00', '2026-04-10 11:45:00', 6, 1050000.00);
SET @s1 = SCOPE_IDENTITY();

-- ---- Sub 2: Lote Bellas Artes (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (3, 2, 'CERRADA', 275000.00, 340000.00, '2026-04-15 14:00:00', '2026-04-15 15:30:00', 4, 340000.00);
SET @s2 = SCOPE_IDENTITY();

-- ---- Sub 3: Reloj de pie (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (4, 2, 'CERRADA', 320000.00, 415000.00, '2026-04-22 10:00:00', '2026-04-22 12:00:00', 8, 415000.00);
SET @s3 = SCOPE_IDENTITY();

-- ---- Sub 4: Anillo solitario (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (11, 5, 'CERRADA', 1200000.00, 1580000.00, '2026-04-25 10:00:00', '2026-04-25 12:30:00', 12, 1580000.00);
SET @s4 = SCOPE_IDENTITY();

-- ---- Sub 5: Piano Steinway (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (25, 2, 'CERRADA', 2800000.00, 3200000.00, '2026-04-28 10:00:00', '2026-04-28 11:20:00', 15, 3200000.00);
SET @s5 = SCOPE_IDENTITY();

-- ---- Sub 6: BMW Serie 7 (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (15, 9, 'CERRADA', 8500000.00, 9100000.00, '2026-04-30 09:00:00', '2026-04-30 10:45:00', 18, 9100000.00);
SET @s6 = SCOPE_IDENTITY();

-- ---- Sub 7: Escritorio victoriano (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (12, 5, 'CERRADA', 145000.00, 165000.00, '2026-05-02 14:00:00', '2026-05-02 15:00:00', 3, 165000.00);
SET @s7 = SCOPE_IDENTITY();

-- ---- Sub 8: Generador Diesel (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (18, 2, 'CERRADA', 380000.00, 420000.00, '2026-05-05 10:00:00', '2026-05-05 11:30:00', 22, 420000.00);
SET @s8 = SCOPE_IDENTITY();

-- ---- Sub 9: Coleccion de vinos (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (14, 5, 'CERRADA', 38000.00, 45000.00, '2026-05-06 16:00:00', '2026-05-06 17:00:00', 5, 45000.00);
SET @s9 = SCOPE_IDENTITY();

-- ---- Sub 10: Moto Harley-Davidson (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (17, 9, 'CERRADA', 1850000.00, 2050000.00, '2026-05-08 10:00:00', '2026-05-08 11:45:00', 9, 2050000.00);
SET @s10 = SCOPE_IDENTITY();

-- ---- Sub 11: Juego de te Meissen (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (7, 2, 'CERRADA', 90000.00, 110000.00, '2026-05-10 14:00:00', '2026-05-10 15:10:00', 25, 110000.00);
SET @s11 = SCOPE_IDENTITY();

-- ---- Sub 12: Set herramientas Snap-on (CERRADA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura, FechaCierre, IdGanador, PrecioFinal)
VALUES (21, 5, 'CERRADA', 45000.00, 52000.00, '2026-05-12 10:00:00', '2026-05-12 11:00:00', 7, 52000.00);
SET @s12 = SCOPE_IDENTITY();

-- ---- Sub 13: Lote Antiguedades (ACTIVA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (9, 2, 'ACTIVA', 685000.00, 750000.00, '2026-05-16 09:00:00');
SET @s13 = SCOPE_IDENTITY();

-- ---- Sub 14: Ford F-150 (ACTIVA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (16, 9, 'ACTIVA', 6200000.00, 7000000.00, '2026-05-16 09:30:00');
SET @s14 = SCOPE_IDENTITY();

-- ---- Sub 15: Lote Maquinaria Industrial (ACTIVA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (28, 2, 'ACTIVA', 740000.00, 790000.00, '2026-05-16 10:00:00');
SET @s15 = SCOPE_IDENTITY();

-- ---- Sub 16: Violin italiano (ACTIVA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (26, 5, 'ACTIVA', 480000.00, 530000.00, '2026-05-16 10:30:00');
SET @s16 = SCOPE_IDENTITY();

-- ---- Sub 17: Tapiz flamenco (ACTIVA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (31, 5, 'ACTIVA', 175000.00, 185000.00, '2026-05-16 11:00:00');
SET @s17 = SCOPE_IDENTITY();

-- ---- Sub 18: Silla Luis XV (ACTIVA) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (32, 9, 'ACTIVA', 110000.00, 115000.00, '2026-05-16 11:30:00');
SET @s18 = SCOPE_IDENTITY();

-- ---- Sub 19: Lote Musical Clasico (PENDIENTE) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (30, 2, 'PENDIENTE', 3280000.00, 3280000.00, '2026-05-16 08:00:00');
SET @s19 = SCOPE_IDENTITY();

-- ---- Sub 20: Camara Leica (PENDIENTE) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (33, 5, 'PENDIENTE', 320000.00, 320000.00, '2026-05-16 08:00:00');
SET @s20 = SCOPE_IDENTITY();

-- ---- Sub 21: Kit suspension (PENDIENTE) ----
INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura)
VALUES (24, 9, 'PENDIENTE', 35000.00, 35000.00, '2026-05-16 08:00:00');
SET @s21 = SCOPE_IDENTITY();

-- ============================================================
-- PUJAS
-- ============================================================

-- === Sub 1: Collar de brillantes ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s1, 1,  870000.00,  '2026-04-10 10:08:00', 'ACEPTADA'),
(@s1, 6,  900000.00,  '2026-04-10 10:15:00', 'ACEPTADA'),
(@s1, 3,  880000.00,  '2026-04-10 10:16:00', 'RECHAZADA'),
(@s1, 5,  950000.00,  '2026-04-10 10:22:00', 'ACEPTADA'),
(@s1, 6,  1000000.00, '2026-04-10 10:35:00', 'ACEPTADA'),
(@s1, 5,  990000.00,  '2026-04-10 10:36:00', 'RECHAZADA'),
(@s1, 6,  1050000.00, '2026-04-10 11:44:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s1;

-- === Sub 2: Lote Bellas Artes ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s2, 7,  285000.00, '2026-04-15 14:07:00', 'ACEPTADA'),
(@s2, 4,  300000.00, '2026-04-15 14:15:00', 'ACEPTADA'),
(@s2, 7,  295000.00, '2026-04-15 14:16:00', 'RECHAZADA'),
(@s2, 2,  315000.00, '2026-04-15 14:28:00', 'ACEPTADA'),
(@s2, 4,  340000.00, '2026-04-15 15:10:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s2;

-- === Sub 3: Reloj de pie ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s3, 8,  335000.00, '2026-04-22 10:10:00', 'ACEPTADA'),
(@s3, 1,  355000.00, '2026-04-22 10:22:00', 'ACEPTADA'),
(@s3, 3,  340000.00, '2026-04-22 10:23:00', 'RECHAZADA'),
(@s3, 8,  380000.00, '2026-04-22 10:45:00', 'ACEPTADA'),
(@s3, 1,  370000.00, '2026-04-22 10:46:00', 'RECHAZADA'),
(@s3, 8,  415000.00, '2026-04-22 11:58:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s3;

-- === Sub 4: Anillo solitario ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s4, 27, 1230000.00, '2026-04-25 10:08:00', 'ACEPTADA'),
(@s4, 12, 1300000.00, '2026-04-25 10:20:00', 'ACEPTADA'),
(@s4, 27, 1290000.00, '2026-04-25 10:21:00', 'RECHAZADA'),
(@s4, 4,  1380000.00, '2026-04-25 10:45:00', 'ACEPTADA'),
(@s4, 12, 1450000.00, '2026-04-25 11:00:00', 'ACEPTADA'),
(@s4, 4,  1500000.00, '2026-04-25 11:20:00', 'ACEPTADA'),
(@s4, 12, 1580000.00, '2026-04-25 12:25:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s4;

-- === Sub 5: Piano Steinway ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s5, 25, 2850000.00, '2026-04-28 10:05:00', 'ACEPTADA'),
(@s5, 15, 2950000.00, '2026-04-28 10:15:00', 'ACEPTADA'),
(@s5, 25, 2920000.00, '2026-04-28 10:16:00', 'RECHAZADA'),
(@s5, 15, 3100000.00, '2026-04-28 10:40:00', 'ACEPTADA'),
(@s5, 15, 3200000.00, '2026-04-28 11:18:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s5;

-- === Sub 6: BMW Serie 7 ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s6, 20, 8600000.00, '2026-04-30 09:10:00', 'ACEPTADA'),
(@s6, 18, 8750000.00, '2026-04-30 09:25:00', 'ACEPTADA'),
(@s6, 14, 8700000.00, '2026-04-30 09:26:00', 'RECHAZADA'),
(@s6, 20, 8900000.00, '2026-04-30 09:50:00', 'ACEPTADA'),
(@s6, 18, 9100000.00, '2026-04-30 10:40:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s6;

-- === Sub 7: Escritorio victoriano ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s7, 13, 148000.00, '2026-05-02 14:10:00', 'ACEPTADA'),
(@s7, 3,  155000.00, '2026-05-02 14:25:00', 'ACEPTADA'),
(@s7, 13, 150000.00, '2026-05-02 14:26:00', 'RECHAZADA'),
(@s7, 3,  165000.00, '2026-05-02 14:55:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s7;

-- === Sub 8: Generador Diesel ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s8, 29, 385000.00, '2026-05-05 10:08:00', 'ACEPTADA'),
(@s8, 22, 395000.00, '2026-05-05 10:20:00', 'ACEPTADA'),
(@s8, 11, 390000.00, '2026-05-05 10:21:00', 'RECHAZADA'),
(@s8, 22, 420000.00, '2026-05-05 11:25:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s8;

-- === Sub 9: Coleccion vinos ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s9, 10, 40000.00, '2026-05-06 16:05:00', 'ACEPTADA'),
(@s9, 5,  42000.00, '2026-05-06 16:18:00', 'ACEPTADA'),
(@s9, 5,  45000.00, '2026-05-06 16:55:00', 'ACEPTADA');

-- === Sub 10: Moto Harley ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s10, 20, 1870000.00, '2026-05-08 10:10:00', 'ACEPTADA'),
(@s10, 9,  1920000.00, '2026-05-08 10:28:00', 'ACEPTADA'),
(@s10, 20, 1900000.00, '2026-05-08 10:29:00', 'RECHAZADA'),
(@s10, 9,  2050000.00, '2026-05-08 11:40:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s10;

-- === Sub 11: Juego de te Meissen ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s11, 6,   92000.00, '2026-05-10 14:05:00', 'ACEPTADA'),
(@s11, 25, 100000.00, '2026-05-10 14:20:00', 'ACEPTADA'),
(@s11, 6,   98000.00, '2026-05-10 14:21:00', 'RECHAZADA'),
(@s11, 25, 110000.00, '2026-05-10 15:05:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s11;

-- === Sub 12: Set herramientas Snap-on ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s12, 23, 46000.00, '2026-05-12 10:05:00', 'ACEPTADA'),
(@s12, 7,  48000.00, '2026-05-12 10:18:00', 'ACEPTADA'),
(@s12, 23, 47500.00, '2026-05-12 10:19:00', 'RECHAZADA'),
(@s12, 7,  52000.00, '2026-05-12 10:55:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s12;

-- === Sub 13: Lote Antiguedades (ACTIVA) ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s13, 8,  700000.00, '2026-05-16 09:08:00', 'ACEPTADA'),
(@s13, 19, 720000.00, '2026-05-16 09:22:00', 'ACEPTADA'),
(@s13, 8,  710000.00, '2026-05-16 09:23:00', 'RECHAZADA'),
(@s13, 8,  750000.00, '2026-05-16 09:50:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s13;

-- === Sub 14: Ford F-150 (ACTIVA) ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s14, 20, 6400000.00, '2026-05-16 09:38:00', 'ACEPTADA'),
(@s14, 26, 6700000.00, '2026-05-16 09:55:00', 'ACEPTADA'),
(@s14, 20, 6600000.00, '2026-05-16 09:56:00', 'RECHAZADA'),
(@s14, 20, 7000000.00, '2026-05-16 10:20:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s14;

-- === Sub 15: Lote Maquinaria (ACTIVA) ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s15, 29, 750000.00, '2026-05-16 10:05:00', 'ACEPTADA'),
(@s15, 11, 770000.00, '2026-05-16 10:18:00', 'ACEPTADA'),
(@s15, 29, 760000.00, '2026-05-16 10:19:00', 'RECHAZADA'),
(@s15, 11, 790000.00, '2026-05-16 10:45:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s15;

-- === Sub 16: Violin (ACTIVA) ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s16, 24, 490000.00, '2026-05-16 10:35:00', 'ACEPTADA'),
(@s16, 28, 510000.00, '2026-05-16 10:48:00', 'ACEPTADA'),
(@s16, 24, 500000.00, '2026-05-16 10:49:00', 'RECHAZADA'),
(@s16, 28, 530000.00, '2026-05-16 11:10:00', 'ACEPTADA');
UPDATE Puja SET MotivoRechazo = 'Monto inferior al precio vigente' WHERE Estado = 'RECHAZADA' AND IdSubasta = @s16;

-- === Sub 17: Tapiz flamenco (ACTIVA) ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s17, 13, 178000.00, '2026-05-16 11:05:00', 'ACEPTADA'),
(@s17, 13, 185000.00, '2026-05-16 11:25:00', 'ACEPTADA');

-- === Sub 18: Silla Luis XV (ACTIVA) ===
INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado) VALUES
(@s18, 16, 112000.00, '2026-05-16 11:35:00', 'ACEPTADA'),
(@s18, 2,  115000.00, '2026-05-16 11:50:00', 'ACEPTADA');

-- ============================================================
-- ADJUDICACIONES
-- ============================================================
INSERT INTO Adjudicacion (IdSubasta, IdUnidad, IdGanador, PrecioFinal, FechaHoraAdjudicacion, IdMartillero, Observaciones) VALUES
(@s1,  10, 6,  1050000.00, '2026-04-10 11:46:00', 2, 'Collar adjudicado a Coleccionistas Arg. Pago contra entrega certificado.'),
(@s2,  3,  4,  340000.00,  '2026-04-15 15:32:00', 2, 'Lote Bellas Artes adjudicado a Ana Kovalenko. Retiro coordinado.'),
(@s3,  4,  8,  415000.00,  '2026-04-22 12:02:00', 2, 'Reloj adjudicado a Horacio Blanco. Traslado a cargo del comprador.'),
(@s4,  11, 12, 1580000.00, '2026-04-25 12:31:00', 5, 'Anillo solitario adjudicado a Matias Benegas. Pago en cuotas acordado.'),
(@s5,  25, 15, 3200000.00, '2026-04-28 11:22:00', 2, 'Piano Steinway adjudicado a Alejandro Niro. Traslado piano: empresa especializada.'),
(@s6,  15, 18, 9100000.00, '2026-04-30 10:46:00', 9, 'BMW adjudicado a Grupo Inversores BA. Transferencia dominio incluida.'),
(@s7,  12, 3,  165000.00,  '2026-05-02 15:01:00', 5, 'Escritorio adjudicado a Tech Solutions. Retiro en deposito.'),
(@s8,  18, 22, 420000.00,  '2026-05-05 11:31:00', 2, 'Generador adjudicado a Industrias Pampa. Flete a cargo del comprador.'),
(@s9,  14, 5,  45000.00,   '2026-05-06 17:01:00', 5, 'Vinos adjudicados a Ricardo Fontana. Retiro inmediato.'),
(@s10, 17, 9,  2050000.00, '2026-05-08 11:46:00', 9, 'Harley adjudicada a Ignacio Pereyra. Transferencia moto gestionada.'),
(@s11, 7,  25, 110000.00,  '2026-05-10 15:12:00', 2, 'Juego de te Meissen adjudicado a Federico Zamponi. Embalaje especial.'),
(@s12, 21, 7,  52000.00,   '2026-05-12 11:01:00', 5, 'Set Snap-on adjudicado a Valentina Ruiz. Retiro inmediato.');

-- ============================================================
-- SUSCRIPCIONES (6 subastas activas + 3 pendientes)
-- ============================================================
INSERT INTO Suscripcion (IdPostor, IdSubasta, FechaSuscripcion, Activa) VALUES
-- Sub 13: Lote Antiguedades
(8,  @s13, '2026-05-16 08:55:00', 1),
(19, @s13, '2026-05-16 09:00:00', 1),
(6,  @s13, '2026-05-16 09:05:00', 1),
(1,  @s13, '2026-05-16 09:10:00', 1),
-- Sub 14: Ford F-150
(20, @s14, '2026-05-16 09:25:00', 1),
(26, @s14, '2026-05-16 09:28:00', 1),
(14, @s14, '2026-05-16 09:30:00', 1),
-- Sub 15: Lote Maquinaria
(29, @s15, '2026-05-16 09:55:00', 1),
(11, @s15, '2026-05-16 10:00:00', 1),
(22, @s15, '2026-05-16 10:05:00', 1),
-- Sub 16: Violin
(24, @s16, '2026-05-16 10:28:00', 1),
(28, @s16, '2026-05-16 10:30:00', 1),
(25, @s16, '2026-05-16 10:32:00', 1),
-- Sub 17: Tapiz
(13, @s17, '2026-05-16 11:00:00', 1),
(24, @s17, '2026-05-16 11:02:00', 1),
-- Sub 18: Silla Luis XV
(16, @s18, '2026-05-16 11:28:00', 1),
(2,  @s18, '2026-05-16 11:30:00', 1),
-- Sub 19: Lote Musical (PENDIENTE — suscriptos anticipados)
(15, @s19, '2026-05-16 08:00:00', 1),
(25, @s19, '2026-05-16 08:05:00', 1),
(28, @s19, '2026-05-16 08:10:00', 1),
-- Sub 20: Camara Leica (PENDIENTE)
(27, @s20, '2026-05-16 08:00:00', 1),
(30, @s20, '2026-05-16 08:05:00', 1),
-- Sub 21: Kit suspension (PENDIENTE)
(23, @s21, '2026-05-16 08:00:00', 1),
(29, @s21, '2026-05-16 08:05:00', 1);

-- ============================================================
-- BITACORA
-- ============================================================
INSERT INTO Bitacora (Formulario, Accion, Criticidad, FechaHora, IdUsuario) VALUES
('Login',           'Inicio de sesion: admin',                                              'Baja',  '2026-04-10 09:45:00', 1),
('Subastas',        'Apertura subasta Collar brillantes — Sub ID ' + CAST(@s1  AS VARCHAR), 'Alta',  '2026-04-10 10:00:00', 2),
('Subastas',        'Cierre — Collar adjudicado. Ganador postor 6. $1.050.000',             'Alta',  '2026-04-10 11:46:00', 2),
('Subastas',        'Apertura subasta Lote Bellas Artes — Sub ID ' + CAST(@s2  AS VARCHAR), 'Alta',  '2026-04-15 14:00:00', 2),
('Subastas',        'Cierre — Lote Bellas Artes adjudicado. Ganador postor 4. $340.000',    'Alta',  '2026-04-15 15:32:00', 2),
('Subastas',        'Apertura subasta Reloj Junghans — Sub ID ' + CAST(@s3   AS VARCHAR),   'Alta',  '2026-04-22 10:00:00', 2),
('Subastas',        'Cierre — Reloj adjudicado. Ganador postor 8. $415.000',                'Alta',  '2026-04-22 12:02:00', 2),
('Subastas',        'Apertura subasta BMW Serie 7 — Sub ID ' + CAST(@s6   AS VARCHAR),      'Alta',  '2026-04-30 09:00:00', 9),
('Subastas',        'Cierre — BMW adjudicado. Ganador postor 18. $9.100.000',               'Alta',  '2026-04-30 10:46:00', 9),
('Subastas',        'Apertura subasta Lote Antiguedades — Sub ID ' + CAST(@s13 AS VARCHAR), 'Alta',  '2026-05-16 09:00:00', 2),
('Subastas',        'Apertura subasta Ford F-150 — Sub ID ' + CAST(@s14 AS VARCHAR),        'Alta',  '2026-05-16 09:30:00', 9),
('Subastas',        'Apertura subasta Lote Maquinaria — Sub ID ' + CAST(@s15 AS VARCHAR),   'Alta',  '2026-05-16 10:00:00', 2),
('Subastas',        'Apertura subasta Violin Ruggieri — Sub ID ' + CAST(@s16 AS VARCHAR),   'Alta',  '2026-05-16 10:30:00', 5),
('Postores',        'Alta postor: Juan Perez (SALA)',                                        'Baja',  '2026-01-15 09:05:00', 3),
('Postores',        'Alta postor: Coleccionistas Arg. (WEB)',                                'Baja',  '2026-02-10 16:10:00', 3),
('GestionUsuarios', 'Alta usuario: martillero (Carlos Mendez)',                              'Media', '2026-01-10 08:05:00', 1),
('GestionUsuarios', 'Alta usuario: operador (Laura Torres)',                                 'Media', '2026-01-10 08:06:00', 1),
('GestionUsuarios', 'Alta usuario: supervisor (Roberto Silva)',                              'Media', '2026-01-10 08:07:00', 1),
('GestionUsuarios', 'Alta usuario: martillero2 (Diego Suarez)',                             'Media', '2026-02-01 09:05:00', 1),
('Catalogo',        'Alta articulo: Collar de brillantes Art Deco ($850.000)',              'Baja',  '2026-03-10 09:05:00', 2);

PRINT '============================================================';
PRINT 'Base de datos AlmonedaNacionalDB creada correctamente.';
PRINT '';
PRINT 'Usuarios: 10  (admin / martillero / operador / supervisor + 6 mas)';
PRINT 'Postores: 30';
PRINT 'Catalogo: 33 items (26 articulos + 7 lotes con jerarquia anidada)';
PRINT 'Subastas: 21 (12 CERRADAS + 6 ACTIVAS + 3 PENDIENTES)';
PRINT 'Pujas:    62 (mezcla ACEPTADAS/RECHAZADAS)';
PRINT 'Adjudicaciones: 12  |  Suscripciones: 24  |  Bitacora: 20';
PRINT '============================================================';
GO


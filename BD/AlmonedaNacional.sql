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
-- DATOS INICIALES — Usuario Administrador por defecto
-- Password: admin123 (hash SHA-256)
-- ============================================================
INSERT INTO Usuario (Nombre, Apellido, Email, PasswordHash, Rol, Activo, FechaAlta)
VALUES (
    'Admin',
    'Sistema',
    'admin@almoneda.com',
    -- SHA-256 de "admin123"
    '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
    'Administrador',
    1,
    GETDATE()
);
GO

-- ============================================================
-- DATOS DE PRUEBA
-- ============================================================

-- Usuarios
INSERT INTO Usuario (Nombre, Apellido, Email, PasswordHash, Rol, Activo, FechaAlta) VALUES
('Carlos',  'Mendez',  'martillero@almoneda.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Martillero', 1, GETDATE()),
('Laura',   'Torres',  'operador@almoneda.com',   '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Operador',   1, GETDATE()),
('Roberto', 'Silva',   'supervisor@almoneda.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Supervisor', 1, GETDATE());
GO

-- Postores
INSERT INTO Postor (Nombre, DniCuit, Email, Canal, FechaAlta) VALUES
('Juan Perez',      '28456789',  'juan@mail.com',   'SALA', GETDATE()),
('Maria Garcia',    '32100456',  'maria@mail.com',  'WEB',  GETDATE()),
('Tech Solutions',  '30-71234567-9', 'tech@corp.com', 'MOVIL', GETDATE());
GO

-- Catalogo de prueba (Composite en accion)
-- Articulo individual
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Torno CNC Modelo X200', 'Torno de control numerico, 2019, buen estado', 'ARTICULO', 45000.00, GETDATE(), 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 45000.00, 'Maquinaria', 'Bueno', 'Deposito A - Sector 3');

INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Juego de Herramientas Industriales', 'Set completo de llaves y herramientas', 'ARTICULO', 8500.00, GETDATE(), 1);
INSERT INTO ArticuloIndividual (Id, ValorDeclarado, Categoria, EstadoFisico, Ubicacion)
VALUES (SCOPE_IDENTITY(), 8500.00, 'Herramientas', 'Excelente', 'Deposito A - Sector 1');

-- Lote que contiene los articulos anteriores
INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo)
VALUES ('Lote Sector A — Maquinaria', 'Lote compuesto: torno + herramientas del sector A', 'LOTE', 53500.00, GETDATE(), 1);
INSERT INTO Lote (Id, CantidadComponentes) VALUES (SCOPE_IDENTITY(), 0);
GO

PRINT 'Base de datos AlmonedaNacionalDB creada correctamente.';
GO

-- ============================================================
-- FormulariosUAF — Script de creación de base de datos
-- SQL Server 2019+
-- Generado: 2026-06-04
-- Nota: Las tablas de Identity (Usuarios, Roles, etc.) son
--       generadas automáticamente por EF Core Migrations.
--       Este script documenta las tablas de negocio.
-- ============================================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'FormulariosUAF')
    CREATE DATABASE FormulariosUAF COLLATE Modern_Spanish_CI_AS;
GO

USE FormulariosUAF;
GO

-- ============================================================
-- Tabla: Clientes
-- ============================================================
IF OBJECT_ID('Clientes', 'U') IS NULL
CREATE TABLE Clientes (
    Id          INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RUT         NVARCHAR(20)        NOT NULL,
    BusinessName NVARCHAR(500)      NOT NULL,
    IsActive    BIT                 NOT NULL DEFAULT 1,
    CreatedAt   DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy   NVARCHAR(450)       NOT NULL DEFAULT '',
    UpdatedAt   DATETIME2               NULL,
    UpdatedBy   NVARCHAR(450)           NULL,
    CONSTRAINT UQ_Clientes_RUT UNIQUE (RUT)
);
GO

-- ============================================================
-- Tabla: Solicitudes
-- ============================================================
IF OBJECT_ID('Solicitudes', 'U') IS NULL
CREATE TABLE Solicitudes (
    Id              UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWID() PRIMARY KEY,
    RequestNumber   NVARCHAR(50)        NOT NULL,
    ClientId        INT                 NOT NULL,
    VendorUserId    NVARCHAR(450)       NOT NULL,
    RequestType     INT                 NOT NULL,  -- 0=ClienteNuevo,1=TransaccionUnica,2=Actualización,3=ActSinCambios
    Status          INT                 NOT NULL DEFAULT 0,
    ClientToken     NVARCHAR(200)       NOT NULL,
    TokenExpiry     DATETIME2           NOT NULL,
    InternalNotes   NVARCHAR(2000)          NULL,
    CurrentStep     INT                 NOT NULL DEFAULT 1,
    IsDeleted       BIT                 NOT NULL DEFAULT 0,
    CreatedAt       DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy       NVARCHAR(450)       NOT NULL DEFAULT '',
    UpdatedAt       DATETIME2               NULL,
    UpdatedBy       NVARCHAR(450)           NULL,
    SentAt          DATETIME2               NULL,
    OpenedAt        DATETIME2               NULL,
    CompletedAt     DATETIME2               NULL,
    DueDate         DATETIME2               NULL,
    CONSTRAINT UQ_Solicitudes_Number UNIQUE (RequestNumber),
    CONSTRAINT UQ_Solicitudes_Token  UNIQUE (ClientToken),
    CONSTRAINT FK_Solicitudes_Clientes FOREIGN KEY (ClientId) REFERENCES Clientes(Id)
);
CREATE INDEX IX_Solicitudes_Status     ON Solicitudes(Status) WHERE IsDeleted = 0;
CREATE INDEX IX_Solicitudes_CreatedAt  ON Solicitudes(CreatedAt);
CREATE INDEX IX_Solicitudes_ClientId   ON Solicitudes(ClientId);
GO

-- ============================================================
-- Tabla: DeclaracionesPersonaJuridica
-- ============================================================
IF OBJECT_ID('DeclaracionesPersonaJuridica', 'U') IS NULL
CREATE TABLE DeclaracionesPersonaJuridica (
    Id                          INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RequestId                   UNIQUEIDENTIFIER    NOT NULL,
    RUT                         NVARCHAR(20)        NOT NULL,
    BusinessName                NVARCHAR(500)       NOT NULL,
    Address                     NVARCHAR(500)       NOT NULL,
    City                        NVARCHAR(200)       NOT NULL,
    CountryOfIncorporation      NVARCHAR(200)       NOT NULL DEFAULT 'Chile',
    Phone                       NVARCHAR(50)            NULL,
    LegalRepresentativeIdNumber NVARCHAR(50)        NOT NULL,
    LegalRepresentativeName     NVARCHAR(500)       NOT NULL,
    EntityType                  INT                 NOT NULL,
    EntityTypeOther             NVARCHAR(200)           NULL,
    CreatedAt                   DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt                   DATETIME2               NULL,
    CONSTRAINT FK_DPJ_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_DPJ_RequestId UNIQUE (RequestId)
);
GO

-- ============================================================
-- Tabla: BeneficiariosFinales
-- ============================================================
IF OBJECT_ID('BeneficiariosFinales', 'U') IS NULL
CREATE TABLE BeneficiariosFinales (
    Id                      INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RequestId               UNIQUEIDENTIFIER    NOT NULL,
    IdNumber                NVARCHAR(50)        NOT NULL,
    FullName                NVARCHAR(500)       NOT NULL,
    Address                 NVARCHAR(500)           NULL,
    City                    NVARCHAR(200)           NULL,
    Country                 NVARCHAR(200)       NOT NULL DEFAULT 'Chile',
    ParticipationPercentage DECIMAL(5,2)        NOT NULL,
    IsBeneficialOwner       BIT                 NOT NULL DEFAULT 0,
    IsEffectiveControl      BIT                 NOT NULL DEFAULT 0,
    IsPEP                   BIT                 NOT NULL DEFAULT 0,
    PepDetail               NVARCHAR(1000)          NULL,
    SortOrder               INT                 NOT NULL DEFAULT 0,
    CreatedAt               DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt               DATETIME2               NULL,
    CONSTRAINT FK_BF_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE CASCADE
);
CREATE INDEX IX_BF_RequestId ON BeneficiariosFinales(RequestId);
GO

-- ============================================================
-- Tabla: ControlEfectivo
-- ============================================================
IF OBJECT_ID('ControlEfectivo', 'U') IS NULL
CREATE TABLE ControlEfectivo (
    Id                      INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RequestId               UNIQUEIDENTIFIER    NOT NULL,
    IdNumber                NVARCHAR(50)        NOT NULL,
    FullName                NVARCHAR(500)       NOT NULL,
    Address                 NVARCHAR(500)           NULL,
    City                    NVARCHAR(200)           NULL,
    Country                 NVARCHAR(200)       NOT NULL DEFAULT 'Chile',
    ParticipationPercentage DECIMAL(5,2)            NULL,
    IsPEP                   BIT                 NOT NULL DEFAULT 0,
    PepDetail               NVARCHAR(1000)          NULL,
    ControlDescription      NVARCHAR(1000)      NOT NULL,
    SortOrder               INT                 NOT NULL DEFAULT 0,
    CreatedAt               DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt               DATETIME2               NULL,
    CONSTRAINT FK_CE_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE CASCADE
);
GO

-- ============================================================
-- Tabla: DeclaracionesPEP
-- ============================================================
IF OBJECT_ID('DeclaracionesPEP', 'U') IS NULL
CREATE TABLE DeclaracionesPEP (
    Id                  INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RequestId           UNIQUEIDENTIFIER    NOT NULL,
    DeclarantName       NVARCHAR(500)       NOT NULL,
    IdNumber            NVARCHAR(50)        NOT NULL,
    Nationality         NVARCHAR(200)       NOT NULL,
    DeclaresPEP         BIT                 NOT NULL DEFAULT 0,
    PepName             NVARCHAR(500)           NULL,
    Institution         NVARCHAR(500)           NULL,
    PepReasonType       INT                     NULL,
    PepReasonOther      NVARCHAR(500)           NULL,
    VinculoType         NVARCHAR(500)           NULL,
    DeclarationDate     DATETIME2           NOT NULL,
    AcceptsUnderOath    BIT                 NOT NULL DEFAULT 0,
    SignatureFullName    NVARCHAR(500)       NOT NULL,
    SignatureIdNumber    NVARCHAR(50)        NOT NULL,
    SignatureDateTime   DATETIME2           NOT NULL,
    SignatureIpAddress  NVARCHAR(50)        NOT NULL,
    SignatureUserAgent  NVARCHAR(1000)          NULL,
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2               NULL,
    CONSTRAINT FK_DPEP_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_DPEP_RequestId UNIQUE (RequestId)
);
GO

-- ============================================================
-- Tabla: Declarantes
-- ============================================================
IF OBJECT_ID('Declarantes', 'U') IS NULL
CREATE TABLE Declarantes (
    Id                          INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RequestId                   UNIQUEIDENTIFIER    NOT NULL,
    NationalityType             INT                 NOT NULL,
    IdNumber                    NVARCHAR(50)        NOT NULL,
    FirstName                   NVARCHAR(200)       NOT NULL,
    LastName1                   NVARCHAR(200)       NOT NULL,
    LastName2                   NVARCHAR(200)           NULL,
    PlaceOfOrigin               NVARCHAR(200)           NULL,
    RelationshipWithLegalEntity NVARCHAR(500)       NOT NULL,
    DeclaresUnderOath           BIT                 NOT NULL DEFAULT 0,
    City                        NVARCHAR(200)       NOT NULL,
    DeclarationDate             DATETIME2           NOT NULL,
    CreatedAt                   DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt                   DATETIME2               NULL,
    CONSTRAINT FK_Dec_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_Dec_RequestId UNIQUE (RequestId)
);
GO

-- ============================================================
-- Tabla: Documentos
-- ============================================================
IF OBJECT_ID('Documentos', 'U') IS NULL
CREATE TABLE Documentos (
    Id                  INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RequestId           UNIQUEIDENTIFIER    NOT NULL,
    DocumentType        INT                 NOT NULL,
    DocumentTypeOther   NVARCHAR(200)           NULL,
    OriginalFileName    NVARCHAR(500)       NOT NULL,
    StoredFileName      NVARCHAR(500)       NOT NULL,
    StoragePath         NVARCHAR(1000)      NOT NULL,
    MimeType            NVARCHAR(200)       NOT NULL,
    FileSizeBytes       BIGINT              NOT NULL,
    IsActive            BIT                 NOT NULL DEFAULT 1,
    Version             INT                 NOT NULL DEFAULT 1,
    StorageProvider     INT                 NOT NULL DEFAULT 0,
    UploadedAt          DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UploadedByIp        NVARCHAR(50)        NOT NULL DEFAULT '',
    CONSTRAINT FK_Docs_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE CASCADE
);
CREATE INDEX IX_Documentos_RequestId  ON Documentos(RequestId);
CREATE INDEX IX_Documentos_Active     ON Documentos(RequestId, IsActive);
GO

-- ============================================================
-- Tabla: RegistrosAlmacenamiento
-- ============================================================
IF OBJECT_ID('RegistrosAlmacenamiento', 'U') IS NULL
CREATE TABLE RegistrosAlmacenamiento (
    Id              INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    DocumentId      INT                 NOT NULL,
    StorageProvider INT                 NOT NULL,
    StoragePath     NVARCHAR(2000)      NOT NULL,
    StorageKey      NVARCHAR(500)       NOT NULL,
    UploadedAt      DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    Metadata        NVARCHAR(MAX)           NULL,
    CONSTRAINT FK_RA_Documentos FOREIGN KEY (DocumentId) REFERENCES Documentos(Id) ON DELETE CASCADE
);
GO

-- ============================================================
-- Tabla: HistorialEstados
-- ============================================================
IF OBJECT_ID('HistorialEstados', 'U') IS NULL
CREATE TABLE HistorialEstados (
    Id          INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    RequestId   UNIQUEIDENTIFIER    NOT NULL,
    OldStatus   INT                 NOT NULL,
    NewStatus   INT                 NOT NULL,
    ChangedBy   NVARCHAR(500)       NOT NULL,
    Notes       NVARCHAR(2000)          NULL,
    ChangedAt   DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_HE_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE CASCADE
);
CREATE INDEX IX_HE_RequestId ON HistorialEstados(RequestId);
GO

-- ============================================================
-- Tabla: AuditLogs
-- ============================================================
IF OBJECT_ID('AuditLogs', 'U') IS NULL
CREATE TABLE AuditLogs (
    Id          BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    RequestId   UNIQUEIDENTIFIER         NULL,
    Action      NVARCHAR(200)        NOT NULL,
    EntityType  NVARCHAR(200)        NOT NULL,
    EntityId    NVARCHAR(200)            NULL,
    OldValues   NVARCHAR(MAX)            NULL,
    NewValues   NVARCHAR(MAX)            NULL,
    UserId      NVARCHAR(450)            NULL,
    UserName    NVARCHAR(500)            NULL,
    IpAddress   NVARCHAR(50)         NOT NULL DEFAULT '',
    UserAgent   NVARCHAR(1000)           NULL,
    CreatedAt   DATETIME2            NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_AL_Solicitudes FOREIGN KEY (RequestId) REFERENCES Solicitudes(Id) ON DELETE SET NULL
);
CREATE INDEX IX_AL_CreatedAt  ON AuditLogs(CreatedAt);
CREATE INDEX IX_AL_RequestId  ON AuditLogs(RequestId);
CREATE INDEX IX_AL_Action     ON AuditLogs(Action);
GO

-- ============================================================
-- Vista: Vista de solicitudes con estado legible
-- ============================================================
IF OBJECT_ID('vw_SolicitudesResumen', 'V') IS NOT NULL DROP VIEW vw_SolicitudesResumen;
GO
CREATE VIEW vw_SolicitudesResumen AS
SELECT
    s.Id,
    s.RequestNumber,
    s.Status,
    CASE s.Status
        WHEN 0 THEN 'Borrador'
        WHEN 1 THEN 'Enviada al Cliente'
        WHEN 2 THEN 'Abierta por Cliente'
        WHEN 3 THEN 'Completada por Cliente'
        WHEN 4 THEN 'En Revisión'
        WHEN 5 THEN 'Observada'
        WHEN 6 THEN 'Corregida por Cliente'
        WHEN 7 THEN 'Aprobada'
        WHEN 8 THEN 'Rechazada'
        WHEN 9 THEN 'Vencida'
        ELSE 'Desconocido'
    END AS StatusLabel,
    c.RUT AS ClienteRUT,
    c.BusinessName AS RazonSocial,
    s.CreatedAt,
    s.SentAt,
    s.CompletedAt,
    s.DueDate,
    (SELECT COUNT(*) FROM BeneficiariosFinales bf WHERE bf.RequestId = s.Id AND bf.IsPEP = 1) AS BeneficiariosPEP,
    (SELECT COUNT(*) FROM Documentos d WHERE d.RequestId = s.Id AND d.IsActive = 1) AS TotalDocumentos
FROM Solicitudes s
INNER JOIN Clientes c ON s.ClientId = c.Id
WHERE s.IsDeleted = 0;
GO

PRINT 'Base de datos FormulariosUAF creada correctamente.';
GO

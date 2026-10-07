-- RENOMBRADO a convención T_ / PREFIJO_DESCRIPCION_ENTIDAD (NO modifica datos, tipos ni relaciones)
-- Base: FormularioUAF / Qa2FormularioUAF (SQL Server 2016+). Generado desde la migración EF 20261001130256_ConvencionNombresT.
-- Todo corre en UNA transacción. Si cualquier paso falla, se hace ROLLBACK de todo, se muestra el error
-- y los pasos siguientes no se ejecutan (al final aparecerá además un error en el COMMIT: es esperado).
-- Idempotente: si la migración ya está (o no está) aplicada, no hace nada.
-- Requiere ventana sin usuarios: la versión antigua de la aplicación deja de funcionar apenas se renombra.

SET XACT_ABORT ON;
GO
BEGIN TRANSACTION;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        DROP INDEX [UserNameIndex] ON [Usuarios];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        DROP INDEX [RoleNameIndex] ON [Roles];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosTokens]', N'T_USUARIOTOKEN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosRoles]', N'T_USUARIOROL';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosLogins]', N'T_USUARIOLOGIN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosClaims]', N'T_USUARIOCLAIM';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios]', N'T_USUARIO';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses]', N'T_ANALISISCARPETATRIBUTARIA';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts]', N'T_ALERTACARPETATRIBUTARIA';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords]', N'T_FIRMA';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RolesClaims]', N'T_ROLCLAIM';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Roles]', N'T_ROL';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories]', N'T_HISTORIALESTADOSOLICITUD';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests]', N'T_SOLICITUD';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations]', N'T_DECLARACIONPEP';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications]', N'T_NOTIFICACION';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations]', N'T_DECLARACIONPERSONAJURIDICA';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords]', N'T_ALMACENAMIENTOARCHIVO';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers]', N'T_CONTROLADOREFECTIVO';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents]', N'T_DOCUMENTO';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons]', N'T_PERSONADECLARADA';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants]', N'T_DECLARANTE';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients]', N'T_CLIENTE';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners]', N'T_BENEFICIARIOFINAL';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs]', N'T_AUDITORIA';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOTOKEN].[Value]', N'ST_VALOR_USUARIOTOKEN', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOTOKEN].[Name]', N'ST_NOMBRE_USUARIOTOKEN', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOTOKEN].[LoginProvider]', N'ST_PROVEEDORLOGIN_USUARIOTOKEN', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOTOKEN].[UserId]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOROL].[RoleId]', N'IN_COD_ROL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOROL].[UserId]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOROL].[IX_UsuariosRoles_RoleId]', N'IX_T_USUARIOROL_IN_COD_ROL', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOLOGIN].[UserId]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOLOGIN].[ProviderDisplayName]', N'ST_NOMBREVISIBLEPROVEEDOR_USUARIOLOGIN', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOLOGIN].[ProviderKey]', N'ST_CLAVEPROVEEDOR_USUARIOLOGIN', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOLOGIN].[LoginProvider]', N'ST_PROVEEDORLOGIN_USUARIOLOGIN', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOLOGIN].[IX_UsuariosLogins_UserId]', N'IX_T_USUARIOLOGIN_IN_COD_USUARIO', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOCLAIM].[UserId]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOCLAIM].[ClaimValue]', N'ST_VALORCLAIM_USUARIOCLAIM', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOCLAIM].[ClaimType]', N'ST_TIPOCLAIM_USUARIOCLAIM', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOCLAIM].[Id]', N'IN_COD_USUARIOCLAIM', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOCLAIM].[IX_UsuariosClaims_UserId]', N'IX_T_USUARIOCLAIM_IN_COD_USUARIO', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[VendorCode]', N'ST_CODVENDEDOR_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[UserName]', N'ST_NOMBREUSUARIO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[TwoFactorEnabled]', N'BO_DOBLEFACTORHABILITADO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[SecurityStamp]', N'ST_SELLOSEGURIDAD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[Rut]', N'ST_RUT_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[PhoneNumberConfirmed]', N'BO_TELEFONOCONFIRMADO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[PhoneNumber]', N'ST_TELEFONO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[PasswordHash]', N'ST_HASHCONTRASENA_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[NormalizedUserName]', N'ST_NOMBREUSUARIONORMALIZADO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[NormalizedEmail]', N'ST_EMAILNORMALIZADO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[Nombre]', N'ST_NOMBRE_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[LockoutEnd]', N'DT_FECHAFINBLOQUEO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[LockoutEnabled]', N'BO_BLOQUEOHABILITADO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[LastLoginAt]', N'DT_FECHAULTIMOLOGIN_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[IsActive]', N'BO_ACTIVO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[FullName]', N'ST_NOMBRECOMPLETO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[EmailConfirmed]', N'BO_EMAILCONFIRMADO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[Email]', N'ST_EMAIL_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[CreatedAt]', N'DT_FECHACREACION_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[ConcurrencyStamp]', N'ST_SELLOCONCURRENCIA_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[ApellidoPaterno]', N'ST_APELLIDOPATERNO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[ApellidoMaterno]', N'ST_APELLIDOMATERNO_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[AccessFailedCount]', N'IN_CANTIDADACCESOSFALLIDOS_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO].[Id]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[WasReadable]', N'BO_LEGIBLE_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[RawText]', N'ST_TEXTOBRUTO_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[ExtractedRut]', N'ST_RUTEXTRAIDO_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[ExtractedLegalRep]', N'ST_REPRESENTANTELEGALEXTRAIDO_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[ExtractedIssuedDate]', N'ST_FECHAEMISIONEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[ExtractedBusinessName]', N'ST_RAZONSOCIALEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[ExtractedAddress]', N'ST_DIRECCIONEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[ExtractedActivity]', N'ST_ACTIVIDADEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[AnalyzedAt]', N'DT_FECHAANALISIS_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[Id]', N'IN_COD_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA].[IX_TaxFolderAnalyses_RequestId]', N'IX_T_ANALISISCARPETATRIBUTARIA_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA].[TaxFolderAnalysisId]', N'IN_COD_ANALISISCARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA].[Severity]', N'ST_SEVERIDAD_ALERTACARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA].[Message]', N'ST_MENSAJE_ALERTACARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA].[IsResolved]', N'BO_RESUELTA_ALERTACARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA].[AlertType]', N'IN_TIPO_ALERTACARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA].[Id]', N'IN_COD_ALERTACARPETATRIBUTARIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA].[IX_TaxFolderAlerts_TaxFolderAnalysisId]', N'IX_T_ALERTACARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[UserAgent]', N'ST_USERAGENT_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[UpdatedAt]', N'DT_FECHAMODIFICACION_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[Type]', N'IN_TIPO_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[Status]', N'IN_ESTADO_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[SignerName]', N'ST_NOMBREFIRMANTE_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[SignerIdNumber]', N'ST_NUMEROIDENTIFICACIONFIRMANTE_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[SignedPdfPath]', N'ST_RUTAPDFFIRMADO_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[SignedAt]', N'DT_FECHAFIRMA_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[SignatureUrl]', N'ST_URL_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[ProviderTransactionId]', N'ST_CODTRANSACCIONPROVEEDOR_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[IpAddress]', N'ST_DIRECCIONIP_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[FolioNumber]', N'ST_NUMEROFOLIO_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[DocumentHash]', N'ST_HASHDOCUMENTO_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[CreatedAt]', N'DT_FECHACREACION_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[Certificate]', N'ST_CERTIFICADO_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[Id]', N'IN_COD_FIRMA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA].[IX_SignatureRecords_RequestId]', N'IX_T_FIRMA_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROLCLAIM].[RoleId]', N'IN_COD_ROL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROLCLAIM].[ClaimValue]', N'ST_VALORCLAIM_ROLCLAIM', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROLCLAIM].[ClaimType]', N'ST_TIPOCLAIM_ROLCLAIM', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROLCLAIM].[Id]', N'IN_COD_ROLCLAIM', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROLCLAIM].[IX_RolesClaims_RoleId]', N'IX_T_ROLCLAIM_IN_COD_ROL', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROL].[NormalizedName]', N'ST_NOMBRENORMALIZADO_ROL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROL].[Name]', N'ST_NOMBRE_ROL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROL].[ConcurrencyStamp]', N'ST_SELLOCONCURRENCIA_ROL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROL].[Id]', N'IN_COD_ROL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[OldStatus]', N'IN_ESTADOANTERIOR_HISTORIALESTADOSOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[Notes]', N'ST_NOTAS_HISTORIALESTADOSOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[NewStatus]', N'IN_ESTADONUEVO_HISTORIALESTADOSOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[ChangedBy]', N'ST_CAMBIADOPOR_HISTORIALESTADOSOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[ChangedAt]', N'DT_FECHACAMBIO_HISTORIALESTADOSOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[Id]', N'IN_COD_HISTORIALESTADOSOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD].[IX_RequestStatusHistories_RequestId]', N'IX_T_HISTORIALESTADOSOLICITUD_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[VendorUserId]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[UpdatedBy]', N'ST_MODIFICADOPOR_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[UpdatedAt]', N'DT_FECHAMODIFICACION_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[TokenExpiry]', N'DT_FECHAVENCIMIENTOTOKEN_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[Status]', N'IN_ESTADO_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[SentAt]', N'DT_FECHAENVIO_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[RequestType]', N'IN_TIPO_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[RequestNumber]', N'ST_NUMERO_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[OpenedAt]', N'DT_FECHAAPERTURA_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[NetcarBusinessNumber]', N'ST_NUMERONEGOCIONETCAR_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[IsDeleted]', N'BO_ELIMINADO_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[InternalNotes]', N'ST_NOTASINTERNAS_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[DueDate]', N'DT_FECHAVENCIMIENTO_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[CurrentStep]', N'IN_PASOACTUAL_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[CreatedBy]', N'ST_CREADOPOR_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[CreatedAt]', N'DT_FECHACREACION_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[CompletedAt]', N'DT_FECHACOMPLETADA_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[ClientToken]', N'ST_TOKENCLIENTE_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[ClientPhone]', N'ST_TELEFONOCLIENTE_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[ClientId]', N'IN_COD_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[ClientEmail]', N'ST_EMAILCLIENTE_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[Id]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[IX_Requests_VendorUserId]', N'IX_T_SOLICITUD_IN_COD_USUARIO', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[IX_Requests_Status]', N'IX_T_SOLICITUD_IN_ESTADO_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[IX_Requests_RequestNumber]', N'IX_T_SOLICITUD_ST_NUMERO_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[IX_Requests_CreatedAt]', N'IX_T_SOLICITUD_DT_FECHACREACION_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[IX_Requests_ClientToken]', N'IX_T_SOLICITUD_ST_TOKENCLIENTE_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD].[IX_Requests_ClientId]', N'IX_T_SOLICITUD_IN_COD_CLIENTE', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[VinculoType]', N'ST_TIPOVINCULO_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[UpdatedAt]', N'DT_FECHAMODIFICACION_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[SignatureUserAgent]', N'ST_USERAGENTFIRMA_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[SignatureIpAddress]', N'ST_DIRECCIONIPFIRMA_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[SignatureIdNumber]', N'ST_NUMEROIDENTIFICACIONFIRMA_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[SignatureFullName]', N'ST_NOMBRECOMPLETOFIRMA_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[SignatureDateTime]', N'DT_FECHAFIRMA_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[PepReasonType]', N'IN_MOTIVOPEP_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[PepReasonOther]', N'ST_MOTIVOPEPOTRO_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[PepName]', N'ST_NOMBREPEP_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[Nationality]', N'ST_NACIONALIDAD_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[Institution]', N'ST_INSTITUCION_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[IdNumber]', N'ST_NUMEROIDENTIFICACION_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[DeclaresPEP]', N'BO_DECLARAPEP_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[DeclarationDate]', N'DT_FECHADECLARACION_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[DeclarantName]', N'ST_NOMBREDECLARANTE_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[CreatedAt]', N'DT_FECHACREACION_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[AcceptsUnderOath]', N'BO_ACEPTABAJOJURAMENTO_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[Id]', N'IN_COD_DECLARACIONPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP].[IX_PepDeclarations_RequestId]', N'IX_T_DECLARACIONPEP_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[UserId]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[Type]', N'IN_TIPO_NOTIFICACION', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[ReadAt]', N'DT_FECHALECTURA_NOTIFICACION', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[Message]', N'ST_MENSAJE_NOTIFICACION', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[IsRead]', N'BO_LEIDA_NOTIFICACION', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[CreatedAt]', N'DT_FECHACREACION_NOTIFICACION', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[Id]', N'IN_COD_NOTIFICACION', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[IX_Notifications_UserId_IsRead]', N'IX_T_NOTIFICACION_IN_COD_USUARIO_BO_LEIDA_NOTIFICACION', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[IX_Notifications_UserId]', N'IX_T_NOTIFICACION_IN_COD_USUARIO', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION].[IX_Notifications_CreatedAt]', N'IX_T_NOTIFICACION_DT_FECHACREACION_NOTIFICACION', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[UpdatedAt]', N'DT_FECHAMODIFICACION_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[RUT]', N'ST_RUT_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[Phone]', N'ST_TELEFONO_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[LegalRepresentativeName]', N'ST_NOMBREREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[LegalRepresentativeIdNumber]', N'ST_NUMEROIDENTIFICACIONREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[EntityTypeOther]', N'ST_TIPOENTIDADOTRO_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[EntityType]', N'IN_TIPOENTIDAD_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[CreatedAt]', N'DT_FECHACREACION_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[CountryOfIncorporation]', N'ST_PAISCONSTITUCION_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[City]', N'ST_CIUDAD_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[BusinessName]', N'ST_RAZONSOCIAL_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[Address]', N'ST_DIRECCION_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[Id]', N'IN_COD_DECLARACIONPERSONAJURIDICA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA].[IX_LegalEntityDeclarations_RequestId]', N'IX_T_DECLARACIONPERSONAJURIDICA_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[UploadedAt]', N'DT_FECHACARGA_ALMACENAMIENTOARCHIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[StorageProvider]', N'IN_PROVEEDORALMACENAMIENTO_ALMACENAMIENTOARCHIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[StoragePath]', N'ST_RUTAALMACENAMIENTO_ALMACENAMIENTOARCHIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[StorageKey]', N'ST_CLAVEALMACENAMIENTO_ALMACENAMIENTOARCHIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[Metadata]', N'ST_METADATOS_ALMACENAMIENTOARCHIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[DocumentId]', N'IN_COD_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[Id]', N'IN_COD_ALMACENAMIENTOARCHIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO].[IX_FileStorageRecords_DocumentId]', N'IX_T_ALMACENAMIENTOARCHIVO_IN_COD_DOCUMENTO', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[UpdatedAt]', N'DT_FECHAMODIFICACION_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[SortOrder]', N'IN_ORDEN_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[PepDetail]', N'ST_DETALLEPEP_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[ParticipationPercentage]', N'NU_PORCENTAJEPARTICIPACION_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[IsPEP]', N'BO_ESPEP_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[IdNumber]', N'ST_NUMEROIDENTIFICACION_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[FullName]', N'ST_NOMBRECOMPLETO_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[CreatedAt]', N'DT_FECHACREACION_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[Country]', N'ST_PAIS_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[ControlDescription]', N'ST_DESCRIPCIONCONTROL_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[City]', N'ST_CIUDAD_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[Address]', N'ST_DIRECCION_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[Id]', N'IN_COD_CONTROLADOREFECTIVO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO].[IX_EffectiveControllers_RequestId]', N'IX_T_CONTROLADOREFECTIVO_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[Version]', N'IN_VERSION_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[UploadedByIp]', N'ST_IPCARGA_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[UploadedAt]', N'DT_FECHACARGA_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[StoredFileName]', N'ST_NOMBREARCHIVOALMACENADO_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[StorageProvider]', N'IN_PROVEEDORALMACENAMIENTO_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[StoragePath]', N'ST_RUTAALMACENAMIENTO_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[OriginalFileName]', N'ST_NOMBREARCHIVOORIGINAL_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[MimeType]', N'ST_TIPOMIME_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[IsActive]', N'BO_ACTIVO_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[FileSizeBytes]', N'IN_TAMANOBYTES_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[DocumentTypeOther]', N'ST_TIPOOTRO_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[DocumentType]', N'IN_TIPO_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[Id]', N'IN_COD_DOCUMENTO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO].[IX_Documents_RequestId]', N'IX_T_DOCUMENTO_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[UpdatedAt]', N'DT_FECHAMODIFICACION_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[SortOrder]', N'IN_ORDEN_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[RelationshipTypeOther]', N'ST_TIPORELACIONOTRO_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[RelationshipType]', N'IN_TIPORELACION_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[PepTypeName]', N'ST_NOMBRETIPOPEP_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[PepType]', N'IN_TIPOPEP_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[PepRelationship]', N'ST_RELACIONPEP_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[PepPosition]', N'ST_CARGOPEP_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[PepObservation]', N'ST_OBSERVACIONPEP_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[PepInstitution]', N'ST_INSTITUCIONPEP_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[ParticipationPercentage]', N'NU_PORCENTAJEPARTICIPACION_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[IsPEP]', N'BO_ESPEP_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[IsEffectiveController]', N'BO_ESCONTROLADOREFECTIVO_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[IdNumber]', N'ST_NUMEROIDENTIFICACION_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[HasMinTenPercentParticipation]', N'BO_PARTICIPACIONMINDIEZPORCIENTO_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[HandlesCashOrFunds]', N'BO_MANEJAEFECTIVOFONDOS_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[FullName]', N'ST_NOMBRECOMPLETO_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[EffectiveControlDescription]', N'ST_DESCRIPCIONCONTROLEFECTIVO_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[CreatedAt]', N'DT_FECHACREACION_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[Country]', N'ST_PAIS_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[City]', N'ST_CIUDAD_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[Address]', N'ST_DIRECCION_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[Id]', N'IN_COD_PERSONADECLARADA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA].[IX_DeclaredPersons_RequestId]', N'IX_T_PERSONADECLARADA_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[UpdatedAt]', N'DT_FECHAMODIFICACION_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[SignatureUserAgent]', N'ST_USERAGENTFIRMA_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[SignatureIpAddress]', N'ST_DIRECCIONIPFIRMA_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[SignatureIdNumber]', N'ST_NUMEROIDENTIFICACIONFIRMA_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[SignatureFullName]', N'ST_NOMBRECOMPLETOFIRMA_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[SignatureDateTime]', N'DT_FECHAFIRMA_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[RelationshipWithLegalEntity]', N'ST_RELACIONPERSONAJURIDICA_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[PlaceOfOrigin]', N'ST_LUGARORIGEN_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[Phone]', N'ST_TELEFONO_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[NationalityType]', N'IN_TIPONACIONALIDAD_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[LastName2]', N'ST_APELLIDOMATERNO_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[LastName1]', N'ST_APELLIDOPATERNO_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[IdNumber]', N'ST_NUMEROIDENTIFICACION_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[FirstName]', N'ST_NOMBRE_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[Email]', N'ST_EMAIL_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[DeclaresUnderOath]', N'BO_DECLARABAJOJURAMENTO_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[DeclarationDate]', N'DT_FECHADECLARACION_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[CreatedAt]', N'DT_FECHACREACION_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[City]', N'ST_CIUDAD_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[Id]', N'IN_COD_DECLARANTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE].[IX_Declarants_RequestId]', N'IX_T_DECLARANTE_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[UpdatedBy]', N'ST_MODIFICADOPOR_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[UpdatedAt]', N'DT_FECHAMODIFICACION_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[RUT]', N'ST_RUT_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[IsActive]', N'BO_ACTIVO_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[CreatedBy]', N'ST_CREADOPOR_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[CreatedAt]', N'DT_FECHACREACION_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[BusinessName]', N'ST_RAZONSOCIAL_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[Id]', N'IN_COD_CLIENTE', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE].[IX_Clients_RUT]', N'IX_T_CLIENTE_ST_RUT_CLIENTE', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[UpdatedAt]', N'DT_FECHAMODIFICACION_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[SortOrder]', N'IN_ORDEN_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[PepDetail]', N'ST_DETALLEPEP_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[ParticipationPercentage]', N'NU_PORCENTAJEPARTICIPACION_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[IsPEP]', N'BO_ESPEP_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[IsEffectiveControl]', N'BO_ESCONTROLEFECTIVO_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[IsBeneficialOwner]', N'BO_ESBENEFICIARIOFINAL_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[IdNumber]', N'ST_NUMEROIDENTIFICACION_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[FullName]', N'ST_NOMBRECOMPLETO_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[CreatedAt]', N'DT_FECHACREACION_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[Country]', N'ST_PAIS_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[City]', N'ST_CIUDAD_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[Address]', N'ST_DIRECCION_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[Id]', N'IN_COD_BENEFICIARIOFINAL', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL].[IX_BeneficialOwners_RequestId]', N'IX_T_BENEFICIARIOFINAL_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[UserName]', N'ST_NOMBREUSUARIO_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[UserId]', N'IN_COD_USUARIO', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[UserAgent]', N'ST_USERAGENT_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[RequestId]', N'IN_COD_SOLICITUD', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[OldValues]', N'ST_VALORESANTERIORES_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[NewValues]', N'ST_VALORESNUEVOS_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[IpAddress]', N'ST_DIRECCIONIP_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[EntityType]', N'ST_TIPOENTIDAD_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[EntityId]', N'ST_CODENTIDAD_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[CreatedAt]', N'DT_FECHACREACION_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[Action]', N'ST_ACCION_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[Id]', N'IN_COD_AUDITORIA', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[IX_AuditLogs_RequestId]', N'IX_T_AUDITORIA_IN_COD_SOLICITUD', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA].[IX_AuditLogs_CreatedAt]', N'IX_T_AUDITORIA_DT_FECHACREACION_AUDITORIA', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [T_USUARIO] ([ST_NOMBREUSUARIONORMALIZADO_USUARIO]) WHERE [ST_NOMBREUSUARIONORMALIZADO_USUARIO] IS NOT NULL');
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [T_ROL] ([ST_NOMBRENORMALIZADO_ROL]) WHERE [ST_NOMBRENORMALIZADO_ROL] IS NOT NULL');
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_UsuariosTokens]', N'PK_T_USUARIOTOKEN', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_UsuariosRoles]', N'PK_T_USUARIOROL', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_UsuariosLogins]', N'PK_T_USUARIOLOGIN', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_UsuariosClaims]', N'PK_T_USUARIOCLAIM', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_Usuarios]', N'PK_T_USUARIO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_TaxFolderAnalyses]', N'PK_T_ANALISISCARPETATRIBUTARIA', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_TaxFolderAlerts]', N'PK_T_ALERTACARPETATRIBUTARIA', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_SignatureRecords]', N'PK_T_FIRMA', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_RolesClaims]', N'PK_T_ROLCLAIM', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_Roles]', N'PK_T_ROL', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_RequestStatusHistories]', N'PK_T_HISTORIALESTADOSOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_Requests]', N'PK_T_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_PepDeclarations]', N'PK_T_DECLARACIONPEP', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_Notifications]', N'PK_T_NOTIFICACION', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_LegalEntityDeclarations]', N'PK_T_DECLARACIONPERSONAJURIDICA', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_FileStorageRecords]', N'PK_T_ALMACENAMIENTOARCHIVO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_EffectiveControllers]', N'PK_T_CONTROLADOREFECTIVO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_Documents]', N'PK_T_DOCUMENTO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_DeclaredPersons]', N'PK_T_PERSONADECLARADA', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_Declarants]', N'PK_T_DECLARANTE', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_Clients]', N'PK_T_CLIENTE', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_BeneficialOwners]', N'PK_T_BENEFICIARIOFINAL', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_AuditLogs]', N'PK_T_AUDITORIA', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_AuditLogs_Requests_RequestId]', N'FK_T_AUDITORIA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_BeneficialOwners_Requests_RequestId]', N'FK_T_BENEFICIARIOFINAL_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_Declarants_Requests_RequestId]', N'FK_T_DECLARANTE_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_DeclaredPersons_Requests_RequestId]', N'FK_T_PERSONADECLARADA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_Documents_Requests_RequestId]', N'FK_T_DOCUMENTO_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_EffectiveControllers_Requests_RequestId]', N'FK_T_CONTROLADOREFECTIVO_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_FileStorageRecords_Documents_DocumentId]', N'FK_T_ALMACENAMIENTOARCHIVO_T_DOCUMENTO_IN_COD_DOCUMENTO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_LegalEntityDeclarations_Requests_RequestId]', N'FK_T_DECLARACIONPERSONAJURIDICA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_PepDeclarations_Requests_RequestId]', N'FK_T_DECLARACIONPEP_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_Requests_Clients_ClientId]', N'FK_T_SOLICITUD_T_CLIENTE_IN_COD_CLIENTE', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_Requests_Usuarios_VendorUserId]', N'FK_T_SOLICITUD_T_USUARIO_IN_COD_USUARIO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_RequestStatusHistories_Requests_RequestId]', N'FK_T_HISTORIALESTADOSOLICITUD_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_RolesClaims_Roles_RoleId]', N'FK_T_ROLCLAIM_T_ROL_IN_COD_ROL', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_SignatureRecords_Requests_RequestId]', N'FK_T_FIRMA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_TaxFolderAlerts_TaxFolderAnalyses_TaxFolderAnalysisId]', N'FK_T_ALERTACARPETATRIBUTARIA_T_ANALISISCARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_TaxFolderAnalyses_Requests_RequestId]', N'FK_T_ANALISISCARPETATRIBUTARIA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_UsuariosClaims_Usuarios_UserId]', N'FK_T_USUARIOCLAIM_T_USUARIO_IN_COD_USUARIO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_UsuariosLogins_Usuarios_UserId]', N'FK_T_USUARIOLOGIN_T_USUARIO_IN_COD_USUARIO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_UsuariosRoles_Usuarios_UserId]', N'FK_T_USUARIOROL_T_USUARIO_IN_COD_USUARIO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_UsuariosRoles_Roles_RoleId]', N'FK_T_USUARIOROL_T_ROL_IN_COD_ROL', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_UsuariosTokens_Usuarios_UserId]', N'FK_T_USUARIOTOKEN_T_USUARIO_IN_COD_USUARIO', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
        VALUES (N'20261001130256_ConvencionNombresT', N'8.0.10');
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

COMMIT;
GO


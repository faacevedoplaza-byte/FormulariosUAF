-- ROLLBACK: devuelve todos los nombres originales (revierte 01_Renombrado_ConvencionT.sql)
-- Base: FormularioUAF / Qa2FormularioUAF (SQL Server 2016+). Generado desde la migración EF 20261001130256_ConvencionNombresT.
-- Todo corre en UNA transacción. Si cualquier paso falla, se hace ROLLBACK de todo, se muestra el error
-- y los pasos siguientes no se ejecutan (al final aparecerá además un error en el COMMIT: es esperado).
-- Idempotente: si la migración ya está (o no está) aplicada, no hace nada.
-- Requiere ventana sin usuarios: la versión antigua de la aplicación deja de funcionar apenas se renombra.

SET XACT_ABORT ON;
GO
BEGIN TRANSACTION;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        DROP INDEX [UserNameIndex] ON [T_USUARIO];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        DROP INDEX [RoleNameIndex] ON [T_ROL];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOTOKEN]', N'UsuariosTokens';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOROL]', N'UsuariosRoles';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOLOGIN]', N'UsuariosLogins';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIOCLAIM]', N'UsuariosClaims';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_USUARIO]', N'Usuarios';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_SOLICITUD]', N'Requests';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROLCLAIM]', N'RolesClaims';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ROL]', N'Roles';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_PERSONADECLARADA]', N'DeclaredPersons';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_NOTIFICACION]', N'Notifications';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_HISTORIALESTADOSOLICITUD]', N'RequestStatusHistories';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_FIRMA]', N'SignatureRecords';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DOCUMENTO]', N'Documents';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARANTE]', N'Declarants';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPERSONAJURIDICA]', N'LegalEntityDeclarations';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_DECLARACIONPEP]', N'PepDeclarations';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CONTROLADOREFECTIVO]', N'EffectiveControllers';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_CLIENTE]', N'Clients';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_BENEFICIARIOFINAL]', N'BeneficialOwners';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_AUDITORIA]', N'AuditLogs';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ANALISISCARPETATRIBUTARIA]', N'TaxFolderAnalyses';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALMACENAMIENTOARCHIVO]', N'FileStorageRecords';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[T_ALERTACARPETATRIBUTARIA]', N'TaxFolderAlerts';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosTokens].[ST_VALOR_USUARIOTOKEN]', N'Value', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosTokens].[ST_NOMBRE_USUARIOTOKEN]', N'Name', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosTokens].[ST_PROVEEDORLOGIN_USUARIOTOKEN]', N'LoginProvider', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosTokens].[IN_COD_USUARIO]', N'UserId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosRoles].[IN_COD_ROL]', N'RoleId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosRoles].[IN_COD_USUARIO]', N'UserId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosRoles].[IX_T_USUARIOROL_IN_COD_ROL]', N'IX_UsuariosRoles_RoleId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosLogins].[ST_NOMBREVISIBLEPROVEEDOR_USUARIOLOGIN]', N'ProviderDisplayName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosLogins].[IN_COD_USUARIO]', N'UserId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosLogins].[ST_CLAVEPROVEEDOR_USUARIOLOGIN]', N'ProviderKey', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosLogins].[ST_PROVEEDORLOGIN_USUARIOLOGIN]', N'LoginProvider', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosLogins].[IX_T_USUARIOLOGIN_IN_COD_USUARIO]', N'IX_UsuariosLogins_UserId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosClaims].[ST_VALORCLAIM_USUARIOCLAIM]', N'ClaimValue', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosClaims].[ST_TIPOCLAIM_USUARIOCLAIM]', N'ClaimType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosClaims].[IN_COD_USUARIO]', N'UserId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosClaims].[IN_COD_USUARIOCLAIM]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[UsuariosClaims].[IX_T_USUARIOCLAIM_IN_COD_USUARIO]', N'IX_UsuariosClaims_UserId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_TELEFONO_USUARIO]', N'PhoneNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_SELLOSEGURIDAD_USUARIO]', N'SecurityStamp', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_SELLOCONCURRENCIA_USUARIO]', N'ConcurrencyStamp', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_RUT_USUARIO]', N'Rut', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_NOMBRE_USUARIO]', N'Nombre', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_NOMBREUSUARIO_USUARIO]', N'UserName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_NOMBREUSUARIONORMALIZADO_USUARIO]', N'NormalizedUserName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_NOMBRECOMPLETO_USUARIO]', N'FullName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_HASHCONTRASENA_USUARIO]', N'PasswordHash', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_EMAIL_USUARIO]', N'Email', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_EMAILNORMALIZADO_USUARIO]', N'NormalizedEmail', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_CODVENDEDOR_USUARIO]', N'VendorCode', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_APELLIDOPATERNO_USUARIO]', N'ApellidoPaterno', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[ST_APELLIDOMATERNO_USUARIO]', N'ApellidoMaterno', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[IN_CANTIDADACCESOSFALLIDOS_USUARIO]', N'AccessFailedCount', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[DT_FECHAULTIMOLOGIN_USUARIO]', N'LastLoginAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[DT_FECHAFINBLOQUEO_USUARIO]', N'LockoutEnd', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[DT_FECHACREACION_USUARIO]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[BO_TELEFONOCONFIRMADO_USUARIO]', N'PhoneNumberConfirmed', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[BO_EMAILCONFIRMADO_USUARIO]', N'EmailConfirmed', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[BO_DOBLEFACTORHABILITADO_USUARIO]', N'TwoFactorEnabled', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[BO_BLOQUEOHABILITADO_USUARIO]', N'LockoutEnabled', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[BO_ACTIVO_USUARIO]', N'IsActive', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Usuarios].[IN_COD_USUARIO]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_TOKENCLIENTE_SOLICITUD]', N'ClientToken', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_TELEFONOCLIENTE_SOLICITUD]', N'ClientPhone', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_NUMERO_SOLICITUD]', N'RequestNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_NUMERONEGOCIONETCAR_SOLICITUD]', N'NetcarBusinessNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_NOTASINTERNAS_SOLICITUD]', N'InternalNotes', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_MODIFICADOPOR_SOLICITUD]', N'UpdatedBy', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_EMAILCLIENTE_SOLICITUD]', N'ClientEmail', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[ST_CREADOPOR_SOLICITUD]', N'CreatedBy', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IN_TIPO_SOLICITUD]', N'RequestType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IN_PASOACTUAL_SOLICITUD]', N'CurrentStep', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IN_ESTADO_SOLICITUD]', N'Status', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IN_COD_USUARIO]', N'VendorUserId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IN_COD_CLIENTE]', N'ClientId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[DT_FECHAVENCIMIENTO_SOLICITUD]', N'DueDate', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[DT_FECHAVENCIMIENTOTOKEN_SOLICITUD]', N'TokenExpiry', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[DT_FECHAMODIFICACION_SOLICITUD]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[DT_FECHAENVIO_SOLICITUD]', N'SentAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[DT_FECHACREACION_SOLICITUD]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[DT_FECHACOMPLETADA_SOLICITUD]', N'CompletedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[DT_FECHAAPERTURA_SOLICITUD]', N'OpenedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[BO_ELIMINADO_SOLICITUD]', N'IsDeleted', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IN_COD_SOLICITUD]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IX_T_SOLICITUD_ST_TOKENCLIENTE_SOLICITUD]', N'IX_Requests_ClientToken', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IX_T_SOLICITUD_ST_NUMERO_SOLICITUD]', N'IX_Requests_RequestNumber', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IX_T_SOLICITUD_IN_ESTADO_SOLICITUD]', N'IX_Requests_Status', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IX_T_SOLICITUD_IN_COD_USUARIO]', N'IX_Requests_VendorUserId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IX_T_SOLICITUD_IN_COD_CLIENTE]', N'IX_Requests_ClientId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Requests].[IX_T_SOLICITUD_DT_FECHACREACION_SOLICITUD]', N'IX_Requests_CreatedAt', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RolesClaims].[ST_VALORCLAIM_ROLCLAIM]', N'ClaimValue', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RolesClaims].[ST_TIPOCLAIM_ROLCLAIM]', N'ClaimType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RolesClaims].[IN_COD_ROL]', N'RoleId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RolesClaims].[IN_COD_ROLCLAIM]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RolesClaims].[IX_T_ROLCLAIM_IN_COD_ROL]', N'IX_RolesClaims_RoleId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Roles].[ST_SELLOCONCURRENCIA_ROL]', N'ConcurrencyStamp', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Roles].[ST_NOMBRE_ROL]', N'Name', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Roles].[ST_NOMBRENORMALIZADO_ROL]', N'NormalizedName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Roles].[IN_COD_ROL]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_TIPORELACIONOTRO_PERSONADECLARADA]', N'RelationshipTypeOther', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_RELACIONPEP_PERSONADECLARADA]', N'PepRelationship', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_PAIS_PERSONADECLARADA]', N'Country', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_OBSERVACIONPEP_PERSONADECLARADA]', N'PepObservation', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_NUMEROIDENTIFICACION_PERSONADECLARADA]', N'IdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_NOMBRETIPOPEP_PERSONADECLARADA]', N'PepTypeName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_NOMBRECOMPLETO_PERSONADECLARADA]', N'FullName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_INSTITUCIONPEP_PERSONADECLARADA]', N'PepInstitution', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_DIRECCION_PERSONADECLARADA]', N'Address', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_DESCRIPCIONCONTROLEFECTIVO_PERSONADECLARADA]', N'EffectiveControlDescription', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_CIUDAD_PERSONADECLARADA]', N'City', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[ST_CARGOPEP_PERSONADECLARADA]', N'PepPosition', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[NU_PORCENTAJEPARTICIPACION_PERSONADECLARADA]', N'ParticipationPercentage', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[IN_TIPORELACION_PERSONADECLARADA]', N'RelationshipType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[IN_TIPOPEP_PERSONADECLARADA]', N'PepType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[IN_ORDEN_PERSONADECLARADA]', N'SortOrder', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[DT_FECHAMODIFICACION_PERSONADECLARADA]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[DT_FECHACREACION_PERSONADECLARADA]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[BO_PARTICIPACIONMINDIEZPORCIENTO_PERSONADECLARADA]', N'HasMinTenPercentParticipation', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[BO_MANEJAEFECTIVOFONDOS_PERSONADECLARADA]', N'HandlesCashOrFunds', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[BO_ESPEP_PERSONADECLARADA]', N'IsPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[BO_ESCONTROLADOREFECTIVO_PERSONADECLARADA]', N'IsEffectiveController', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[IN_COD_PERSONADECLARADA]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[DeclaredPersons].[IX_T_PERSONADECLARADA_IN_COD_SOLICITUD]', N'IX_DeclaredPersons_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[ST_MENSAJE_NOTIFICACION]', N'Message', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[IN_TIPO_NOTIFICACION]', N'Type', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[IN_COD_USUARIO]', N'UserId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[DT_FECHALECTURA_NOTIFICACION]', N'ReadAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[DT_FECHACREACION_NOTIFICACION]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[BO_LEIDA_NOTIFICACION]', N'IsRead', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[IN_COD_NOTIFICACION]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[IX_T_NOTIFICACION_IN_COD_USUARIO_BO_LEIDA_NOTIFICACION]', N'IX_Notifications_UserId_IsRead', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[IX_T_NOTIFICACION_IN_COD_USUARIO]', N'IX_Notifications_UserId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Notifications].[IX_T_NOTIFICACION_DT_FECHACREACION_NOTIFICACION]', N'IX_Notifications_CreatedAt', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[ST_NOTAS_HISTORIALESTADOSOLICITUD]', N'Notes', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[ST_CAMBIADOPOR_HISTORIALESTADOSOLICITUD]', N'ChangedBy', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[IN_ESTADONUEVO_HISTORIALESTADOSOLICITUD]', N'NewStatus', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[IN_ESTADOANTERIOR_HISTORIALESTADOSOLICITUD]', N'OldStatus', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[DT_FECHACAMBIO_HISTORIALESTADOSOLICITUD]', N'ChangedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[IN_COD_HISTORIALESTADOSOLICITUD]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[RequestStatusHistories].[IX_T_HISTORIALESTADOSOLICITUD_IN_COD_SOLICITUD]', N'IX_RequestStatusHistories_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_USERAGENT_FIRMA]', N'UserAgent', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_URL_FIRMA]', N'SignatureUrl', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_RUTAPDFFIRMADO_FIRMA]', N'SignedPdfPath', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_NUMEROIDENTIFICACIONFIRMANTE_FIRMA]', N'SignerIdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_NUMEROFOLIO_FIRMA]', N'FolioNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_NOMBREFIRMANTE_FIRMA]', N'SignerName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_HASHDOCUMENTO_FIRMA]', N'DocumentHash', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_DIRECCIONIP_FIRMA]', N'IpAddress', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_CODTRANSACCIONPROVEEDOR_FIRMA]', N'ProviderTransactionId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[ST_CERTIFICADO_FIRMA]', N'Certificate', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[IN_TIPO_FIRMA]', N'Type', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[IN_ESTADO_FIRMA]', N'Status', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[DT_FECHAMODIFICACION_FIRMA]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[DT_FECHAFIRMA_FIRMA]', N'SignedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[DT_FECHACREACION_FIRMA]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[IN_COD_FIRMA]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[SignatureRecords].[IX_T_FIRMA_IN_COD_SOLICITUD]', N'IX_SignatureRecords_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[ST_TIPOOTRO_DOCUMENTO]', N'DocumentTypeOther', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[ST_TIPOMIME_DOCUMENTO]', N'MimeType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[ST_RUTAALMACENAMIENTO_DOCUMENTO]', N'StoragePath', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[ST_NOMBREARCHIVOORIGINAL_DOCUMENTO]', N'OriginalFileName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[ST_NOMBREARCHIVOALMACENADO_DOCUMENTO]', N'StoredFileName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[ST_IPCARGA_DOCUMENTO]', N'UploadedByIp', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[IN_VERSION_DOCUMENTO]', N'Version', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[IN_TIPO_DOCUMENTO]', N'DocumentType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[IN_TAMANOBYTES_DOCUMENTO]', N'FileSizeBytes', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[IN_PROVEEDORALMACENAMIENTO_DOCUMENTO]', N'StorageProvider', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[DT_FECHACARGA_DOCUMENTO]', N'UploadedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[BO_ACTIVO_DOCUMENTO]', N'IsActive', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[IN_COD_DOCUMENTO]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Documents].[IX_T_DOCUMENTO_IN_COD_SOLICITUD]', N'IX_Documents_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_USERAGENTFIRMA_DECLARANTE]', N'SignatureUserAgent', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_TELEFONO_DECLARANTE]', N'Phone', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_RELACIONPERSONAJURIDICA_DECLARANTE]', N'RelationshipWithLegalEntity', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_NUMEROIDENTIFICACION_DECLARANTE]', N'IdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_NUMEROIDENTIFICACIONFIRMA_DECLARANTE]', N'SignatureIdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_NOMBRE_DECLARANTE]', N'FirstName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_NOMBRECOMPLETOFIRMA_DECLARANTE]', N'SignatureFullName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_LUGARORIGEN_DECLARANTE]', N'PlaceOfOrigin', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_EMAIL_DECLARANTE]', N'Email', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_DIRECCIONIPFIRMA_DECLARANTE]', N'SignatureIpAddress', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_CIUDAD_DECLARANTE]', N'City', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_APELLIDOPATERNO_DECLARANTE]', N'LastName1', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[ST_APELLIDOMATERNO_DECLARANTE]', N'LastName2', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[IN_TIPONACIONALIDAD_DECLARANTE]', N'NationalityType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[DT_FECHAMODIFICACION_DECLARANTE]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[DT_FECHAFIRMA_DECLARANTE]', N'SignatureDateTime', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[DT_FECHADECLARACION_DECLARANTE]', N'DeclarationDate', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[DT_FECHACREACION_DECLARANTE]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[BO_DECLARABAJOJURAMENTO_DECLARANTE]', N'DeclaresUnderOath', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[IN_COD_DECLARANTE]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Declarants].[IX_T_DECLARANTE_IN_COD_SOLICITUD]', N'IX_Declarants_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_TIPOENTIDADOTRO_DECLARACIONPERSONAJURIDICA]', N'EntityTypeOther', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_TELEFONO_DECLARACIONPERSONAJURIDICA]', N'Phone', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_RUT_DECLARACIONPERSONAJURIDICA]', N'RUT', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_RAZONSOCIAL_DECLARACIONPERSONAJURIDICA]', N'BusinessName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_PAISCONSTITUCION_DECLARACIONPERSONAJURIDICA]', N'CountryOfIncorporation', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_NUMEROIDENTIFICACIONREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA]', N'LegalRepresentativeIdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_NOMBREREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA]', N'LegalRepresentativeName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_DIRECCION_DECLARACIONPERSONAJURIDICA]', N'Address', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[ST_CIUDAD_DECLARACIONPERSONAJURIDICA]', N'City', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[IN_TIPOENTIDAD_DECLARACIONPERSONAJURIDICA]', N'EntityType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[DT_FECHAMODIFICACION_DECLARACIONPERSONAJURIDICA]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[DT_FECHACREACION_DECLARACIONPERSONAJURIDICA]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[IN_COD_DECLARACIONPERSONAJURIDICA]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[LegalEntityDeclarations].[IX_T_DECLARACIONPERSONAJURIDICA_IN_COD_SOLICITUD]', N'IX_LegalEntityDeclarations_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_USERAGENTFIRMA_DECLARACIONPEP]', N'SignatureUserAgent', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_TIPOVINCULO_DECLARACIONPEP]', N'VinculoType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_NUMEROIDENTIFICACION_DECLARACIONPEP]', N'IdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_NUMEROIDENTIFICACIONFIRMA_DECLARACIONPEP]', N'SignatureIdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_NOMBREPEP_DECLARACIONPEP]', N'PepName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_NOMBREDECLARANTE_DECLARACIONPEP]', N'DeclarantName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_NOMBRECOMPLETOFIRMA_DECLARACIONPEP]', N'SignatureFullName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_NACIONALIDAD_DECLARACIONPEP]', N'Nationality', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_MOTIVOPEPOTRO_DECLARACIONPEP]', N'PepReasonOther', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_INSTITUCION_DECLARACIONPEP]', N'Institution', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[ST_DIRECCIONIPFIRMA_DECLARACIONPEP]', N'SignatureIpAddress', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[IN_MOTIVOPEP_DECLARACIONPEP]', N'PepReasonType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[DT_FECHAMODIFICACION_DECLARACIONPEP]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[DT_FECHAFIRMA_DECLARACIONPEP]', N'SignatureDateTime', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[DT_FECHADECLARACION_DECLARACIONPEP]', N'DeclarationDate', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[DT_FECHACREACION_DECLARACIONPEP]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[BO_DECLARAPEP_DECLARACIONPEP]', N'DeclaresPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[BO_ACEPTABAJOJURAMENTO_DECLARACIONPEP]', N'AcceptsUnderOath', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[IN_COD_DECLARACIONPEP]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[PepDeclarations].[IX_T_DECLARACIONPEP_IN_COD_SOLICITUD]', N'IX_PepDeclarations_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[ST_PAIS_CONTROLADOREFECTIVO]', N'Country', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[ST_NUMEROIDENTIFICACION_CONTROLADOREFECTIVO]', N'IdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[ST_NOMBRECOMPLETO_CONTROLADOREFECTIVO]', N'FullName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[ST_DIRECCION_CONTROLADOREFECTIVO]', N'Address', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[ST_DETALLEPEP_CONTROLADOREFECTIVO]', N'PepDetail', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[ST_DESCRIPCIONCONTROL_CONTROLADOREFECTIVO]', N'ControlDescription', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[ST_CIUDAD_CONTROLADOREFECTIVO]', N'City', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[NU_PORCENTAJEPARTICIPACION_CONTROLADOREFECTIVO]', N'ParticipationPercentage', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[IN_ORDEN_CONTROLADOREFECTIVO]', N'SortOrder', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[DT_FECHAMODIFICACION_CONTROLADOREFECTIVO]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[DT_FECHACREACION_CONTROLADOREFECTIVO]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[BO_ESPEP_CONTROLADOREFECTIVO]', N'IsPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[IN_COD_CONTROLADOREFECTIVO]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[EffectiveControllers].[IX_T_CONTROLADOREFECTIVO_IN_COD_SOLICITUD]', N'IX_EffectiveControllers_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[ST_RUT_CLIENTE]', N'RUT', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[ST_RAZONSOCIAL_CLIENTE]', N'BusinessName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[ST_MODIFICADOPOR_CLIENTE]', N'UpdatedBy', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[ST_CREADOPOR_CLIENTE]', N'CreatedBy', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[DT_FECHAMODIFICACION_CLIENTE]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[DT_FECHACREACION_CLIENTE]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[BO_ACTIVO_CLIENTE]', N'IsActive', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[IN_COD_CLIENTE]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[Clients].[IX_T_CLIENTE_ST_RUT_CLIENTE]', N'IX_Clients_RUT', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[ST_PAIS_BENEFICIARIOFINAL]', N'Country', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[ST_NUMEROIDENTIFICACION_BENEFICIARIOFINAL]', N'IdNumber', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[ST_NOMBRECOMPLETO_BENEFICIARIOFINAL]', N'FullName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[ST_DIRECCION_BENEFICIARIOFINAL]', N'Address', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[ST_DETALLEPEP_BENEFICIARIOFINAL]', N'PepDetail', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[ST_CIUDAD_BENEFICIARIOFINAL]', N'City', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[NU_PORCENTAJEPARTICIPACION_BENEFICIARIOFINAL]', N'ParticipationPercentage', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[IN_ORDEN_BENEFICIARIOFINAL]', N'SortOrder', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[DT_FECHAMODIFICACION_BENEFICIARIOFINAL]', N'UpdatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[DT_FECHACREACION_BENEFICIARIOFINAL]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[BO_ESPEP_BENEFICIARIOFINAL]', N'IsPEP', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[BO_ESCONTROLEFECTIVO_BENEFICIARIOFINAL]', N'IsEffectiveControl', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[BO_ESBENEFICIARIOFINAL_BENEFICIARIOFINAL]', N'IsBeneficialOwner', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[IN_COD_BENEFICIARIOFINAL]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[BeneficialOwners].[IX_T_BENEFICIARIOFINAL_IN_COD_SOLICITUD]', N'IX_BeneficialOwners_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_VALORESNUEVOS_AUDITORIA]', N'NewValues', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_VALORESANTERIORES_AUDITORIA]', N'OldValues', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_USERAGENT_AUDITORIA]', N'UserAgent', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_TIPOENTIDAD_AUDITORIA]', N'EntityType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_NOMBREUSUARIO_AUDITORIA]', N'UserName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_DIRECCIONIP_AUDITORIA]', N'IpAddress', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_CODENTIDAD_AUDITORIA]', N'EntityId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[ST_ACCION_AUDITORIA]', N'Action', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[IN_COD_USUARIO]', N'UserId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[DT_FECHACREACION_AUDITORIA]', N'CreatedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[IN_COD_AUDITORIA]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[IX_T_AUDITORIA_IN_COD_SOLICITUD]', N'IX_AuditLogs_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[AuditLogs].[IX_T_AUDITORIA_DT_FECHACREACION_AUDITORIA]', N'IX_AuditLogs_CreatedAt', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[ST_TEXTOBRUTO_ANALISISCARPETATRIBUTARIA]', N'RawText', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[ST_RUTEXTRAIDO_ANALISISCARPETATRIBUTARIA]', N'ExtractedRut', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[ST_REPRESENTANTELEGALEXTRAIDO_ANALISISCARPETATRIBUTARIA]', N'ExtractedLegalRep', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[ST_RAZONSOCIALEXTRAIDA_ANALISISCARPETATRIBUTARIA]', N'ExtractedBusinessName', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[ST_FECHAEMISIONEXTRAIDA_ANALISISCARPETATRIBUTARIA]', N'ExtractedIssuedDate', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[ST_DIRECCIONEXTRAIDA_ANALISISCARPETATRIBUTARIA]', N'ExtractedAddress', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[ST_ACTIVIDADEXTRAIDA_ANALISISCARPETATRIBUTARIA]', N'ExtractedActivity', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[IN_COD_SOLICITUD]', N'RequestId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[DT_FECHAANALISIS_ANALISISCARPETATRIBUTARIA]', N'AnalyzedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[BO_LEGIBLE_ANALISISCARPETATRIBUTARIA]', N'WasReadable', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[IN_COD_ANALISISCARPETATRIBUTARIA]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAnalyses].[IX_T_ANALISISCARPETATRIBUTARIA_IN_COD_SOLICITUD]', N'IX_TaxFolderAnalyses_RequestId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[ST_RUTAALMACENAMIENTO_ALMACENAMIENTOARCHIVO]', N'StoragePath', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[ST_METADATOS_ALMACENAMIENTOARCHIVO]', N'Metadata', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[ST_CLAVEALMACENAMIENTO_ALMACENAMIENTOARCHIVO]', N'StorageKey', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[IN_PROVEEDORALMACENAMIENTO_ALMACENAMIENTOARCHIVO]', N'StorageProvider', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[IN_COD_DOCUMENTO]', N'DocumentId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[DT_FECHACARGA_ALMACENAMIENTOARCHIVO]', N'UploadedAt', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[IN_COD_ALMACENAMIENTOARCHIVO]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[FileStorageRecords].[IX_T_ALMACENAMIENTOARCHIVO_IN_COD_DOCUMENTO]', N'IX_FileStorageRecords_DocumentId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts].[ST_SEVERIDAD_ALERTACARPETATRIBUTARIA]', N'Severity', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts].[ST_MENSAJE_ALERTACARPETATRIBUTARIA]', N'Message', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts].[IN_TIPO_ALERTACARPETATRIBUTARIA]', N'AlertType', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts].[IN_COD_ANALISISCARPETATRIBUTARIA]', N'TaxFolderAnalysisId', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts].[BO_RESUELTA_ALERTACARPETATRIBUTARIA]', N'IsResolved', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts].[IN_COD_ALERTACARPETATRIBUTARIA]', N'Id', N'COLUMN';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[TaxFolderAlerts].[IX_T_ALERTACARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA]', N'IX_TaxFolderAlerts_TaxFolderAnalysisId', N'INDEX';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Usuarios] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_USUARIOTOKEN]', N'PK_UsuariosTokens', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_USUARIOROL]', N'PK_UsuariosRoles', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_USUARIOLOGIN]', N'PK_UsuariosLogins', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_USUARIOCLAIM]', N'PK_UsuariosClaims', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_USUARIO]', N'PK_Usuarios', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_ANALISISCARPETATRIBUTARIA]', N'PK_TaxFolderAnalyses', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_ALERTACARPETATRIBUTARIA]', N'PK_TaxFolderAlerts', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_FIRMA]', N'PK_SignatureRecords', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_ROLCLAIM]', N'PK_RolesClaims', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_ROL]', N'PK_Roles', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_HISTORIALESTADOSOLICITUD]', N'PK_RequestStatusHistories', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_SOLICITUD]', N'PK_Requests', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_DECLARACIONPEP]', N'PK_PepDeclarations', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_NOTIFICACION]', N'PK_Notifications', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_DECLARACIONPERSONAJURIDICA]', N'PK_LegalEntityDeclarations', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_ALMACENAMIENTOARCHIVO]', N'PK_FileStorageRecords', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_CONTROLADOREFECTIVO]', N'PK_EffectiveControllers', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_DOCUMENTO]', N'PK_Documents', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_PERSONADECLARADA]', N'PK_DeclaredPersons', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_DECLARANTE]', N'PK_Declarants', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_CLIENTE]', N'PK_Clients', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_BENEFICIARIOFINAL]', N'PK_BeneficialOwners', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[PK_T_AUDITORIA]', N'PK_AuditLogs', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_AUDITORIA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_AuditLogs_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_BENEFICIARIOFINAL_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_BeneficialOwners_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_DECLARANTE_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_Declarants_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_PERSONADECLARADA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_DeclaredPersons_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_DOCUMENTO_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_Documents_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_CONTROLADOREFECTIVO_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_EffectiveControllers_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_ALMACENAMIENTOARCHIVO_T_DOCUMENTO_IN_COD_DOCUMENTO]', N'FK_FileStorageRecords_Documents_DocumentId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_DECLARACIONPERSONAJURIDICA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_LegalEntityDeclarations_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_DECLARACIONPEP_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_PepDeclarations_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_SOLICITUD_T_CLIENTE_IN_COD_CLIENTE]', N'FK_Requests_Clients_ClientId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_SOLICITUD_T_USUARIO_IN_COD_USUARIO]', N'FK_Requests_Usuarios_VendorUserId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_HISTORIALESTADOSOLICITUD_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_RequestStatusHistories_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_ROLCLAIM_T_ROL_IN_COD_ROL]', N'FK_RolesClaims_Roles_RoleId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_FIRMA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_SignatureRecords_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_ALERTACARPETATRIBUTARIA_T_ANALISISCARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA]', N'FK_TaxFolderAlerts_TaxFolderAnalyses_TaxFolderAnalysisId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_ANALISISCARPETATRIBUTARIA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_TaxFolderAnalyses_Requests_RequestId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_USUARIOCLAIM_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosClaims_Usuarios_UserId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_USUARIOLOGIN_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosLogins_Usuarios_UserId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_USUARIOROL_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosRoles_Usuarios_UserId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_USUARIOROL_T_ROL_IN_COD_ROL]', N'FK_UsuariosRoles_Roles_RoleId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        EXEC sp_rename N'[dbo].[FK_T_USUARIOTOKEN_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosTokens_Usuarios_UserId', N'OBJECT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

IF @@TRANCOUNT > 0 AND EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261001130256_ConvencionNombresT'
)
BEGIN
    BEGIN TRY
        DELETE FROM [__EFMigrationsHistory]
        WHERE [MigrationId] = N'20261001130256_ConvencionNombresT';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

COMMIT;
GO


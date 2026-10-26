/* ============================================================================
   Cuenta SQL de SOLO LECTURA para la importación piloto de vendedores
   (Services/ImportarVendedoresNetcar.cs, cadena 'NetcarReadOnly').

   Servidor: 10.100.84.4   Base: netcarsiglo21com
   Ejecutar como sa. Solo crea un login/usuario de seguridad: NO toca datos ni tablas.
   SQL Server 2016 RTM: sin CREATE OR ALTER.

   Permiso mínimo: SELECT SOLO de las columnas que usa la importación en
   dbo.T_ACC_USUARIO. Sin INSERT/UPDATE/DELETE, sin acceso a otras tablas.
   Reemplazar <CLAVE_SEGURA> antes de ejecutar. NO guardar la clave en el repo.
   ============================================================================ */

USE master;
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'uaf_import_lectura')
    CREATE LOGIN uaf_import_lectura WITH PASSWORD = '<CLAVE_SEGURA>',
        DEFAULT_DATABASE = netcarsiglo21com, CHECK_POLICY = ON;
DENY VIEW ANY DATABASE TO uaf_import_lectura;
GO

USE netcarsiglo21com;
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'uaf_import_lectura')
    CREATE USER uaf_import_lectura FOR LOGIN uaf_import_lectura;
GO

GRANT SELECT ON dbo.T_ACC_USUARIO (
    ST_ID_USUARIO,
    ST_RUT_USUARIO,
    ST_NOMBRE_USUARIO,
    ST_APEPAT_USUARIO,
    ST_EMAIL_USUARIO,
    ST_PASSWORD_USUARIO,
    IN_ESVENDEDOR_USUARIO,
    DT_FECHAVIGENCIA_USUARIO
) TO uaf_import_lectura;
GO

/* Al terminar la importación definitiva, quitar el acceso:
USE netcarsiglo21com; DROP USER uaf_import_lectura;
USE master;           DROP LOGIN uaf_import_lectura;
*/

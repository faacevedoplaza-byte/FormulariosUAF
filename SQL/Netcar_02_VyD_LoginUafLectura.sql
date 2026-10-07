/*
    FormulariosUAF — usuario de solo lectura uaf_lectura en Netcar VyD.

    Ejecutar SOLO en 10.100.84.5 (el login ya existe en 10.100.84.4 / Siglo 21), ANTES de
    Netcar_01_sp_UAF_VehiculoCotizacion.sql en netcarvaldelarzecom2.

    Mismo esquema que en Siglo 21: login SQL sin roles; en la base solo CONNECT y luego
    EXECUTE sobre los SP sp_UAF_* (lo otorga el script 01). No recibe db_datareader ni SELECT.
    Misma contraseña que en Siglo 21 (cadena NetcarVyDConnection de appsettings.json).

    Rollback:
      USE netcarvaldelarzecom2; DROP USER uaf_lectura;
      USE master; DROP LOGIN uaf_lectura;
*/
USE master;
GO
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'uaf_lectura')
    CREATE LOGIN uaf_lectura
        WITH PASSWORD = 'VYD.,vistauaf',
             DEFAULT_DATABASE = netcarvaldelarzecom2,
             CHECK_POLICY = ON,
             CHECK_EXPIRATION = OFF;
GO

USE netcarvaldelarzecom2;
GO
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'uaf_lectura')
    CREATE USER uaf_lectura FOR LOGIN uaf_lectura WITH DEFAULT_SCHEMA = dbo;
GO

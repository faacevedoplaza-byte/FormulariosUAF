-- ============================================================================
-- 00_ConteoPrevio_ConvencionT.sql  (solo lectura)
-- Ejecutar ANTES de 01_Renombrado_ConvencionT.sql y guardar el resultado.
-- Después, la sección 11 de 02_Validacion_ConvencionT.sql debe dar exactamente lo mismo.
-- ============================================================================
SET NOCOUNT ON;
SELECT N'Requests' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[Requests]
UNION ALL SELECT N'Clients' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[Clients]
UNION ALL SELECT N'Usuarios' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[Usuarios]
UNION ALL SELECT N'Roles' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[Roles]
UNION ALL SELECT N'UsuariosRoles' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[UsuariosRoles]
UNION ALL SELECT N'UsuariosClaims' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[UsuariosClaims]
UNION ALL SELECT N'UsuariosLogins' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[UsuariosLogins]
UNION ALL SELECT N'UsuariosTokens' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[UsuariosTokens]
UNION ALL SELECT N'RolesClaims' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[RolesClaims]
UNION ALL SELECT N'LegalEntityDeclarations' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[LegalEntityDeclarations]
UNION ALL SELECT N'DeclaredPersons' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[DeclaredPersons]
UNION ALL SELECT N'Declarants' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[Declarants]
UNION ALL SELECT N'Documents' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[Documents]
UNION ALL SELECT N'FileStorageRecords' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[FileStorageRecords]
UNION ALL SELECT N'RequestStatusHistories' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[RequestStatusHistories]
UNION ALL SELECT N'AuditLogs' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[AuditLogs]
UNION ALL SELECT N'Notifications' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[Notifications]
UNION ALL SELECT N'SignatureRecords' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[SignatureRecords]
UNION ALL SELECT N'TaxFolderAnalyses' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[TaxFolderAnalyses]
UNION ALL SELECT N'TaxFolderAlerts' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[TaxFolderAlerts]
UNION ALL SELECT N'BeneficialOwners' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[BeneficialOwners]
UNION ALL SELECT N'EffectiveControllers' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[EffectiveControllers]
UNION ALL SELECT N'PepDeclarations' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[PepDeclarations];
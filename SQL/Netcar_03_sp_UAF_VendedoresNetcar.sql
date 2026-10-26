/*
    FormulariosUAF — importar vendedores de Netcar a /Admin/Usuarios/ImportarNetcar.

    Ejecutar en AMBAS bases Netcar (mismo script), con sqlcmd -I (QUOTED_IDENTIFIER ON):
      - 10.100.84.4  netcarsiglo21com       (Siglo 21)
      - 10.100.84.5  netcarvaldelarzecom2   (VyD)

    La app (usuario uaf_lectura) recibe solo EXECUTE sobre estos SP, no SELECT sobre T_ACC_USUARIO.
      - sp_UAF_VendedoresNetcar: vendedores vigentes, SIN contraseña (para el listado).
      - sp_UAF_ClaveVendedorNetcar: contraseña de UN vendedor vigente; la app la usa solo al crear el usuario
        (Identity la guarda con hash) y no la muestra ni la registra.
    Columnas y vigencia tomadas de netcar/website/acceso/accEditusrtable.aspx.vb
    (IN_ESVENDEDOR_USUARIO = 1, vigente = DT_FECHAVIGENCIA_USUARIO > GETDATE()).

    Rollback:
      DROP PROCEDURE dbo.sp_UAF_VendedoresNetcar;
      DROP PROCEDURE dbo.sp_UAF_ClaveVendedorNetcar;
*/
IF OBJECT_ID('dbo.sp_UAF_VendedoresNetcar', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UAF_VendedoresNetcar;
GO
IF OBJECT_ID('dbo.sp_UAF_ClaveVendedorNetcar', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UAF_ClaveVendedorNetcar;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE dbo.sp_UAF_VendedoresNetcar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT U.IN_COD_USUARIO           AS IdUsuario,
           U.ST_ID_USUARIO            AS Login,
           U.ST_ABREVIACION_USUARIO   AS Abreviacion,
           U.ST_RUT_USUARIO           AS Rut,
           U.ST_NOMBRE_USUARIO        AS Nombre,
           U.ST_APEPAT_USUARIO        AS ApellidoPaterno,
           U.ST_APEMAT_USUARIO        AS ApellidoMaterno,
           U.ST_EMAIL_USUARIO         AS Email,
           U.ST_FONO1_USUARIO         AS Telefono,
           U.DT_FECHAVIGENCIA_USUARIO AS Vigencia
    FROM dbo.T_ACC_USUARIO U
    WHERE U.IN_ESVENDEDOR_USUARIO = 1
      AND U.DT_FECHAVIGENCIA_USUARIO > GETDATE()
    ORDER BY U.ST_APEPAT_USUARIO, U.ST_NOMBRE_USUARIO;
END
GO

CREATE PROCEDURE dbo.sp_UAF_ClaveVendedorNetcar
    @IdUsuario int
AS
BEGIN
    SET NOCOUNT ON;

    -- Solo vendedores vigentes: no sirve para leer la clave de otros perfiles.
    SELECT U.ST_PASSWORD_USUARIO AS Clave
    FROM dbo.T_ACC_USUARIO U
    WHERE U.IN_COD_USUARIO = @IdUsuario
      AND U.IN_ESVENDEDOR_USUARIO = 1
      AND U.DT_FECHAVIGENCIA_USUARIO > GETDATE();
END
GO

GRANT EXECUTE ON dbo.sp_UAF_VendedoresNetcar TO uaf_lectura;
GRANT EXECUTE ON dbo.sp_UAF_ClaveVendedorNetcar TO uaf_lectura;
GO

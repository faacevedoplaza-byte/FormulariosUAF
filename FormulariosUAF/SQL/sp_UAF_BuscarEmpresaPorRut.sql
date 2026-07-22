/* SP búsqueda de empresa por RUT — base netcarsiglo21com (10.100.84.4)
   Lee T_CLI_CLIENTE (IN_TIPO_CLIENTE='3') + domicilio y comuna/ciudad.
   - Dirección:  T_GEN_DIRECCION.IN_CODENTIDAD_DIRECCION = T_CLI_CLIENTE.IN_COD_CLIENTE
                 calle en ST_DESC_DIRECCION.
   - Ciudad/Comuna: la dirección guarda la jerarquía región/comuna en
                 IN_VALORNIVEL1..4REGIONCOMUNA_DIRECCION (ids de detalle de
                 clasificación #2). Se toma el nivel MÁS PROFUNDO con dato
                 (0/NULL = vacío) y se traduce con T_GEN_DETALLECLASIFICACION.
   SQL Server 2016 RTM -> ALTER (no CREATE OR ALTER).
   >>> ABRIR ESTE ARCHIVO EN SSMS (Archivo > Abrir > Archivo) Y EJECUTAR (F5).
       No copiar/pegar; y no dejar otras queries en esta pestaña. <<< */
USE [netcarsiglo21com]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_UAF_BuscarEmpresaPorRut]
    @RutEmpresa VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        c.ST_RUT_CLIENTE AS RutEmpresa,
        c.ST_NOMBRE_CLIENTE AS RazonSocial,
        d.ST_DESC_DIRECCION AS Domicilio,
        dc.ST_DESC_DETALLECLASIFICACION AS Ciudad,
        NULL AS PaisConstitucion,
        c.ST_FONO1_CLIENTE AS Telefono,
        NULL AS TipoEntidad,
        c.ST_RUTREPRESENTANTE_CLIENTE AS RutRepresentanteLegal,
        c.ST_REPRESENTANTELEGAL_CLIENTE AS NombreRepresentanteLegal,
        c.ST_EMAIL_CLIENTE AS Email
    FROM T_CLI_CLIENTE c
    OUTER APPLY (
        SELECT TOP 1
            dd.ST_DESC_DIRECCION,
            dd.IN_VALORNIVEL1REGIONCOMUNA_DIRECCION AS N1,
            dd.IN_VALORNIVEL2REGIONCOMUNA_DIRECCION AS N2,
            dd.IN_VALORNIVEL3REGIONCOMUNA_DIRECCION AS N3,
            dd.IN_VALORNIVEL4REGIONCOMUNA_DIRECCION AS N4
        FROM T_GEN_DIRECCION dd
        WHERE dd.IN_CODENTIDAD_DIRECCION = c.IN_COD_CLIENTE
          AND dd.ST_DESC_DIRECCION IS NOT NULL
        ORDER BY dd.IN_VIGENCIA_DIRECCION DESC, dd.IN_COD_DIRECCION DESC
    ) d
    LEFT JOIN T_GEN_DETALLECLASIFICACION dc
        ON dc.IN_COD_CLASIFICACION = 2
       AND dc.IN_COD_DETALLECLASIFICACION =
           COALESCE(NULLIF(d.N4, 0), NULLIF(d.N3, 0), NULLIF(d.N2, 0), NULLIF(d.N1, 0))
    WHERE c.IN_TIPO_CLIENTE = '3'
      AND REPLACE(REPLACE(c.ST_RUT_CLIENTE, '.', ''), '-', '') = REPLACE(REPLACE(@RutEmpresa, '.', ''), '-', '');
END
GO

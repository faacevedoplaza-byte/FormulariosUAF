/*
    FormulariosUAF — vehículo de la cotización Netcar de una operación RegCheq.

    Ejecutar en AMBAS bases Netcar (mismo script):
      - 10.100.84.4  netcarsiglo21com       (Siglo 21)
      - 10.100.84.5  netcarvaldelarzecom2   (VyD)  → antes, Netcar_02_VyD_LoginUafLectura.sql

    Solo lectura. La app (usuario uaf_lectura) recibe únicamente EXECUTE sobre este SP, igual que
    sp_UAF_BuscarEmpresaPorRut: no puede leer las tablas directamente (encadenamiento de propiedad dbo).
    Las uniones replican netcar/website/tareasprogramadas/query/cotlstnegocio.sql.

    Compatibilidad 100 (SQL 2008): sin STRING_SPLIT ni TRY_CONVERT; la lista se separa con XML.

    Rollback:
      DROP PROCEDURE dbo.sp_UAF_VehiculoCotizacion;
*/
IF OBJECT_ID('dbo.sp_UAF_VehiculoCotizacion', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UAF_VehiculoCotizacion;
GO

-- Obligatorio: el SP guarda estas opciones al crearse y los métodos XML (nodes/value) exigen ON.
-- sqlcmd sin -I crea con QUOTED_IDENTIFIER OFF y el SP fallaría al ejecutarse.
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE dbo.sp_UAF_VehiculoCotizacion
    -- N° de cotización (IN_COD_COTIZACIONVEHICULO) separados por coma. Máximo 200.
    @Cotizaciones varchar(4000)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ids TABLE (Id int PRIMARY KEY);

    -- Solo dígitos: cualquier otro carácter descarta el valor (y evita XML inválido).
    IF @Cotizaciones IS NULL OR @Cotizaciones LIKE '%[^0-9,]%'
        RETURN;

    DECLARE @xml xml = CAST('<i>' + REPLACE(@Cotizaciones, ',', '</i><i>') + '</i>' AS xml);

    INSERT @ids (Id)
    SELECT DISTINCT TOP (200) CAST(x.v AS int)
    FROM (SELECT t.n.value('.', 'varchar(20)') AS v FROM @xml.nodes('/i') AS t(n)) x
    WHERE LEN(x.v) BETWEEN 1 AND 9;

    SELECT COT.IN_COD_COTIZACIONVEHICULO           AS Cotizacion,
           MA.ST_DESC_DETALLECLASIFICACION          AS Marca,
           MO.ST_DESC_DETALLECLASIFICACION          AS Modelo,
           VE.ST_DESC_DETALLECLASIFICACION          AS Version,
           V.IN_AGNOFABRICACION_VEHICULO            AS Anio,
           CO.ST_DESC_COLOR                         AS Color,
           V.ST_PLACAPATENTE_VEHICULO               AS Patente,
           V.ST_NCHASIS_VEHICULO                    AS Chasis,
           COT.IN_ESNUEVO_VEHICULO                  AS EsNuevo,
           COT.IN_PRECIOVEHICULO_COTIZACIONVEHICULO AS Precio,
           E.ST_DESC_ESTADOCOTIZACION               AS Estado,
           U.ST_ABREVIACION_USUARIO                 AS Vendedor,
           COT.DT_FECHAINGRESO_COTIZACIONVEHICULO   AS Fecha
    FROM @ids i
    JOIN dbo.T_VEH_COTIZACIONVEHICULO COT ON COT.IN_COD_COTIZACIONVEHICULO = i.Id
    LEFT JOIN dbo.T_GEN_DETALLECLASIFICACION MA ON MA.IN_COD_DETALLECLASIFICACION = COT.IN_VALORNIVEL1VEHICULO_COTIZACIONVEHICULO
    LEFT JOIN dbo.T_GEN_DETALLECLASIFICACION MO ON MO.IN_COD_DETALLECLASIFICACION = COT.IN_VALORNIVEL2VEHICULO_COTIZACIONVEHICULO
    LEFT JOIN dbo.T_GEN_DETALLECLASIFICACION VE ON VE.IN_COD_DETALLECLASIFICACION = COT.IN_VALORNIVEL3VEHICULO_COTIZACIONVEHICULO
    LEFT JOIN dbo.T_VEH_VEHICULO V ON V.IN_COD_VEHICULO = COT.IN_COD_VEHICULO AND COT.IN_COD_VEHICULO > 0
    LEFT JOIN dbo.T_VEH_COLOR CO ON CO.IN_COD_COLOR = V.IN_COD_COLOR
    LEFT JOIN dbo.T_VEH_ESTADOCOTIZACION E ON E.IN_COD_ESTADOCOTIZACION = COT.IN_ESTADO_COTIZACIONVEHICULO
    LEFT JOIN dbo.T_ACC_USUARIO U ON U.IN_COD_USUARIO = COT.IN_COD_USUARIO;
END
GO

-- Solo EXECUTE para la app (el usuario uaf_lectura debe existir en esta base).
GRANT EXECUTE ON dbo.sp_UAF_VehiculoCotizacion TO uaf_lectura;
GO

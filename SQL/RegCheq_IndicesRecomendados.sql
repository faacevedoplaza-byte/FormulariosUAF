-- ============================================================================
-- RegCheq_IndicesRecomendados.sql
-- Base: IntegracionesNetCar (10.100.84.10), esquema DB_REGCHEQ.
-- Crea índices no agrupados para las búsquedas de la vista /Regcheq (FormulariosUAF)
-- y para las búsquedas que hace la integración RegcheqController después de cada INSERT.
-- No modifica datos ni columnas. Idempotente: solo crea los que no existen.
-- Tablas pequeñas (< 100 MB): cada índice tarda segundos. Ejecutar cuando la integración
-- no esté corriendo (Standard Edition no permite ONLINE = ON; el bloqueo es breve).
-- Rollback: DROP INDEX <nombre> ON <tabla> (ver al final).
-- ============================================================================
SET NOCOUNT ON;

-- 1) Asociados por operación: la vista los busca por operación; la integración busca
--    el ID recién insertado por operación + DNI + ficha.
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ASOCNAT_OPERACION' AND object_id = OBJECT_ID('DB_REGCHEQ.T_OPERACION_ASOCIADOS_NATURAL'))
    CREATE NONCLUSTERED INDEX IX_ASOCNAT_OPERACION
        ON DB_REGCHEQ.T_OPERACION_ASOCIADOS_NATURAL (IN_COD_OPERACION_NATURAL)
        INCLUDE (ST_DNI, ST_FICHAID);

-- 2) Fichas por asociado: vista (nombre del cliente) e integración (ID de la ficha recién insertada).
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_FICHANAT_ASOCIADO' AND object_id = OBJECT_ID('DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_NATURAL'))
    CREATE NONCLUSTERED INDEX IX_FICHANAT_ASOCIADO
        ON DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_NATURAL (IN_COD_ASOCIADO_NATURAL);

-- 3) Listas por ficha: detalle de la operación y filtro "Lista PEP" (pepChile / BT_COINCIDENCE).
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_LISTANAT_FICHA' AND object_id = OBJECT_ID('DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_LISTAS_NATURAL'))
    CREATE NONCLUSTERED INDEX IX_LISTANAT_FICHA
        ON DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_LISTAS_NATURAL (IN_COD_FICHA_NATURAL)
        INCLUDE (ST_LISTATIPO, BT_COINCIDENCE);

-- 4) Operaciones por fecha: orden y paginación del listado, filtro por rango de fechas.
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_OPNAT_CREACION' AND object_id = OBJECT_ID('DB_REGCHEQ.T_OPERACION_NATURAL'))
    CREATE NONCLUSTERED INDEX IX_OPNAT_CREACION
        ON DB_REGCHEQ.T_OPERACION_NATURAL (DT_CREATED_AT DESC, IN_COD_OPERACION_NATURAL DESC);

-- 5) Operaciones por código: búsqueda por código en la vista; la integración busca
--    el ID recién insertado por ST_ID + IN_CODIGO.
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_OPNAT_CODIGO' AND object_id = OBJECT_ID('DB_REGCHEQ.T_OPERACION_NATURAL'))
    CREATE NONCLUSTERED INDEX IX_OPNAT_CODIGO
        ON DB_REGCHEQ.T_OPERACION_NATURAL (IN_CODIGO) INCLUDE (ST_ID);

-- Verificación
SELECT t.name AS Tabla, i.name AS Indice, i.type_desc AS Tipo
FROM sys.indexes i JOIN sys.tables t ON t.object_id = i.object_id
WHERE SCHEMA_NAME(t.schema_id) = 'DB_REGCHEQ' AND i.name LIKE 'IX[_]%'
ORDER BY t.name, i.name;

/* ROLLBACK (eliminar los índices creados):
DROP INDEX IX_ASOCNAT_OPERACION ON DB_REGCHEQ.T_OPERACION_ASOCIADOS_NATURAL;
DROP INDEX IX_FICHANAT_ASOCIADO ON DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_NATURAL;
DROP INDEX IX_LISTANAT_FICHA    ON DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_LISTAS_NATURAL;
DROP INDEX IX_OPNAT_CREACION    ON DB_REGCHEQ.T_OPERACION_NATURAL;
DROP INDEX IX_OPNAT_CODIGO      ON DB_REGCHEQ.T_OPERACION_NATURAL;
*/

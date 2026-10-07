-- ============================================================================
-- RegCheq_QuitarEspaciosColumnas.sql
-- Base: IntegracionesNetCar (10.100.84.10), esquema DB_REGCHEQ.
-- Quita el espacio final del nombre de 6 columnas. Solo cambia el nombre:
-- no toca datos, tipos ni índices.
--
-- Seguro para la integración RegCheq: SQL Server ignora los espacios finales al
-- comparar nombres de columna, así que los INSERT existentes (con o sin espacio)
-- siguen funcionando igual.
--
-- Todo en una transacción: si algo falla, se revierte completo.
-- Idempotente: solo renombra si la columna con espacio todavía existe.
-- Rollback: ver el bloque comentado al final.
-- ============================================================================
SET XACT_ABORT ON;
SET NOCOUNT ON;

DECLARE @columnas TABLE (Tabla sysname, Columna sysname);
INSERT @columnas VALUES
 (N'T_OPERACION_NATURAL',                        N'BT_FORMSREQUIRED'),
 (N'T_OPERACION_NATURAL',                        N'DT_CREATED_AT'),
 (N'T_OPERACION_EMPRESA',                        N'BT_FORMSREQUIRED'),
 (N'T_OPERACION_EMPRESA',                        N'DT_CREATED_AT'),
 (N'T_OPERACION_ASOCIADOS_FICHA_NATURAL',        N'DT_FECHARECIBIDO'),
 (N'T_OPERACION_ASOCIADOS_FICHA_LISTAS_NATURAL', N'IN_COD_FICHA_NATURAL');

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @tabla sysname, @columna sysname, @actual sysname, @objeto nvarchar(600);
    DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT Tabla, Columna FROM @columnas;
    OPEN c;
    FETCH NEXT FROM c INTO @tabla, @columna;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Nombre real en la base (con el espacio). DATALENGTH distingue 'X ' de 'X'.
        SELECT @actual = col.name
        FROM sys.columns col
        WHERE col.object_id = OBJECT_ID(N'DB_REGCHEQ.' + QUOTENAME(@tabla))
          AND RTRIM(col.name) = @columna
          AND DATALENGTH(col.name) > DATALENGTH(@columna);

        IF @actual IS NOT NULL
        BEGIN
            SET @objeto = N'DB_REGCHEQ.' + QUOTENAME(@tabla) + N'.' + QUOTENAME(@actual);
            EXEC sys.sp_rename @objeto, @columna, N'COLUMN';
            PRINT CONCAT('Renombrada: DB_REGCHEQ.', @tabla, '.[', @actual, '] -> [', @columna, ']');
        END
        ELSE
            PRINT CONCAT('Sin cambios (ya está limpia o no existe): DB_REGCHEQ.', @tabla, '.', @columna);

        SET @actual = NULL;
        FETCH NEXT FROM c INTO @tabla, @columna;
    END
    CLOSE c; DEALLOCATE c;

    -- Validación: no debe quedar ninguna columna con espacio al inicio o al final en DB_REGCHEQ
    IF EXISTS (SELECT 1 FROM sys.columns col JOIN sys.tables t ON t.object_id = col.object_id
               WHERE SCHEMA_NAME(t.schema_id) = N'DB_REGCHEQ'
                 AND (DATALENGTH(col.name) <> DATALENGTH(RTRIM(LTRIM(col.name)))))
        THROW 50001, 'Quedaron columnas con espacios en DB_REGCHEQ. Se revierte todo.', 1;

    COMMIT TRANSACTION;
    PRINT 'OK: cambios confirmados.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

-- Verificación
SELECT t.name AS Tabla, QUOTENAME(col.name) AS Columna
FROM sys.columns col JOIN sys.tables t ON t.object_id = col.object_id
WHERE SCHEMA_NAME(t.schema_id) = N'DB_REGCHEQ'
  AND col.name IN (N'BT_FORMSREQUIRED', N'DT_CREATED_AT', N'DT_FECHARECIBIDO', N'IN_COD_FICHA_NATURAL')
  AND t.name IN (N'T_OPERACION_NATURAL', N'T_OPERACION_EMPRESA', N'T_OPERACION_ASOCIADOS_FICHA_NATURAL', N'T_OPERACION_ASOCIADOS_FICHA_LISTAS_NATURAL')
ORDER BY t.name, col.name;

/* ROLLBACK (devolver el espacio, solo si fuera necesario):
EXEC sp_rename N'DB_REGCHEQ.T_OPERACION_NATURAL.BT_FORMSREQUIRED',                     N'BT_FORMSREQUIRED ',     N'COLUMN';
EXEC sp_rename N'DB_REGCHEQ.T_OPERACION_NATURAL.DT_CREATED_AT',                        N'DT_CREATED_AT ',        N'COLUMN';
EXEC sp_rename N'DB_REGCHEQ.T_OPERACION_EMPRESA.BT_FORMSREQUIRED',                     N'BT_FORMSREQUIRED ',     N'COLUMN';
EXEC sp_rename N'DB_REGCHEQ.T_OPERACION_EMPRESA.DT_CREATED_AT',                        N'DT_CREATED_AT ',        N'COLUMN';
EXEC sp_rename N'DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_NATURAL.DT_FECHARECIBIDO',     N'DT_FECHARECIBIDO ',     N'COLUMN';
EXEC sp_rename N'DB_REGCHEQ.T_OPERACION_ASOCIADOS_FICHA_LISTAS_NATURAL.IN_COD_FICHA_NATURAL', N'IN_COD_FICHA_NATURAL ', N'COLUMN';
*/

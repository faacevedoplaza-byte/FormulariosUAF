# Renombrado a la convención T_ / PREFIJO_DESCRIPCION_ENTIDAD

Propuesta aprobada: `Propuesta_Renombrado_FormularioUAF.md` (23 tablas, 266 columnas).
Regla de FK aprobada: la FK se llama igual que la PK referenciada (`Requests.ClientId` → `IN_COD_CLIENTE`).

## Qué cambia

| Pieza | Archivo |
|---|---|
| Mapeo tabla/columna (código) | `FormulariosUAF/Data/DatabaseNaming.cs` (llamado al final de `ApplicationDbContext.OnModelCreating`) |
| Migración EF | `FormulariosUAF/Data/Migrations/20261001130256_ConvencionNombresT.cs` |
| Conteo previo (solo lectura) | `SQL/00_ConteoPrevio_ConvencionT.sql` |
| Renombrado | `SQL/01_Renombrado_ConvencionT.sql` |
| Validación (solo lectura) | `SQL/02_Validacion_ConvencionT.sql` |
| Rollback | `SQL/03_Rollback_ConvencionT.sql` |

El renombrado **no modifica datos, tipos ni relaciones**. Usa `sp_rename` para:
23 tablas, 266 columnas, 23 PK, 21 FK y 28 índices.

Única excepción: los índices `UserNameIndex` y `RoleNameIndex` (Identity) se **eliminan y recrean** con la misma
definición, porque SQL Server no permite renombrar una columna usada en el filtro de un índice filtrado
(error "The index 'RoleNameIndex' is dependent on column..."). Un índice no contiene datos propios.

No se tocan `__EFMigrationsHistory` ni `sysdiagrams`.

## Seguridad de los scripts 01 y 03

- Todo corre en **una sola transacción**. Cada paso está en `TRY/CATCH`: ante cualquier error se hace
  `ROLLBACK` de todo, se muestra el error y los pasos siguientes no se ejecutan. Al final aparece además
  "The COMMIT TRANSACTION request has no corresponding BEGIN TRANSACTION": es esperado e indica que no se aplicó nada.
- Son **idempotentes**: si la migración ya está aplicada (01) o ya revertida (03), no hacen nada.

## Procedimiento (primero en Qa2FormularioUAF, después en FormularioUAF)

1. **Respaldo completo** (el rollback definitivo):
   ```sql
   BACKUP DATABASE [FormularioUAF] TO DISK = N'<ruta>\FormularioUAF_antes_ConvencionT.bak' WITH COPY_ONLY, CHECKSUM, INIT;
   ```
2. Ejecutar `00_ConteoPrevio_ConvencionT.sql` y **guardar el resultado**.
3. **Detener la aplicación** (pool de IIS). La versión antigua falla apenas se renombra la primera tabla.
4. Ejecutar `01_Renombrado_ConvencionT.sql` en SSMS sobre la base correcta.
5. Ejecutar `02_Validacion_ConvencionT.sql`:
   - Sección 0 = 1 y sección 8 = 23 PK / 21 FK / 54 índices / 2 DEFAULT / 0 CHECK.
   - Todas las demás secciones **vacías**.
   - Sección 11 idéntica (Filas y HuellaDatos) al resultado del paso 2.
6. Publicar la **nueva versión** de la aplicación (con `DatabaseNaming.cs` y la migración) y levantar el pool.

> Importante: la aplicación ejecuta `MigrateAsync()` al iniciar. Si se publica la nueva versión sin haber
> corrido el paso 4, **la propia aplicación aplicará el renombrado** al arrancar. Por eso la nueva versión
> solo debe publicarse en la ventana acordada, y QA y Producción deben quedar en la misma versión de esquema
> que el código que corre contra ellas.

## Rollback

| Situación | Cómo revertir |
|---|---|
| `01` falló | Nada que hacer: se revirtió solo. Revisar el error, corregir la causa y volver a ejecutar. |
| `01` terminó pero la validación o la app fallan | Detener la app → ejecutar `03_Rollback_ConvencionT.sql` → validar con `00` (debe coincidir con el paso 2) → volver a publicar la **versión anterior** de la aplicación. |
| `03` falló | Se revirtió solo: la base queda renombrada y consistente. Revisar el error y reintentar, o restaurar el respaldo. |
| Cualquier otro problema | Restaurar el respaldo del paso 1 (se pierde lo escrito después del respaldo). |

`03` es el inverso exacto de `01` (incluidos los nombres de PK, FK e índices). No se debe revertir a mano
invirtiendo comandos sueltos: el orden importa (los índices filtrados deben eliminarse antes de renombrar sus columnas).

En código, revertir = quitar la llamada `DatabaseNaming.Apply(builder)`, borrar la migración
`ConvencionNombresT` (y su `.Designer.cs`) y restaurar `ApplicationDbContextModelSnapshot.cs` desde git,
o simplemente volver a publicar la versión anterior.

## Prueba realizada (LocalDB, base FUAF_PruebaRenombrado con datos ficticios)

- Esquema creado desde las migraciones = inventario de FormularioUAF (tablas, columnas, tipos, índices, FK, DEFAULT).
  Única diferencia previa y ajena a este cambio: `Requests.NetcarBusinessNumber` es `nvarchar(100)` en el servidor
  y `nvarchar(max)` en las migraciones.
- Fallo provocado a mitad de `01` → esquema, datos e historial de migraciones intactos.
- `01` → validación limpia, filas y huella de datos idénticas.
- `01` repetido → sin cambios. `03` → esquema idéntico al original (incluye nombres de constraints) y datos idénticos.
- Fallo provocado a mitad de `03` → base renombrada intacta.
- Validación con anomalías introducidas a propósito → las detecta todas.
- App levantada sobre la base renombrada: seed (crea roles y usuarios), login, /Admin, /Admin/Usuarios,
  /Cumplimiento, /Interno/Notificaciones y /Vendedor responden 200, sin errores SQL en el log.

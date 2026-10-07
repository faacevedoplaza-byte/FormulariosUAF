# Propuesta de renombrado - base FormularioUAF (10.100.84.5)

Generado el 2026-10-01. SOLO PROPUESTA: no incluye scripts de modificacion. Excluidas: `__EFMigrationsHistory` y `sysdiagrams` (tablas de sistema de EF Core y SSMS).

Regla FK (aprobada por el usuario): una columna FK se llama igual que la PK de la tabla referenciada (ej. Requests.ClientId -> IN_COD_CLIENTE), sin sufijo de la tabla donde esta.

Clasificacion: Seguro = significado claro y sin dependencias especiales | Revision = depende de una decision o toca algo sensible | AMBIGUO = no se puede confirmar el significado.

## Tablas

| Tipo | Nombre actual | Nombre propuesto | Filas | Clasificacion | Motivo |
|---|---|---|---|---|---|
| Tabla | Requests | T_SOLICITUD | 12 | Seguro | Prefijo T_ + entidad en espanol, singular. Entidad central del sistema |
| Tabla | Clients | T_CLIENTE | 8 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | Usuarios | T_USUARIO | 8 | Seguro | Prefijo T_ + entidad en espanol, singular. Singular |
| Tabla | Roles | T_ROL | 5 | Seguro | Prefijo T_ + entidad en espanol, singular. Singular |
| Tabla | UsuariosRoles | T_USUARIOROL | 8 | Seguro | Prefijo T_ + entidad en espanol, singular. Tabla puente Identity |
| Tabla | UsuariosClaims | T_USUARIOCLAIM | 0 | Revision | Prefijo T_ + entidad en espanol, singular. "Claim" es termino tecnico de ASP.NET Identity; se mantiene sin traducir |
| Tabla | UsuariosLogins | T_USUARIOLOGIN | 0 | Seguro | Prefijo T_ + entidad en espanol, singular. Logins externos Identity |
| Tabla | UsuariosTokens | T_USUARIOTOKEN | 0 | Seguro | Prefijo T_ + entidad en espanol, singular. Tokens Identity |
| Tabla | RolesClaims | T_ROLCLAIM | 0 | Revision | Prefijo T_ + entidad en espanol, singular. "Claim" se mantiene sin traducir |
| Tabla | LegalEntityDeclarations | T_DECLARACIONPERSONAJURIDICA | 12 | Revision | Prefijo T_ + entidad en espanol, singular. Nombre largo; alternativa corta: PERSONAJURIDICA |
| Tabla | DeclaredPersons | T_PERSONADECLARADA | 8 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | Declarants | T_DECLARANTE | 8 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | Documents | T_DOCUMENTO | 20 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | FileStorageRecords | T_ALMACENAMIENTOARCHIVO | 0 | Revision | Prefijo T_ + entidad en espanol, singular. Traduccion de "registro de almacenamiento de archivo" |
| Tabla | RequestStatusHistories | T_HISTORIALESTADOSOLICITUD | 48 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | AuditLogs | T_AUDITORIA | 164 | Revision | Prefijo T_ + entidad en espanol, singular. Traduccion de "registro de auditoria"; alternativa: REGISTROAUDITORIA |
| Tabla | Notifications | T_NOTIFICACION | 10 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | SignatureRecords | T_FIRMA | 0 | Revision | Prefijo T_ + entidad en espanol, singular. Traduccion de "registro de firma"; alternativa: REGISTROFIRMA |
| Tabla | TaxFolderAnalyses | T_ANALISISCARPETATRIBUTARIA | 7 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | TaxFolderAlerts | T_ALERTACARPETATRIBUTARIA | 7 | Seguro | Prefijo T_ + entidad en espanol, singular. Traduccion directa |
| Tabla | BeneficialOwners | T_BENEFICIARIOFINAL | 0 | Seguro | Prefijo T_ + entidad en espanol, singular. Tabla legacy (0 filas), se renombra igual |
| Tabla | EffectiveControllers | T_CONTROLADOREFECTIVO | 0 | Seguro | Prefijo T_ + entidad en espanol, singular. Tabla legacy (0 filas), se renombra igual |
| Tabla | PepDeclarations | T_DECLARACIONPEP | 0 | Seguro | Prefijo T_ + entidad en espanol, singular. Tabla legacy (0 filas), se renombra igual |

## Requests -> T_SOLICITUD

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | Requests.Id | IN_COD_SOLICITUD | Revision | PK; uniqueidentifier -> IN; PK uniqueidentifier (GUID): IN_ por regla de identificador, no por tipo |
| Columna | Requests.RequestNumber | ST_NUMERO_SOLICITUD | Seguro | nvarchar(50) -> ST |
| Columna | Requests.ClientId | IN_COD_CLIENTE | Seguro | FK -> Clients.Id; int -> IN; FK -> T_CLIENTE |
| Columna | Requests.VendorUserId | IN_COD_USUARIO | Revision | FK -> Usuarios.Id; nvarchar(450) -> IN; FK nvarchar(450) -> T_USUARIO; se pierde la palabra "vendedor" del nombre |
| Columna | Requests.RequestType | IN_TIPO_SOLICITUD | Seguro | int -> IN; enum RequestType |
| Columna | Requests.Status | IN_ESTADO_SOLICITUD | Seguro | int -> IN; enum RequestStatus |
| Columna | Requests.ClientToken | ST_TOKENCLIENTE_SOLICITUD | Seguro | nvarchar(200) -> ST |
| Columna | Requests.TokenExpiry | DT_FECHAVENCIMIENTOTOKEN_SOLICITUD | Seguro | datetime2 -> DT |
| Columna | Requests.InternalNotes | ST_NOTASINTERNAS_SOLICITUD | Seguro | nvarchar(max) -> ST |
| Columna | Requests.CurrentStep | IN_PASOACTUAL_SOLICITUD | Seguro | int -> IN; Paso actual del wizard |
| Columna | Requests.CreatedAt | DT_FECHACREACION_SOLICITUD | Seguro | datetime2 -> DT |
| Columna | Requests.CreatedBy | ST_CREADOPOR_SOLICITUD | Seguro | nvarchar(max) -> ST |
| Columna | Requests.UpdatedAt | DT_FECHAMODIFICACION_SOLICITUD | Seguro | datetime2 -> DT |
| Columna | Requests.UpdatedBy | ST_MODIFICADOPOR_SOLICITUD | Seguro | nvarchar(max) -> ST |
| Columna | Requests.SentAt | DT_FECHAENVIO_SOLICITUD | Seguro | datetime2 -> DT |
| Columna | Requests.OpenedAt | DT_FECHAAPERTURA_SOLICITUD | Seguro | datetime2 -> DT |
| Columna | Requests.CompletedAt | DT_FECHACOMPLETADA_SOLICITUD | Seguro | datetime2 -> DT |
| Columna | Requests.DueDate | DT_FECHAVENCIMIENTO_SOLICITUD | Seguro | datetime2 -> DT |
| Columna | Requests.IsDeleted | BO_ELIMINADO_SOLICITUD | Revision | bit -> BO; Usado en HasQueryFilter (soft delete) de EF |
| Columna | Requests.ClientEmail | ST_EMAILCLIENTE_SOLICITUD | Seguro | nvarchar(max) -> ST |
| Columna | Requests.ClientPhone | ST_TELEFONOCLIENTE_SOLICITUD | Seguro | nvarchar(max) -> ST |
| Columna | Requests.NetcarBusinessNumber | ST_NUMERONEGOCIONETCAR_SOLICITUD | Revision | nvarchar(100) -> ST; Confirmar que es el "numero de negocio" de Netcar |

## Clients -> T_CLIENTE

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | Clients.Id | IN_COD_CLIENTE | Seguro | PK; int -> IN; PK int identity |
| Columna | Clients.RUT | ST_RUT_CLIENTE | Seguro | nvarchar(20) -> ST |
| Columna | Clients.BusinessName | ST_RAZONSOCIAL_CLIENTE | Seguro | nvarchar(500) -> ST |
| Columna | Clients.IsActive | BO_ACTIVO_CLIENTE | Seguro | bit -> BO |
| Columna | Clients.CreatedAt | DT_FECHACREACION_CLIENTE | Seguro | datetime2 -> DT |
| Columna | Clients.CreatedBy | ST_CREADOPOR_CLIENTE | Seguro | nvarchar(max) -> ST |
| Columna | Clients.UpdatedAt | DT_FECHAMODIFICACION_CLIENTE | Seguro | datetime2 -> DT |
| Columna | Clients.UpdatedBy | ST_MODIFICADOPOR_CLIENTE | Seguro | nvarchar(max) -> ST |

## Usuarios -> T_USUARIO

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | Usuarios.Id | IN_COD_USUARIO | Revision | PK; nvarchar(450) -> IN; PK nvarchar(450) de Identity: IN_ por regla de identificador, no por tipo |
| Columna | Usuarios.FullName | ST_NOMBRECOMPLETO_USUARIO | Seguro | nvarchar(max) -> ST |
| Columna | Usuarios.VendorCode | ST_CODVENDEDOR_USUARIO | Seguro | nvarchar(max) -> ST |
| Columna | Usuarios.IsActive | BO_ACTIVO_USUARIO | Seguro | bit -> BO |
| Columna | Usuarios.CreatedAt | DT_FECHACREACION_USUARIO | Seguro | datetime2 -> DT |
| Columna | Usuarios.LastLoginAt | DT_FECHAULTIMOLOGIN_USUARIO | Seguro | datetime2 -> DT |
| Columna | Usuarios.UserName | ST_NOMBREUSUARIO_USUARIO | Revision | nvarchar(256) -> ST; Columna Identity (login = RUT normalizado) |
| Columna | Usuarios.NormalizedUserName | ST_NOMBREUSUARIONORMALIZADO_USUARIO | Revision | nvarchar(256) -> ST; Columna Identity; usada en indice filtrado UserNameIndex |
| Columna | Usuarios.Email | ST_EMAIL_USUARIO | Revision | nvarchar(256) -> ST; Columna Identity |
| Columna | Usuarios.NormalizedEmail | ST_EMAILNORMALIZADO_USUARIO | Revision | nvarchar(256) -> ST; Columna Identity; indice EmailIndex |
| Columna | Usuarios.EmailConfirmed | BO_EMAILCONFIRMADO_USUARIO | Revision | bit -> BO; Columna Identity |
| Columna | Usuarios.PasswordHash | ST_HASHCONTRASENA_USUARIO | Revision | nvarchar(max) -> ST; Columna Identity |
| Columna | Usuarios.SecurityStamp | ST_SELLOSEGURIDAD_USUARIO | Revision | nvarchar(max) -> ST; Columna Identity; traduccion de termino tecnico |
| Columna | Usuarios.ConcurrencyStamp | ST_SELLOCONCURRENCIA_USUARIO | Revision | nvarchar(max) -> ST; Columna Identity; token de concurrencia optimista |
| Columna | Usuarios.PhoneNumber | ST_TELEFONO_USUARIO | Revision | nvarchar(max) -> ST; Columna Identity |
| Columna | Usuarios.PhoneNumberConfirmed | BO_TELEFONOCONFIRMADO_USUARIO | Revision | bit -> BO; Columna Identity |
| Columna | Usuarios.TwoFactorEnabled | BO_DOBLEFACTORHABILITADO_USUARIO | Revision | bit -> BO; Columna Identity |
| Columna | Usuarios.LockoutEnd | DT_FECHAFINBLOQUEO_USUARIO | Revision | datetimeoffset -> DT; Columna Identity; tipo datetimeoffset |
| Columna | Usuarios.LockoutEnabled | BO_BLOQUEOHABILITADO_USUARIO | Revision | bit -> BO; Columna Identity |
| Columna | Usuarios.AccessFailedCount | IN_CANTIDADACCESOSFALLIDOS_USUARIO | Revision | int -> IN; Columna Identity |
| Columna | Usuarios.ApellidoMaterno | ST_APELLIDOMATERNO_USUARIO | Seguro | nvarchar(100) -> ST |
| Columna | Usuarios.ApellidoPaterno | ST_APELLIDOPATERNO_USUARIO | Seguro | nvarchar(100) -> ST |
| Columna | Usuarios.Nombre | ST_NOMBRE_USUARIO | Seguro | nvarchar(100) -> ST |
| Columna | Usuarios.Rut | ST_RUT_USUARIO | Seguro | nvarchar(20) -> ST |

## Roles -> T_ROL

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | Roles.Id | IN_COD_ROL | Revision | PK; nvarchar(450) -> IN; PK nvarchar(450) de Identity |
| Columna | Roles.Name | ST_NOMBRE_ROL | Revision | nvarchar(256) -> ST; Columna Identity |
| Columna | Roles.NormalizedName | ST_NOMBRENORMALIZADO_ROL | Revision | nvarchar(256) -> ST; Columna Identity; usada en indice filtrado RoleNameIndex |
| Columna | Roles.ConcurrencyStamp | ST_SELLOCONCURRENCIA_ROL | Revision | nvarchar(max) -> ST; Columna Identity |

## UsuariosRoles -> T_USUARIOROL

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | UsuariosRoles.UserId | IN_COD_USUARIO | Revision | PK+FK -> Usuarios.Id; nvarchar(450) -> IN; FK nvarchar -> T_USUARIO; parte de PK compuesta |
| Columna | UsuariosRoles.RoleId | IN_COD_ROL | Revision | PK+FK -> Roles.Id; nvarchar(450) -> IN; FK nvarchar -> T_ROL; parte de PK compuesta |

## UsuariosClaims -> T_USUARIOCLAIM

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | UsuariosClaims.Id | IN_COD_USUARIOCLAIM | Seguro | PK; int -> IN; PK int identity |
| Columna | UsuariosClaims.UserId | IN_COD_USUARIO | Revision | FK -> Usuarios.Id; nvarchar(450) -> IN; FK nvarchar -> T_USUARIO |
| Columna | UsuariosClaims.ClaimType | ST_TIPOCLAIM_USUARIOCLAIM | Revision | nvarchar(max) -> ST; Columna Identity |
| Columna | UsuariosClaims.ClaimValue | ST_VALORCLAIM_USUARIOCLAIM | Revision | nvarchar(max) -> ST; Columna Identity |

## UsuariosLogins -> T_USUARIOLOGIN

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | UsuariosLogins.LoginProvider | ST_PROVEEDORLOGIN_USUARIOLOGIN | Revision | PK; nvarchar(450) -> ST; Parte de PK compuesta; no hay IN_COD_ propio |
| Columna | UsuariosLogins.ProviderKey | ST_CLAVEPROVEEDOR_USUARIOLOGIN | Revision | PK; nvarchar(450) -> ST; Parte de PK compuesta |
| Columna | UsuariosLogins.ProviderDisplayName | ST_NOMBREVISIBLEPROVEEDOR_USUARIOLOGIN | Revision | nvarchar(max) -> ST; Columna Identity |
| Columna | UsuariosLogins.UserId | IN_COD_USUARIO | Revision | FK -> Usuarios.Id; nvarchar(450) -> IN; FK nvarchar -> T_USUARIO |

## UsuariosTokens -> T_USUARIOTOKEN

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | UsuariosTokens.UserId | IN_COD_USUARIO | Revision | PK+FK -> Usuarios.Id; nvarchar(450) -> IN; FK nvarchar -> T_USUARIO; parte de PK compuesta |
| Columna | UsuariosTokens.LoginProvider | ST_PROVEEDORLOGIN_USUARIOTOKEN | Revision | PK; nvarchar(450) -> ST; Parte de PK compuesta |
| Columna | UsuariosTokens.Name | ST_NOMBRE_USUARIOTOKEN | Revision | PK; nvarchar(450) -> ST; Parte de PK compuesta |
| Columna | UsuariosTokens.Value | ST_VALOR_USUARIOTOKEN | Revision | nvarchar(max) -> ST; Columna Identity |

## RolesClaims -> T_ROLCLAIM

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | RolesClaims.Id | IN_COD_ROLCLAIM | Seguro | PK; int -> IN; PK int identity |
| Columna | RolesClaims.RoleId | IN_COD_ROL | Revision | FK -> Roles.Id; nvarchar(450) -> IN; FK nvarchar -> T_ROL |
| Columna | RolesClaims.ClaimType | ST_TIPOCLAIM_ROLCLAIM | Revision | nvarchar(max) -> ST; Columna Identity |
| Columna | RolesClaims.ClaimValue | ST_VALORCLAIM_ROLCLAIM | Revision | nvarchar(max) -> ST; Columna Identity |

## LegalEntityDeclarations -> T_DECLARACIONPERSONAJURIDICA

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | LegalEntityDeclarations.Id | IN_COD_DECLARACIONPERSONAJURIDICA | Seguro | PK; int -> IN; PK int identity |
| Columna | LegalEntityDeclarations.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD (indice unico 1:1) |
| Columna | LegalEntityDeclarations.RUT | ST_RUT_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.BusinessName | ST_RAZONSOCIAL_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.Address | ST_DIRECCION_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.City | ST_CIUDAD_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.CountryOfIncorporation | ST_PAISCONSTITUCION_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.Phone | ST_TELEFONO_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.LegalRepresentativeIdNumber | ST_NUMEROIDENTIFICACIONREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.LegalRepresentativeName | ST_NOMBREREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST |
| Columna | LegalEntityDeclarations.EntityType | IN_TIPOENTIDAD_DECLARACIONPERSONAJURIDICA | Seguro | int -> IN; enum EntityType |
| Columna | LegalEntityDeclarations.EntityTypeOther | ST_TIPOENTIDADOTRO_DECLARACIONPERSONAJURIDICA | Seguro | nvarchar(max) -> ST; Texto libre cuando el tipo es "Otro" |
| Columna | LegalEntityDeclarations.CreatedAt | DT_FECHACREACION_DECLARACIONPERSONAJURIDICA | Seguro | datetime2 -> DT |
| Columna | LegalEntityDeclarations.UpdatedAt | DT_FECHAMODIFICACION_DECLARACIONPERSONAJURIDICA | Seguro | datetime2 -> DT |

## DeclaredPersons -> T_PERSONADECLARADA

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | DeclaredPersons.Id | IN_COD_PERSONADECLARADA | Revision | PK; uniqueidentifier -> IN; PK uniqueidentifier (GUID) |
| Columna | DeclaredPersons.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD |
| Columna | DeclaredPersons.IdNumber | ST_NUMEROIDENTIFICACION_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.FullName | ST_NOMBRECOMPLETO_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.Address | ST_DIRECCION_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.City | ST_CIUDAD_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.Country | ST_PAIS_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.ParticipationPercentage | NU_PORCENTAJEPARTICIPACION_PERSONADECLARADA | Seguro | decimal(5,2) -> NU |
| Columna | DeclaredPersons.RelationshipType | IN_TIPORELACION_PERSONADECLARADA | Seguro | int -> IN; enum RelationshipType |
| Columna | DeclaredPersons.RelationshipTypeOther | ST_TIPORELACIONOTRO_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.HasMinTenPercentParticipation | BO_PARTICIPACIONMINDIEZPORCIENTO_PERSONADECLARADA | Revision | bit -> BO; Nombre largo; confirmar redaccion |
| Columna | DeclaredPersons.IsEffectiveController | BO_ESCONTROLADOREFECTIVO_PERSONADECLARADA | Seguro | bit -> BO |
| Columna | DeclaredPersons.EffectiveControlDescription | ST_DESCRIPCIONCONTROLEFECTIVO_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.HandlesCashOrFunds | BO_MANEJAEFECTIVOFONDOS_PERSONADECLARADA | Seguro | bit -> BO |
| Columna | DeclaredPersons.IsPEP | BO_ESPEP_PERSONADECLARADA | Seguro | bit -> BO |
| Columna | DeclaredPersons.PepType | IN_TIPOPEP_PERSONADECLARADA | Seguro | int -> IN; enum PepType |
| Columna | DeclaredPersons.PepTypeName | ST_NOMBRETIPOPEP_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.PepInstitution | ST_INSTITUCIONPEP_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.PepPosition | ST_CARGOPEP_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.PepRelationship | ST_RELACIONPEP_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.PepObservation | ST_OBSERVACIONPEP_PERSONADECLARADA | Seguro | nvarchar(max) -> ST |
| Columna | DeclaredPersons.SortOrder | IN_ORDEN_PERSONADECLARADA | Seguro | int -> IN |
| Columna | DeclaredPersons.CreatedAt | DT_FECHACREACION_PERSONADECLARADA | Seguro | datetime2 -> DT |
| Columna | DeclaredPersons.UpdatedAt | DT_FECHAMODIFICACION_PERSONADECLARADA | Seguro | datetime2 -> DT |

## Declarants -> T_DECLARANTE

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | Declarants.Id | IN_COD_DECLARANTE | Seguro | PK; int -> IN; PK int identity |
| Columna | Declarants.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD (indice unico 1:1) |
| Columna | Declarants.NationalityType | IN_TIPONACIONALIDAD_DECLARANTE | Seguro | int -> IN; enum NationalityType |
| Columna | Declarants.IdNumber | ST_NUMEROIDENTIFICACION_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.FirstName | ST_NOMBRE_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.LastName1 | ST_APELLIDOPATERNO_DECLARANTE | Revision | nvarchar(max) -> ST; Se infiere paterno por convencion chilena; literal seria APELLIDO1 |
| Columna | Declarants.LastName2 | ST_APELLIDOMATERNO_DECLARANTE | Revision | nvarchar(max) -> ST; Se infiere materno; literal seria APELLIDO2 |
| Columna | Declarants.PlaceOfOrigin | ST_LUGARORIGEN_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.RelationshipWithLegalEntity | ST_RELACIONPERSONAJURIDICA_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.DeclaresUnderOath | BO_DECLARABAJOJURAMENTO_DECLARANTE | Seguro | bit -> BO |
| Columna | Declarants.City | ST_CIUDAD_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.DeclarationDate | DT_FECHADECLARACION_DECLARANTE | Seguro | datetime2 -> DT |
| Columna | Declarants.CreatedAt | DT_FECHACREACION_DECLARANTE | Seguro | datetime2 -> DT |
| Columna | Declarants.UpdatedAt | DT_FECHAMODIFICACION_DECLARANTE | Seguro | datetime2 -> DT |
| Columna | Declarants.Email | ST_EMAIL_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.Phone | ST_TELEFONO_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.SignatureDateTime | DT_FECHAFIRMA_DECLARANTE | Seguro | datetime2 -> DT |
| Columna | Declarants.SignatureFullName | ST_NOMBRECOMPLETOFIRMA_DECLARANTE | Revision | nvarchar(max) -> ST; Tiene DEFAULT con nombre de sistema (DF__Declarant__Signa__5AEE82B9) |
| Columna | Declarants.SignatureIdNumber | ST_NUMEROIDENTIFICACIONFIRMA_DECLARANTE | Revision | nvarchar(max) -> ST; Tiene DEFAULT con nombre de sistema (DF__Declarant__Signa__5BE2A6F2) |
| Columna | Declarants.SignatureIpAddress | ST_DIRECCIONIPFIRMA_DECLARANTE | Seguro | nvarchar(max) -> ST |
| Columna | Declarants.SignatureUserAgent | ST_USERAGENTFIRMA_DECLARANTE | Revision | nvarchar(max) -> ST; "User agent" se mantiene sin traducir |

## Documents -> T_DOCUMENTO

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | Documents.Id | IN_COD_DOCUMENTO | Seguro | PK; int -> IN; PK int identity |
| Columna | Documents.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD |
| Columna | Documents.DocumentType | IN_TIPO_DOCUMENTO | Seguro | int -> IN; enum DocumentType |
| Columna | Documents.DocumentTypeOther | ST_TIPOOTRO_DOCUMENTO | Seguro | nvarchar(max) -> ST |
| Columna | Documents.OriginalFileName | ST_NOMBREARCHIVOORIGINAL_DOCUMENTO | Seguro | nvarchar(max) -> ST |
| Columna | Documents.StoredFileName | ST_NOMBREARCHIVOALMACENADO_DOCUMENTO | Seguro | nvarchar(max) -> ST |
| Columna | Documents.StoragePath | ST_RUTAALMACENAMIENTO_DOCUMENTO | Seguro | nvarchar(max) -> ST |
| Columna | Documents.MimeType | ST_TIPOMIME_DOCUMENTO | Seguro | nvarchar(max) -> ST |
| Columna | Documents.FileSizeBytes | IN_TAMANOBYTES_DOCUMENTO | Seguro | bigint -> IN; bigint -> IN_ |
| Columna | Documents.IsActive | BO_ACTIVO_DOCUMENTO | Seguro | bit -> BO |
| Columna | Documents.Version | IN_VERSION_DOCUMENTO | Seguro | int -> IN |
| Columna | Documents.StorageProvider | IN_PROVEEDORALMACENAMIENTO_DOCUMENTO | Seguro | int -> IN; enum StorageProvider |
| Columna | Documents.UploadedAt | DT_FECHACARGA_DOCUMENTO | Seguro | datetime2 -> DT |
| Columna | Documents.UploadedByIp | ST_IPCARGA_DOCUMENTO | Seguro | nvarchar(max) -> ST |

## FileStorageRecords -> T_ALMACENAMIENTOARCHIVO

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | FileStorageRecords.Id | IN_COD_ALMACENAMIENTOARCHIVO | Seguro | PK; int -> IN; PK int identity |
| Columna | FileStorageRecords.DocumentId | IN_COD_DOCUMENTO | Seguro | FK -> Documents.Id; int -> IN; FK -> T_DOCUMENTO |
| Columna | FileStorageRecords.StorageProvider | IN_PROVEEDORALMACENAMIENTO_ALMACENAMIENTOARCHIVO | Seguro | int -> IN; enum StorageProvider |
| Columna | FileStorageRecords.StoragePath | ST_RUTAALMACENAMIENTO_ALMACENAMIENTOARCHIVO | Seguro | nvarchar(max) -> ST |
| Columna | FileStorageRecords.StorageKey | ST_CLAVEALMACENAMIENTO_ALMACENAMIENTOARCHIVO | **AMBIGUO - requiere revision** | nvarchar(max) -> ST; Tabla vacia; no se puede confirmar si es clave/nombre de blob o identificador externo |
| Columna | FileStorageRecords.UploadedAt | DT_FECHACARGA_ALMACENAMIENTOARCHIVO | Seguro | datetime2 -> DT |
| Columna | FileStorageRecords.Metadata | ST_METADATOS_ALMACENAMIENTOARCHIVO | Revision | nvarchar(max) -> ST; Tabla vacia; contenido no verificable |

## RequestStatusHistories -> T_HISTORIALESTADOSOLICITUD

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | RequestStatusHistories.Id | IN_COD_HISTORIALESTADOSOLICITUD | Seguro | PK; int -> IN; PK int identity |
| Columna | RequestStatusHistories.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD |
| Columna | RequestStatusHistories.OldStatus | IN_ESTADOANTERIOR_HISTORIALESTADOSOLICITUD | Seguro | int -> IN |
| Columna | RequestStatusHistories.NewStatus | IN_ESTADONUEVO_HISTORIALESTADOSOLICITUD | Seguro | int -> IN |
| Columna | RequestStatusHistories.ChangedBy | ST_CAMBIADOPOR_HISTORIALESTADOSOLICITUD | Seguro | nvarchar(max) -> ST |
| Columna | RequestStatusHistories.Notes | ST_NOTAS_HISTORIALESTADOSOLICITUD | Seguro | nvarchar(max) -> ST |
| Columna | RequestStatusHistories.ChangedAt | DT_FECHACAMBIO_HISTORIALESTADOSOLICITUD | Seguro | datetime2 -> DT |

## AuditLogs -> T_AUDITORIA

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | AuditLogs.Id | IN_COD_AUDITORIA | Seguro | PK; bigint -> IN; PK bigint identity |
| Columna | AuditLogs.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD (ON DELETE SET NULL) |
| Columna | AuditLogs.Action | ST_ACCION_AUDITORIA | Seguro | nvarchar(max) -> ST |
| Columna | AuditLogs.EntityType | ST_TIPOENTIDAD_AUDITORIA | Seguro | nvarchar(max) -> ST |
| Columna | AuditLogs.EntityId | ST_CODENTIDAD_AUDITORIA | Revision | nvarchar(max) -> ST; Texto con el Id de cualquier entidad auditada; no es FK |
| Columna | AuditLogs.OldValues | ST_VALORESANTERIORES_AUDITORIA | Seguro | nvarchar(max) -> ST |
| Columna | AuditLogs.NewValues | ST_VALORESNUEVOS_AUDITORIA | Seguro | nvarchar(max) -> ST |
| Columna | AuditLogs.UserId | IN_COD_USUARIO | Revision | nvarchar(max) -> IN; Referencia logica a T_USUARIO SIN FK; nvarchar(max) |
| Columna | AuditLogs.UserName | ST_NOMBREUSUARIO_AUDITORIA | Seguro | nvarchar(max) -> ST |
| Columna | AuditLogs.IpAddress | ST_DIRECCIONIP_AUDITORIA | Seguro | nvarchar(max) -> ST |
| Columna | AuditLogs.UserAgent | ST_USERAGENT_AUDITORIA | Revision | nvarchar(max) -> ST; "User agent" se mantiene sin traducir |
| Columna | AuditLogs.CreatedAt | DT_FECHACREACION_AUDITORIA | Seguro | datetime2 -> DT |

## Notifications -> T_NOTIFICACION

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | Notifications.Id | IN_COD_NOTIFICACION | Seguro | PK; int -> IN; PK int identity |
| Columna | Notifications.UserId | IN_COD_USUARIO | Revision | nvarchar(450) -> IN; Referencia logica a T_USUARIO SIN FK |
| Columna | Notifications.Type | IN_TIPO_NOTIFICACION | Seguro | int -> IN; enum NotificationType |
| Columna | Notifications.Message | ST_MENSAJE_NOTIFICACION | Seguro | nvarchar(max) -> ST |
| Columna | Notifications.RequestId | IN_COD_SOLICITUD | Revision | uniqueidentifier -> IN; Referencia logica a T_SOLICITUD SIN FK |
| Columna | Notifications.IsRead | BO_LEIDA_NOTIFICACION | Seguro | bit -> BO |
| Columna | Notifications.ReadAt | DT_FECHALECTURA_NOTIFICACION | Seguro | datetime2 -> DT |
| Columna | Notifications.CreatedAt | DT_FECHACREACION_NOTIFICACION | Seguro | datetime2 -> DT |

## SignatureRecords -> T_FIRMA

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | SignatureRecords.Id | IN_COD_FIRMA | Seguro | PK; int -> IN; PK int identity |
| Columna | SignatureRecords.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD |
| Columna | SignatureRecords.Type | IN_TIPO_FIRMA | Seguro | int -> IN; enum SignatureType |
| Columna | SignatureRecords.Status | IN_ESTADO_FIRMA | Seguro | int -> IN; enum SignatureStatus |
| Columna | SignatureRecords.SignerName | ST_NOMBREFIRMANTE_FIRMA | Seguro | nvarchar(max) -> ST |
| Columna | SignatureRecords.SignerIdNumber | ST_NUMEROIDENTIFICACIONFIRMANTE_FIRMA | Seguro | nvarchar(max) -> ST |
| Columna | SignatureRecords.SignedAt | DT_FECHAFIRMA_FIRMA | Seguro | datetime2 -> DT |
| Columna | SignatureRecords.IpAddress | ST_DIRECCIONIP_FIRMA | Seguro | nvarchar(max) -> ST |
| Columna | SignatureRecords.UserAgent | ST_USERAGENT_FIRMA | Revision | nvarchar(max) -> ST; "User agent" se mantiene sin traducir |
| Columna | SignatureRecords.DocumentHash | ST_HASHDOCUMENTO_FIRMA | Seguro | nvarchar(max) -> ST |
| Columna | SignatureRecords.FolioNumber | ST_NUMEROFOLIO_FIRMA | Seguro | nvarchar(max) -> ST |
| Columna | SignatureRecords.ProviderTransactionId | ST_CODTRANSACCIONPROVEEDOR_FIRMA | Revision | nvarchar(max) -> ST; Campo para FEA futura; sin uso aun |
| Columna | SignatureRecords.SignatureUrl | ST_URL_FIRMA | Revision | nvarchar(max) -> ST; Campo para FEA futura; sin uso aun |
| Columna | SignatureRecords.Certificate | ST_CERTIFICADO_FIRMA | Revision | nvarchar(max) -> ST; Campo para FEA futura; sin uso aun |
| Columna | SignatureRecords.SignedPdfPath | ST_RUTAPDFFIRMADO_FIRMA | Seguro | nvarchar(max) -> ST |
| Columna | SignatureRecords.CreatedAt | DT_FECHACREACION_FIRMA | Seguro | datetime2 -> DT |
| Columna | SignatureRecords.UpdatedAt | DT_FECHAMODIFICACION_FIRMA | Seguro | datetime2 -> DT |

## TaxFolderAnalyses -> T_ANALISISCARPETATRIBUTARIA

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | TaxFolderAnalyses.Id | IN_COD_ANALISISCARPETATRIBUTARIA | Seguro | PK; int -> IN; PK int identity |
| Columna | TaxFolderAnalyses.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD (indice unico 1:1) |
| Columna | TaxFolderAnalyses.ExtractedRut | ST_RUTEXTRAIDO_ANALISISCARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST |
| Columna | TaxFolderAnalyses.ExtractedBusinessName | ST_RAZONSOCIALEXTRAIDA_ANALISISCARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST |
| Columna | TaxFolderAnalyses.ExtractedAddress | ST_DIRECCIONEXTRAIDA_ANALISISCARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST |
| Columna | TaxFolderAnalyses.ExtractedActivity | ST_ACTIVIDADEXTRAIDA_ANALISISCARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST |
| Columna | TaxFolderAnalyses.ExtractedLegalRep | ST_REPRESENTANTELEGALEXTRAIDO_ANALISISCARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST |
| Columna | TaxFolderAnalyses.ExtractedIssuedDate | ST_FECHAEMISIONEXTRAIDA_ANALISISCARPETATRIBUTARIA | Revision | nvarchar(max) -> ST; Es una fecha guardada como TEXTO: lleva ST_ (no se cambia el tipo) |
| Columna | TaxFolderAnalyses.RawText | ST_TEXTOBRUTO_ANALISISCARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST; Texto extraido del PDF sin procesar |
| Columna | TaxFolderAnalyses.WasReadable | BO_LEGIBLE_ANALISISCARPETATRIBUTARIA | Seguro | bit -> BO |
| Columna | TaxFolderAnalyses.AnalyzedAt | DT_FECHAANALISIS_ANALISISCARPETATRIBUTARIA | Seguro | datetime2 -> DT |

## TaxFolderAlerts -> T_ALERTACARPETATRIBUTARIA

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | TaxFolderAlerts.Id | IN_COD_ALERTACARPETATRIBUTARIA | Seguro | PK; int -> IN; PK int identity |
| Columna | TaxFolderAlerts.TaxFolderAnalysisId | IN_COD_ANALISISCARPETATRIBUTARIA | Seguro | FK -> TaxFolderAnalyses.Id; int -> IN; FK -> T_ANALISISCARPETATRIBUTARIA |
| Columna | TaxFolderAlerts.AlertType | IN_TIPO_ALERTACARPETATRIBUTARIA | Seguro | int -> IN; enum TaxFolderAlertType |
| Columna | TaxFolderAlerts.Message | ST_MENSAJE_ALERTACARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST |
| Columna | TaxFolderAlerts.Severity | ST_SEVERIDAD_ALERTACARPETATRIBUTARIA | Seguro | nvarchar(max) -> ST |
| Columna | TaxFolderAlerts.IsResolved | BO_RESUELTA_ALERTACARPETATRIBUTARIA | Seguro | bit -> BO |

## BeneficialOwners -> T_BENEFICIARIOFINAL

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | BeneficialOwners.Id | IN_COD_BENEFICIARIOFINAL | Seguro | PK; int -> IN; PK int identity |
| Columna | BeneficialOwners.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD |
| Columna | BeneficialOwners.IdNumber | ST_NUMEROIDENTIFICACION_BENEFICIARIOFINAL | Seguro | nvarchar(max) -> ST |
| Columna | BeneficialOwners.FullName | ST_NOMBRECOMPLETO_BENEFICIARIOFINAL | Seguro | nvarchar(max) -> ST |
| Columna | BeneficialOwners.Address | ST_DIRECCION_BENEFICIARIOFINAL | Seguro | nvarchar(max) -> ST |
| Columna | BeneficialOwners.City | ST_CIUDAD_BENEFICIARIOFINAL | Seguro | nvarchar(max) -> ST |
| Columna | BeneficialOwners.Country | ST_PAIS_BENEFICIARIOFINAL | Seguro | nvarchar(max) -> ST |
| Columna | BeneficialOwners.ParticipationPercentage | NU_PORCENTAJEPARTICIPACION_BENEFICIARIOFINAL | Seguro | decimal(5,2) -> NU |
| Columna | BeneficialOwners.IsBeneficialOwner | BO_ESBENEFICIARIOFINAL_BENEFICIARIOFINAL | Seguro | bit -> BO |
| Columna | BeneficialOwners.IsEffectiveControl | BO_ESCONTROLEFECTIVO_BENEFICIARIOFINAL | Seguro | bit -> BO |
| Columna | BeneficialOwners.IsPEP | BO_ESPEP_BENEFICIARIOFINAL | Seguro | bit -> BO |
| Columna | BeneficialOwners.PepDetail | ST_DETALLEPEP_BENEFICIARIOFINAL | Seguro | nvarchar(max) -> ST |
| Columna | BeneficialOwners.SortOrder | IN_ORDEN_BENEFICIARIOFINAL | Seguro | int -> IN |
| Columna | BeneficialOwners.CreatedAt | DT_FECHACREACION_BENEFICIARIOFINAL | Seguro | datetime2 -> DT |
| Columna | BeneficialOwners.UpdatedAt | DT_FECHAMODIFICACION_BENEFICIARIOFINAL | Seguro | datetime2 -> DT |

## EffectiveControllers -> T_CONTROLADOREFECTIVO

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | EffectiveControllers.Id | IN_COD_CONTROLADOREFECTIVO | Seguro | PK; int -> IN; PK int identity |
| Columna | EffectiveControllers.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD |
| Columna | EffectiveControllers.IdNumber | ST_NUMEROIDENTIFICACION_CONTROLADOREFECTIVO | Seguro | nvarchar(max) -> ST |
| Columna | EffectiveControllers.FullName | ST_NOMBRECOMPLETO_CONTROLADOREFECTIVO | Seguro | nvarchar(max) -> ST |
| Columna | EffectiveControllers.Address | ST_DIRECCION_CONTROLADOREFECTIVO | Seguro | nvarchar(max) -> ST |
| Columna | EffectiveControllers.City | ST_CIUDAD_CONTROLADOREFECTIVO | Seguro | nvarchar(max) -> ST |
| Columna | EffectiveControllers.Country | ST_PAIS_CONTROLADOREFECTIVO | Seguro | nvarchar(max) -> ST |
| Columna | EffectiveControllers.ParticipationPercentage | NU_PORCENTAJEPARTICIPACION_CONTROLADOREFECTIVO | Seguro | decimal(5,2) -> NU |
| Columna | EffectiveControllers.IsPEP | BO_ESPEP_CONTROLADOREFECTIVO | Seguro | bit -> BO |
| Columna | EffectiveControllers.PepDetail | ST_DETALLEPEP_CONTROLADOREFECTIVO | Seguro | nvarchar(max) -> ST |
| Columna | EffectiveControllers.ControlDescription | ST_DESCRIPCIONCONTROL_CONTROLADOREFECTIVO | Seguro | nvarchar(max) -> ST |
| Columna | EffectiveControllers.SortOrder | IN_ORDEN_CONTROLADOREFECTIVO | Seguro | int -> IN |
| Columna | EffectiveControllers.CreatedAt | DT_FECHACREACION_CONTROLADOREFECTIVO | Seguro | datetime2 -> DT |
| Columna | EffectiveControllers.UpdatedAt | DT_FECHAMODIFICACION_CONTROLADOREFECTIVO | Seguro | datetime2 -> DT |

## PepDeclarations -> T_DECLARACIONPEP

| Tipo | Nombre actual | Nombre propuesto | Clasificacion | Motivo |
|---|---|---|---|---|
| Columna | PepDeclarations.Id | IN_COD_DECLARACIONPEP | Seguro | PK; int -> IN; PK int identity |
| Columna | PepDeclarations.RequestId | IN_COD_SOLICITUD | Revision | FK -> Requests.Id; uniqueidentifier -> IN; FK uniqueidentifier -> T_SOLICITUD (indice unico 1:1) |
| Columna | PepDeclarations.DeclarantName | ST_NOMBREDECLARANTE_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.IdNumber | ST_NUMEROIDENTIFICACION_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.Nationality | ST_NACIONALIDAD_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.DeclaresPEP | BO_DECLARAPEP_DECLARACIONPEP | Seguro | bit -> BO |
| Columna | PepDeclarations.PepName | ST_NOMBREPEP_DECLARACIONPEP | Revision | nvarchar(max) -> ST; No queda claro si es el nombre de la persona PEP o del cargo; tabla vacia |
| Columna | PepDeclarations.Institution | ST_INSTITUCION_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.PepReasonType | IN_MOTIVOPEP_DECLARACIONPEP | Seguro | int -> IN; enum PepReason (Titular/Asociado/Parentesco/Otro) |
| Columna | PepDeclarations.PepReasonOther | ST_MOTIVOPEPOTRO_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.VinculoType | ST_TIPOVINCULO_DECLARACIONPEP | **AMBIGUO - requiere revision** | nvarchar(max) -> ST; Texto libre, mezcla espanol/ingles, sin enum ni datos (0 filas): no se puede confirmar a que vinculo se refiere |
| Columna | PepDeclarations.DeclarationDate | DT_FECHADECLARACION_DECLARACIONPEP | Seguro | datetime2 -> DT |
| Columna | PepDeclarations.AcceptsUnderOath | BO_ACEPTABAJOJURAMENTO_DECLARACIONPEP | Seguro | bit -> BO |
| Columna | PepDeclarations.SignatureFullName | ST_NOMBRECOMPLETOFIRMA_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.SignatureIdNumber | ST_NUMEROIDENTIFICACIONFIRMA_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.SignatureDateTime | DT_FECHAFIRMA_DECLARACIONPEP | Seguro | datetime2 -> DT |
| Columna | PepDeclarations.SignatureIpAddress | ST_DIRECCIONIPFIRMA_DECLARACIONPEP | Seguro | nvarchar(max) -> ST |
| Columna | PepDeclarations.SignatureUserAgent | ST_USERAGENTFIRMA_DECLARACIONPEP | Revision | nvarchar(max) -> ST; "User agent" se mantiene sin traducir |
| Columna | PepDeclarations.CreatedAt | DT_FECHACREACION_DECLARACIONPEP | Seguro | datetime2 -> DT |
| Columna | PepDeclarations.UpdatedAt | DT_FECHAMODIFICACION_DECLARACIONPEP | Seguro | datetime2 -> DT |

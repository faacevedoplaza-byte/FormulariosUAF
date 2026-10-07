using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Data;

/// <summary>
/// Convención de nombres de la base de datos:
///   Tablas:   T_[ENTIDAD]
///   Columnas: [PREFIJO]_[DESCRIPCION]_[ENTIDAD]  (IN_ enteros/identificadores, ST_ texto, DT_ fechas, NU_ decimales, BO_ bit)
///   PK:       IN_COD_[ENTIDAD]
///   FK:       mismo nombre que la PK de la tabla referenciada (ej. IN_COD_CLIENTE)
/// Las clases del dominio mantienen sus nombres; aquí se traduce cada tabla y propiedad.
/// Toda tabla o propiedad nueva debe agregarse a este mapa; si falta, el modelo no se construye.
/// Ver SQL/Propuesta_Renombrado_FormularioUAF.md
/// </summary>
public static class DatabaseNaming
{
    // Clave: nombre lógico de la tabla (DbSet / ToTable). Valor: nombre físico + columnas por propiedad.
    private static readonly Dictionary<string, (string Table, Dictionary<string, string> Columns)> Map = new()
    {
        ["Requests"] = ("T_SOLICITUD", new()
        {
            ["Id"] = "IN_COD_SOLICITUD",
            ["RequestNumber"] = "ST_NUMERO_SOLICITUD",
            ["ClientId"] = "IN_COD_CLIENTE",
            ["VendorUserId"] = "IN_COD_USUARIO",
            ["RequestType"] = "IN_TIPO_SOLICITUD",
            ["Status"] = "IN_ESTADO_SOLICITUD",
            ["ClientToken"] = "ST_TOKENCLIENTE_SOLICITUD",
            ["TokenExpiry"] = "DT_FECHAVENCIMIENTOTOKEN_SOLICITUD",
            ["InternalNotes"] = "ST_NOTASINTERNAS_SOLICITUD",
            ["CurrentStep"] = "IN_PASOACTUAL_SOLICITUD",
            ["CreatedAt"] = "DT_FECHACREACION_SOLICITUD",
            ["CreatedBy"] = "ST_CREADOPOR_SOLICITUD",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_SOLICITUD",
            ["UpdatedBy"] = "ST_MODIFICADOPOR_SOLICITUD",
            ["SentAt"] = "DT_FECHAENVIO_SOLICITUD",
            ["OpenedAt"] = "DT_FECHAAPERTURA_SOLICITUD",
            ["CompletedAt"] = "DT_FECHACOMPLETADA_SOLICITUD",
            ["DueDate"] = "DT_FECHAVENCIMIENTO_SOLICITUD",
            ["IsDeleted"] = "BO_ELIMINADO_SOLICITUD",
            ["ClientEmail"] = "ST_EMAILCLIENTE_SOLICITUD",
            ["ClientPhone"] = "ST_TELEFONOCLIENTE_SOLICITUD",
            ["NetcarBusinessNumber"] = "ST_NUMERONEGOCIONETCAR_SOLICITUD",
        }),
        ["Clients"] = ("T_CLIENTE", new()
        {
            ["Id"] = "IN_COD_CLIENTE",
            ["RUT"] = "ST_RUT_CLIENTE",
            ["BusinessName"] = "ST_RAZONSOCIAL_CLIENTE",
            ["IsActive"] = "BO_ACTIVO_CLIENTE",
            ["CreatedAt"] = "DT_FECHACREACION_CLIENTE",
            ["CreatedBy"] = "ST_CREADOPOR_CLIENTE",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_CLIENTE",
            ["UpdatedBy"] = "ST_MODIFICADOPOR_CLIENTE",
        }),
        ["Usuarios"] = ("T_USUARIO", new()
        {
            ["Id"] = "IN_COD_USUARIO",
            ["FullName"] = "ST_NOMBRECOMPLETO_USUARIO",
            ["VendorCode"] = "ST_CODVENDEDOR_USUARIO",
            ["IsActive"] = "BO_ACTIVO_USUARIO",
            ["CreatedAt"] = "DT_FECHACREACION_USUARIO",
            ["LastLoginAt"] = "DT_FECHAULTIMOLOGIN_USUARIO",
            ["UserName"] = "ST_NOMBREUSUARIO_USUARIO",
            ["NormalizedUserName"] = "ST_NOMBREUSUARIONORMALIZADO_USUARIO",
            ["Email"] = "ST_EMAIL_USUARIO",
            ["NormalizedEmail"] = "ST_EMAILNORMALIZADO_USUARIO",
            ["EmailConfirmed"] = "BO_EMAILCONFIRMADO_USUARIO",
            ["PasswordHash"] = "ST_HASHCONTRASENA_USUARIO",
            ["SecurityStamp"] = "ST_SELLOSEGURIDAD_USUARIO",
            ["ConcurrencyStamp"] = "ST_SELLOCONCURRENCIA_USUARIO",
            ["PhoneNumber"] = "ST_TELEFONO_USUARIO",
            ["PhoneNumberConfirmed"] = "BO_TELEFONOCONFIRMADO_USUARIO",
            ["TwoFactorEnabled"] = "BO_DOBLEFACTORHABILITADO_USUARIO",
            ["LockoutEnd"] = "DT_FECHAFINBLOQUEO_USUARIO",
            ["LockoutEnabled"] = "BO_BLOQUEOHABILITADO_USUARIO",
            ["AccessFailedCount"] = "IN_CANTIDADACCESOSFALLIDOS_USUARIO",
            ["ApellidoMaterno"] = "ST_APELLIDOMATERNO_USUARIO",
            ["ApellidoPaterno"] = "ST_APELLIDOPATERNO_USUARIO",
            ["Nombre"] = "ST_NOMBRE_USUARIO",
            ["Rut"] = "ST_RUT_USUARIO",
        }),
        ["Roles"] = ("T_ROL", new()
        {
            ["Id"] = "IN_COD_ROL",
            ["Name"] = "ST_NOMBRE_ROL",
            ["NormalizedName"] = "ST_NOMBRENORMALIZADO_ROL",
            ["ConcurrencyStamp"] = "ST_SELLOCONCURRENCIA_ROL",
        }),
        ["UsuariosRoles"] = ("T_USUARIOROL", new()
        {
            ["UserId"] = "IN_COD_USUARIO",
            ["RoleId"] = "IN_COD_ROL",
        }),
        ["UsuariosClaims"] = ("T_USUARIOCLAIM", new()
        {
            ["Id"] = "IN_COD_USUARIOCLAIM",
            ["UserId"] = "IN_COD_USUARIO",
            ["ClaimType"] = "ST_TIPOCLAIM_USUARIOCLAIM",
            ["ClaimValue"] = "ST_VALORCLAIM_USUARIOCLAIM",
        }),
        ["UsuariosLogins"] = ("T_USUARIOLOGIN", new()
        {
            ["LoginProvider"] = "ST_PROVEEDORLOGIN_USUARIOLOGIN",
            ["ProviderKey"] = "ST_CLAVEPROVEEDOR_USUARIOLOGIN",
            ["ProviderDisplayName"] = "ST_NOMBREVISIBLEPROVEEDOR_USUARIOLOGIN",
            ["UserId"] = "IN_COD_USUARIO",
        }),
        ["UsuariosTokens"] = ("T_USUARIOTOKEN", new()
        {
            ["UserId"] = "IN_COD_USUARIO",
            ["LoginProvider"] = "ST_PROVEEDORLOGIN_USUARIOTOKEN",
            ["Name"] = "ST_NOMBRE_USUARIOTOKEN",
            ["Value"] = "ST_VALOR_USUARIOTOKEN",
        }),
        ["RolesClaims"] = ("T_ROLCLAIM", new()
        {
            ["Id"] = "IN_COD_ROLCLAIM",
            ["RoleId"] = "IN_COD_ROL",
            ["ClaimType"] = "ST_TIPOCLAIM_ROLCLAIM",
            ["ClaimValue"] = "ST_VALORCLAIM_ROLCLAIM",
        }),
        ["LegalEntityDeclarations"] = ("T_DECLARACIONPERSONAJURIDICA", new()
        {
            ["Id"] = "IN_COD_DECLARACIONPERSONAJURIDICA",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["RUT"] = "ST_RUT_DECLARACIONPERSONAJURIDICA",
            ["BusinessName"] = "ST_RAZONSOCIAL_DECLARACIONPERSONAJURIDICA",
            ["Address"] = "ST_DIRECCION_DECLARACIONPERSONAJURIDICA",
            ["City"] = "ST_CIUDAD_DECLARACIONPERSONAJURIDICA",
            ["CountryOfIncorporation"] = "ST_PAISCONSTITUCION_DECLARACIONPERSONAJURIDICA",
            ["Phone"] = "ST_TELEFONO_DECLARACIONPERSONAJURIDICA",
            ["LegalRepresentativeIdNumber"] = "ST_NUMEROIDENTIFICACIONREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA",
            ["LegalRepresentativeName"] = "ST_NOMBREREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA",
            ["EntityType"] = "IN_TIPOENTIDAD_DECLARACIONPERSONAJURIDICA",
            ["EntityTypeOther"] = "ST_TIPOENTIDADOTRO_DECLARACIONPERSONAJURIDICA",
            ["CreatedAt"] = "DT_FECHACREACION_DECLARACIONPERSONAJURIDICA",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_DECLARACIONPERSONAJURIDICA",
        }),
        ["DeclaredPersons"] = ("T_PERSONADECLARADA", new()
        {
            ["Id"] = "IN_COD_PERSONADECLARADA",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["TipoDocumentoId"] = "IN_COD_TIPODOCUMENTO",
            ["IdNumber"] = "ST_NUMEROIDENTIFICACION_PERSONADECLARADA",
            ["FullName"] = "ST_NOMBRECOMPLETO_PERSONADECLARADA",
            ["NacionalidadId"] = "IN_COD_NACIONALIDAD",
            ["PaisResidenciaId"] = "IN_COD_PAIS",
            ["Address"] = "ST_DIRECCION_PERSONADECLARADA",
            ["City"] = "ST_CIUDAD_PERSONADECLARADA",
            ["Country"] = "ST_PAIS_PERSONADECLARADA",
            ["ParticipationPercentage"] = "NU_PORCENTAJEPARTICIPACION_PERSONADECLARADA",
            ["RelationshipType"] = "IN_TIPORELACION_PERSONADECLARADA",
            ["RelationshipTypeOther"] = "ST_TIPORELACIONOTRO_PERSONADECLARADA",
            ["HasMinTenPercentParticipation"] = "BO_PARTICIPACIONMINDIEZPORCIENTO_PERSONADECLARADA",
            ["IsEffectiveController"] = "BO_ESCONTROLADOREFECTIVO_PERSONADECLARADA",
            ["EffectiveControlDescription"] = "ST_DESCRIPCIONCONTROLEFECTIVO_PERSONADECLARADA",
            ["HandlesCashOrFunds"] = "BO_MANEJAEFECTIVOFONDOS_PERSONADECLARADA",
            ["IsPEP"] = "BO_ESPEP_PERSONADECLARADA",
            ["PepType"] = "IN_TIPOPEP_PERSONADECLARADA",
            ["PepTypeName"] = "ST_NOMBRETIPOPEP_PERSONADECLARADA",
            ["PepInstitution"] = "ST_INSTITUCIONPEP_PERSONADECLARADA",
            ["PepPosition"] = "ST_CARGOPEP_PERSONADECLARADA",
            ["PepRelationship"] = "ST_RELACIONPEP_PERSONADECLARADA",
            ["PepObservation"] = "ST_OBSERVACIONPEP_PERSONADECLARADA",
            ["SortOrder"] = "IN_ORDEN_PERSONADECLARADA",
            ["CreatedAt"] = "DT_FECHACREACION_PERSONADECLARADA",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_PERSONADECLARADA",
        }),
        ["Declarants"] = ("T_DECLARANTE", new()
        {
            ["Id"] = "IN_COD_DECLARANTE",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["NationalityType"] = "IN_TIPONACIONALIDAD_DECLARANTE",
            ["IdNumber"] = "ST_NUMEROIDENTIFICACION_DECLARANTE",
            ["FirstName"] = "ST_NOMBRE_DECLARANTE",
            ["LastName1"] = "ST_APELLIDOPATERNO_DECLARANTE",
            ["LastName2"] = "ST_APELLIDOMATERNO_DECLARANTE",
            ["PlaceOfOrigin"] = "ST_LUGARORIGEN_DECLARANTE",
            ["RelationshipWithLegalEntity"] = "ST_RELACIONPERSONAJURIDICA_DECLARANTE",
            ["DeclaresUnderOath"] = "BO_DECLARABAJOJURAMENTO_DECLARANTE",
            ["City"] = "ST_CIUDAD_DECLARANTE",
            ["DeclarationDate"] = "DT_FECHADECLARACION_DECLARANTE",
            ["CreatedAt"] = "DT_FECHACREACION_DECLARANTE",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_DECLARANTE",
            ["Email"] = "ST_EMAIL_DECLARANTE",
            ["Phone"] = "ST_TELEFONO_DECLARANTE",
            ["SignatureDateTime"] = "DT_FECHAFIRMA_DECLARANTE",
            ["SignatureFullName"] = "ST_NOMBRECOMPLETOFIRMA_DECLARANTE",
            ["SignatureIdNumber"] = "ST_NUMEROIDENTIFICACIONFIRMA_DECLARANTE",
            ["SignatureIpAddress"] = "ST_DIRECCIONIPFIRMA_DECLARANTE",
            ["SignatureUserAgent"] = "ST_USERAGENTFIRMA_DECLARANTE",
        }),
        ["Documents"] = ("T_DOCUMENTO", new()
        {
            ["Id"] = "IN_COD_DOCUMENTO",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["DocumentType"] = "IN_TIPO_DOCUMENTO",
            ["DocumentTypeOther"] = "ST_TIPOOTRO_DOCUMENTO",
            ["OriginalFileName"] = "ST_NOMBREARCHIVOORIGINAL_DOCUMENTO",
            ["StoredFileName"] = "ST_NOMBREARCHIVOALMACENADO_DOCUMENTO",
            ["StoragePath"] = "ST_RUTAALMACENAMIENTO_DOCUMENTO",
            ["MimeType"] = "ST_TIPOMIME_DOCUMENTO",
            ["FileSizeBytes"] = "IN_TAMANOBYTES_DOCUMENTO",
            ["IsActive"] = "BO_ACTIVO_DOCUMENTO",
            ["Version"] = "IN_VERSION_DOCUMENTO",
            ["StorageProvider"] = "IN_PROVEEDORALMACENAMIENTO_DOCUMENTO",
            ["UploadedAt"] = "DT_FECHACARGA_DOCUMENTO",
            ["UploadedByIp"] = "ST_IPCARGA_DOCUMENTO",
        }),
        ["FileStorageRecords"] = ("T_ALMACENAMIENTOARCHIVO", new()
        {
            ["Id"] = "IN_COD_ALMACENAMIENTOARCHIVO",
            ["DocumentId"] = "IN_COD_DOCUMENTO",
            ["StorageProvider"] = "IN_PROVEEDORALMACENAMIENTO_ALMACENAMIENTOARCHIVO",
            ["StoragePath"] = "ST_RUTAALMACENAMIENTO_ALMACENAMIENTOARCHIVO",
            ["StorageKey"] = "ST_CLAVEALMACENAMIENTO_ALMACENAMIENTOARCHIVO",
            ["UploadedAt"] = "DT_FECHACARGA_ALMACENAMIENTOARCHIVO",
            ["Metadata"] = "ST_METADATOS_ALMACENAMIENTOARCHIVO",
        }),
        ["RequestStatusHistories"] = ("T_HISTORIALESTADOSOLICITUD", new()
        {
            ["Id"] = "IN_COD_HISTORIALESTADOSOLICITUD",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["OldStatus"] = "IN_ESTADOANTERIOR_HISTORIALESTADOSOLICITUD",
            ["NewStatus"] = "IN_ESTADONUEVO_HISTORIALESTADOSOLICITUD",
            ["ChangedBy"] = "ST_CAMBIADOPOR_HISTORIALESTADOSOLICITUD",
            ["Notes"] = "ST_NOTAS_HISTORIALESTADOSOLICITUD",
            ["ChangedAt"] = "DT_FECHACAMBIO_HISTORIALESTADOSOLICITUD",
        }),
        ["AuditLogs"] = ("T_AUDITORIA", new()
        {
            ["Id"] = "IN_COD_AUDITORIA",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["Action"] = "ST_ACCION_AUDITORIA",
            ["EntityType"] = "ST_TIPOENTIDAD_AUDITORIA",
            ["EntityId"] = "ST_CODENTIDAD_AUDITORIA",
            ["OldValues"] = "ST_VALORESANTERIORES_AUDITORIA",
            ["NewValues"] = "ST_VALORESNUEVOS_AUDITORIA",
            ["UserId"] = "IN_COD_USUARIO",
            ["UserName"] = "ST_NOMBREUSUARIO_AUDITORIA",
            ["IpAddress"] = "ST_DIRECCIONIP_AUDITORIA",
            ["UserAgent"] = "ST_USERAGENT_AUDITORIA",
            ["CreatedAt"] = "DT_FECHACREACION_AUDITORIA",
        }),
        ["Notifications"] = ("T_NOTIFICACION", new()
        {
            ["Id"] = "IN_COD_NOTIFICACION",
            ["UserId"] = "IN_COD_USUARIO",
            ["Type"] = "IN_TIPO_NOTIFICACION",
            ["Message"] = "ST_MENSAJE_NOTIFICACION",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["IsRead"] = "BO_LEIDA_NOTIFICACION",
            ["ReadAt"] = "DT_FECHALECTURA_NOTIFICACION",
            ["CreatedAt"] = "DT_FECHACREACION_NOTIFICACION",
        }),
        ["SignatureRecords"] = ("T_FIRMA", new()
        {
            ["Id"] = "IN_COD_FIRMA",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["Type"] = "IN_TIPO_FIRMA",
            ["Status"] = "IN_ESTADO_FIRMA",
            ["SignerName"] = "ST_NOMBREFIRMANTE_FIRMA",
            ["SignerIdNumber"] = "ST_NUMEROIDENTIFICACIONFIRMANTE_FIRMA",
            ["SignedAt"] = "DT_FECHAFIRMA_FIRMA",
            ["IpAddress"] = "ST_DIRECCIONIP_FIRMA",
            ["UserAgent"] = "ST_USERAGENT_FIRMA",
            ["DocumentHash"] = "ST_HASHDOCUMENTO_FIRMA",
            ["FolioNumber"] = "ST_NUMEROFOLIO_FIRMA",
            ["ProviderTransactionId"] = "ST_CODTRANSACCIONPROVEEDOR_FIRMA",
            ["SignatureUrl"] = "ST_URL_FIRMA",
            ["Certificate"] = "ST_CERTIFICADO_FIRMA",
            ["SignedPdfPath"] = "ST_RUTAPDFFIRMADO_FIRMA",
            ["CreatedAt"] = "DT_FECHACREACION_FIRMA",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_FIRMA",
        }),
        ["TaxFolderAnalyses"] = ("T_ANALISISCARPETATRIBUTARIA", new()
        {
            ["Id"] = "IN_COD_ANALISISCARPETATRIBUTARIA",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["ExtractedRut"] = "ST_RUTEXTRAIDO_ANALISISCARPETATRIBUTARIA",
            ["ExtractedBusinessName"] = "ST_RAZONSOCIALEXTRAIDA_ANALISISCARPETATRIBUTARIA",
            ["ExtractedAddress"] = "ST_DIRECCIONEXTRAIDA_ANALISISCARPETATRIBUTARIA",
            ["ExtractedActivity"] = "ST_ACTIVIDADEXTRAIDA_ANALISISCARPETATRIBUTARIA",
            ["ExtractedLegalRep"] = "ST_REPRESENTANTELEGALEXTRAIDO_ANALISISCARPETATRIBUTARIA",
            ["ExtractedIssuedDate"] = "ST_FECHAEMISIONEXTRAIDA_ANALISISCARPETATRIBUTARIA",
            ["RawText"] = "ST_TEXTOBRUTO_ANALISISCARPETATRIBUTARIA",
            ["WasReadable"] = "BO_LEGIBLE_ANALISISCARPETATRIBUTARIA",
            ["AnalyzedAt"] = "DT_FECHAANALISIS_ANALISISCARPETATRIBUTARIA",
        }),
        ["TaxFolderAlerts"] = ("T_ALERTACARPETATRIBUTARIA", new()
        {
            ["Id"] = "IN_COD_ALERTACARPETATRIBUTARIA",
            ["TaxFolderAnalysisId"] = "IN_COD_ANALISISCARPETATRIBUTARIA",
            ["AlertType"] = "IN_TIPO_ALERTACARPETATRIBUTARIA",
            ["Message"] = "ST_MENSAJE_ALERTACARPETATRIBUTARIA",
            ["Severity"] = "ST_SEVERIDAD_ALERTACARPETATRIBUTARIA",
            ["IsResolved"] = "BO_RESUELTA_ALERTACARPETATRIBUTARIA",
        }),
        ["BeneficialOwners"] = ("T_BENEFICIARIOFINAL", new()
        {
            ["Id"] = "IN_COD_BENEFICIARIOFINAL",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["IdNumber"] = "ST_NUMEROIDENTIFICACION_BENEFICIARIOFINAL",
            ["FullName"] = "ST_NOMBRECOMPLETO_BENEFICIARIOFINAL",
            ["Address"] = "ST_DIRECCION_BENEFICIARIOFINAL",
            ["City"] = "ST_CIUDAD_BENEFICIARIOFINAL",
            ["Country"] = "ST_PAIS_BENEFICIARIOFINAL",
            ["ParticipationPercentage"] = "NU_PORCENTAJEPARTICIPACION_BENEFICIARIOFINAL",
            ["IsBeneficialOwner"] = "BO_ESBENEFICIARIOFINAL_BENEFICIARIOFINAL",
            ["IsEffectiveControl"] = "BO_ESCONTROLEFECTIVO_BENEFICIARIOFINAL",
            ["IsPEP"] = "BO_ESPEP_BENEFICIARIOFINAL",
            ["PepDetail"] = "ST_DETALLEPEP_BENEFICIARIOFINAL",
            ["SortOrder"] = "IN_ORDEN_BENEFICIARIOFINAL",
            ["CreatedAt"] = "DT_FECHACREACION_BENEFICIARIOFINAL",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_BENEFICIARIOFINAL",
        }),
        ["EffectiveControllers"] = ("T_CONTROLADOREFECTIVO", new()
        {
            ["Id"] = "IN_COD_CONTROLADOREFECTIVO",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["IdNumber"] = "ST_NUMEROIDENTIFICACION_CONTROLADOREFECTIVO",
            ["FullName"] = "ST_NOMBRECOMPLETO_CONTROLADOREFECTIVO",
            ["Address"] = "ST_DIRECCION_CONTROLADOREFECTIVO",
            ["City"] = "ST_CIUDAD_CONTROLADOREFECTIVO",
            ["Country"] = "ST_PAIS_CONTROLADOREFECTIVO",
            ["ParticipationPercentage"] = "NU_PORCENTAJEPARTICIPACION_CONTROLADOREFECTIVO",
            ["IsPEP"] = "BO_ESPEP_CONTROLADOREFECTIVO",
            ["PepDetail"] = "ST_DETALLEPEP_CONTROLADOREFECTIVO",
            ["ControlDescription"] = "ST_DESCRIPCIONCONTROL_CONTROLADOREFECTIVO",
            ["SortOrder"] = "IN_ORDEN_CONTROLADOREFECTIVO",
            ["CreatedAt"] = "DT_FECHACREACION_CONTROLADOREFECTIVO",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_CONTROLADOREFECTIVO",
        }),
        ["PepDeclarations"] = ("T_DECLARACIONPEP", new()
        {
            ["Id"] = "IN_COD_DECLARACIONPEP",
            ["RequestId"] = "IN_COD_SOLICITUD",
            ["DeclarantName"] = "ST_NOMBREDECLARANTE_DECLARACIONPEP",
            ["IdNumber"] = "ST_NUMEROIDENTIFICACION_DECLARACIONPEP",
            ["Nationality"] = "ST_NACIONALIDAD_DECLARACIONPEP",
            ["DeclaresPEP"] = "BO_DECLARAPEP_DECLARACIONPEP",
            ["PepName"] = "ST_NOMBREPEP_DECLARACIONPEP",
            ["Institution"] = "ST_INSTITUCION_DECLARACIONPEP",
            ["PepReasonType"] = "IN_MOTIVOPEP_DECLARACIONPEP",
            ["PepReasonOther"] = "ST_MOTIVOPEPOTRO_DECLARACIONPEP",
            ["VinculoType"] = "ST_TIPOVINCULO_DECLARACIONPEP",
            ["DeclarationDate"] = "DT_FECHADECLARACION_DECLARACIONPEP",
            ["AcceptsUnderOath"] = "BO_ACEPTABAJOJURAMENTO_DECLARACIONPEP",
            ["SignatureFullName"] = "ST_NOMBRECOMPLETOFIRMA_DECLARACIONPEP",
            ["SignatureIdNumber"] = "ST_NUMEROIDENTIFICACIONFIRMA_DECLARACIONPEP",
            ["SignatureDateTime"] = "DT_FECHAFIRMA_DECLARACIONPEP",
            ["SignatureIpAddress"] = "ST_DIRECCIONIPFIRMA_DECLARACIONPEP",
            ["SignatureUserAgent"] = "ST_USERAGENTFIRMA_DECLARACIONPEP",
            ["CreatedAt"] = "DT_FECHACREACION_DECLARACIONPEP",
            ["UpdatedAt"] = "DT_FECHAMODIFICACION_DECLARACIONPEP",
        }),
        ["TiposDocumento"] = ("T_TIPODOCUMENTO", new()
        {
            ["Id"] = "IN_COD_TIPODOCUMENTO",
            ["Nombre"] = "ST_NOMBRE_TIPODOCUMENTO",
            ["Orden"] = "IN_ORDEN_TIPODOCUMENTO",
            ["Activo"] = "BO_ACTIVO_TIPODOCUMENTO",
        }),
        ["Paises"] = ("T_PAIS", new()
        {
            ["Id"] = "IN_COD_PAIS",
            ["Nombre"] = "ST_NOMBRE_PAIS",
            ["Orden"] = "IN_ORDEN_PAIS",
            ["Activo"] = "BO_ACTIVO_PAIS",
        }),
        ["Nacionalidades"] = ("T_NACIONALIDAD", new()
        {
            ["Id"] = "IN_COD_NACIONALIDAD",
            ["Nombre"] = "ST_NOMBRE_NACIONALIDAD",
            ["Orden"] = "IN_ORDEN_NACIONALIDAD",
            ["Activo"] = "BO_ACTIVO_NACIONALIDAD",
        }),
        ["GestionesOperacion"] = ("T_GESTIONOPERACION", new()
        {
            ["Id"] = "IN_COD_GESTIONOPERACION",
            ["OperacionId"] = "IN_CODOPERACIONREGCHEQ_GESTIONOPERACION",
            ["CodigoOperacion"] = "IN_CODIGOOPERACION_GESTIONOPERACION",
            ["ClienteRut"] = "ST_RUTCLIENTE_GESTIONOPERACION",
            ["ClienteNombre"] = "ST_NOMBRECLIENTE_GESTIONOPERACION",
            ["UsuarioId"] = "IN_COD_USUARIO",
            ["FechaToma"] = "DT_FECHATOMA_GESTIONOPERACION",
            ["CreatedAt"] = "DT_FECHACREACION_GESTIONOPERACION",
            ["FechaCierre"] = "DT_FECHACIERRE_GESTIONOPERACION",
        }),
        ["GestionesOperacionNotas"] = ("T_GESTIONOPERACIONNOTA", new()
        {
            ["Id"] = "IN_COD_GESTIONOPERACIONNOTA",
            ["GestionOperacionId"] = "IN_COD_GESTIONOPERACION",
            ["UsuarioId"] = "IN_COD_USUARIO",
            ["UsuarioNombre"] = "ST_NOMBREUSUARIO_GESTIONOPERACIONNOTA",
            ["Texto"] = "ST_TEXTO_GESTIONOPERACIONNOTA",
            ["EsSistema"] = "BO_SISTEMA_GESTIONOPERACIONNOTA",
            ["CreatedAt"] = "DT_FECHACREACION_GESTIONOPERACIONNOTA",
        }),
    };

    public static void Apply(ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var logicalName = entity.GetTableName();
            if (logicalName is null) continue;

            if (!Map.TryGetValue(logicalName, out var map))
                throw new InvalidOperationException(
                    $"La tabla '{logicalName}' no tiene nombre definido en {nameof(DatabaseNaming)}.");

            entity.SetTableName(map.Table);

            foreach (var property in entity.GetProperties())
            {
                if (!map.Columns.TryGetValue(property.Name, out var column))
                    throw new InvalidOperationException(
                        $"La propiedad '{logicalName}.{property.Name}' no tiene nombre de columna definido en {nameof(DatabaseNaming)}.");

                property.SetColumnName(column);
            }
        }
    }
}

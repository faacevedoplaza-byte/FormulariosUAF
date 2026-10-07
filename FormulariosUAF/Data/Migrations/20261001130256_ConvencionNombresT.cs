using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvencionNombresT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Renombrado a la convención T_ENTIDAD / PREFIJO_DESCRIPCION_ENTIDAD.
            // Solo cambia nombres: no toca datos, tipos ni relaciones.
            // Tablas, columnas, PK, FK e índices se renombran con sp_rename.
            // Excepción: UserNameIndex y RoleNameIndex se eliminan y recrean porque SQL Server
            // no permite renombrar una columna usada en el filtro de un índice filtrado.

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UsuariosTokens",
                newName: "T_USUARIOTOKEN");

            migrationBuilder.RenameTable(
                name: "UsuariosRoles",
                newName: "T_USUARIOROL");

            migrationBuilder.RenameTable(
                name: "UsuariosLogins",
                newName: "T_USUARIOLOGIN");

            migrationBuilder.RenameTable(
                name: "UsuariosClaims",
                newName: "T_USUARIOCLAIM");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "T_USUARIO");

            migrationBuilder.RenameTable(
                name: "TaxFolderAnalyses",
                newName: "T_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameTable(
                name: "TaxFolderAlerts",
                newName: "T_ALERTACARPETATRIBUTARIA");

            migrationBuilder.RenameTable(
                name: "SignatureRecords",
                newName: "T_FIRMA");

            migrationBuilder.RenameTable(
                name: "RolesClaims",
                newName: "T_ROLCLAIM");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "T_ROL");

            migrationBuilder.RenameTable(
                name: "RequestStatusHistories",
                newName: "T_HISTORIALESTADOSOLICITUD");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "T_SOLICITUD");

            migrationBuilder.RenameTable(
                name: "PepDeclarations",
                newName: "T_DECLARACIONPEP");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "T_NOTIFICACION");

            migrationBuilder.RenameTable(
                name: "LegalEntityDeclarations",
                newName: "T_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameTable(
                name: "FileStorageRecords",
                newName: "T_ALMACENAMIENTOARCHIVO");

            migrationBuilder.RenameTable(
                name: "EffectiveControllers",
                newName: "T_CONTROLADOREFECTIVO");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "T_DOCUMENTO");

            migrationBuilder.RenameTable(
                name: "DeclaredPersons",
                newName: "T_PERSONADECLARADA");

            migrationBuilder.RenameTable(
                name: "Declarants",
                newName: "T_DECLARANTE");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "T_CLIENTE");

            migrationBuilder.RenameTable(
                name: "BeneficialOwners",
                newName: "T_BENEFICIARIOFINAL");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "T_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "T_USUARIOTOKEN",
                newName: "ST_VALOR_USUARIOTOKEN");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "T_USUARIOTOKEN",
                newName: "ST_NOMBRE_USUARIOTOKEN");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "T_USUARIOTOKEN",
                newName: "ST_PROVEEDORLOGIN_USUARIOTOKEN");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "T_USUARIOTOKEN",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "T_USUARIOROL",
                newName: "IN_COD_ROL");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "T_USUARIOROL",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosRoles_RoleId",
                table: "T_USUARIOROL",
                newName: "IX_T_USUARIOROL_IN_COD_ROL");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "T_USUARIOLOGIN",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "T_USUARIOLOGIN",
                newName: "ST_NOMBREVISIBLEPROVEEDOR_USUARIOLOGIN");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "T_USUARIOLOGIN",
                newName: "ST_CLAVEPROVEEDOR_USUARIOLOGIN");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "T_USUARIOLOGIN",
                newName: "ST_PROVEEDORLOGIN_USUARIOLOGIN");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosLogins_UserId",
                table: "T_USUARIOLOGIN",
                newName: "IX_T_USUARIOLOGIN_IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "T_USUARIOCLAIM",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "T_USUARIOCLAIM",
                newName: "ST_VALORCLAIM_USUARIOCLAIM");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "T_USUARIOCLAIM",
                newName: "ST_TIPOCLAIM_USUARIOCLAIM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_USUARIOCLAIM",
                newName: "IN_COD_USUARIOCLAIM");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosClaims_UserId",
                table: "T_USUARIOCLAIM",
                newName: "IX_T_USUARIOCLAIM_IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "VendorCode",
                table: "T_USUARIO",
                newName: "ST_CODVENDEDOR_USUARIO");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "T_USUARIO",
                newName: "ST_NOMBREUSUARIO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "T_USUARIO",
                newName: "BO_DOBLEFACTORHABILITADO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "T_USUARIO",
                newName: "ST_SELLOSEGURIDAD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "Rut",
                table: "T_USUARIO",
                newName: "ST_RUT_USUARIO");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "T_USUARIO",
                newName: "BO_TELEFONOCONFIRMADO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "T_USUARIO",
                newName: "ST_TELEFONO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "T_USUARIO",
                newName: "ST_HASHCONTRASENA_USUARIO");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "T_USUARIO",
                newName: "ST_NOMBREUSUARIONORMALIZADO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "T_USUARIO",
                newName: "ST_EMAILNORMALIZADO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "T_USUARIO",
                newName: "ST_NOMBRE_USUARIO");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "T_USUARIO",
                newName: "DT_FECHAFINBLOQUEO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "T_USUARIO",
                newName: "BO_BLOQUEOHABILITADO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "LastLoginAt",
                table: "T_USUARIO",
                newName: "DT_FECHAULTIMOLOGIN_USUARIO");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "T_USUARIO",
                newName: "BO_ACTIVO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "T_USUARIO",
                newName: "ST_NOMBRECOMPLETO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "T_USUARIO",
                newName: "BO_EMAILCONFIRMADO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "T_USUARIO",
                newName: "ST_EMAIL_USUARIO");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_USUARIO",
                newName: "DT_FECHACREACION_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "T_USUARIO",
                newName: "ST_SELLOCONCURRENCIA_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ApellidoPaterno",
                table: "T_USUARIO",
                newName: "ST_APELLIDOPATERNO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ApellidoMaterno",
                table: "T_USUARIO",
                newName: "ST_APELLIDOMATERNO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "T_USUARIO",
                newName: "IN_CANTIDADACCESOSFALLIDOS_USUARIO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_USUARIO",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "WasReadable",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "BO_LEGIBLE_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "RawText",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "ST_TEXTOBRUTO_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "ExtractedRut",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "ST_RUTEXTRAIDO_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "ExtractedLegalRep",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "ST_REPRESENTANTELEGALEXTRAIDO_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "ExtractedIssuedDate",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "ST_FECHAEMISIONEXTRAIDA_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "ExtractedBusinessName",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "ST_RAZONSOCIALEXTRAIDA_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "ExtractedAddress",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "ST_DIRECCIONEXTRAIDA_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "ExtractedActivity",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "ST_ACTIVIDADEXTRAIDA_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "AnalyzedAt",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "DT_FECHAANALISIS_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "IN_COD_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameIndex(
                name: "IX_TaxFolderAnalyses_RequestId",
                table: "T_ANALISISCARPETATRIBUTARIA",
                newName: "IX_T_ANALISISCARPETATRIBUTARIA_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "TaxFolderAnalysisId",
                table: "T_ALERTACARPETATRIBUTARIA",
                newName: "IN_COD_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "T_ALERTACARPETATRIBUTARIA",
                newName: "ST_SEVERIDAD_ALERTACARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "T_ALERTACARPETATRIBUTARIA",
                newName: "ST_MENSAJE_ALERTACARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "IsResolved",
                table: "T_ALERTACARPETATRIBUTARIA",
                newName: "BO_RESUELTA_ALERTACARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "AlertType",
                table: "T_ALERTACARPETATRIBUTARIA",
                newName: "IN_TIPO_ALERTACARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_ALERTACARPETATRIBUTARIA",
                newName: "IN_COD_ALERTACARPETATRIBUTARIA");

            migrationBuilder.RenameIndex(
                name: "IX_TaxFolderAlerts_TaxFolderAnalysisId",
                table: "T_ALERTACARPETATRIBUTARIA",
                newName: "IX_T_ALERTACARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA");

            migrationBuilder.RenameColumn(
                name: "UserAgent",
                table: "T_FIRMA",
                newName: "ST_USERAGENT_FIRMA");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_FIRMA",
                newName: "DT_FECHAMODIFICACION_FIRMA");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "T_FIRMA",
                newName: "IN_TIPO_FIRMA");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "T_FIRMA",
                newName: "IN_ESTADO_FIRMA");

            migrationBuilder.RenameColumn(
                name: "SignerName",
                table: "T_FIRMA",
                newName: "ST_NOMBREFIRMANTE_FIRMA");

            migrationBuilder.RenameColumn(
                name: "SignerIdNumber",
                table: "T_FIRMA",
                newName: "ST_NUMEROIDENTIFICACIONFIRMANTE_FIRMA");

            migrationBuilder.RenameColumn(
                name: "SignedPdfPath",
                table: "T_FIRMA",
                newName: "ST_RUTAPDFFIRMADO_FIRMA");

            migrationBuilder.RenameColumn(
                name: "SignedAt",
                table: "T_FIRMA",
                newName: "DT_FECHAFIRMA_FIRMA");

            migrationBuilder.RenameColumn(
                name: "SignatureUrl",
                table: "T_FIRMA",
                newName: "ST_URL_FIRMA");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_FIRMA",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "ProviderTransactionId",
                table: "T_FIRMA",
                newName: "ST_CODTRANSACCIONPROVEEDOR_FIRMA");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "T_FIRMA",
                newName: "ST_DIRECCIONIP_FIRMA");

            migrationBuilder.RenameColumn(
                name: "FolioNumber",
                table: "T_FIRMA",
                newName: "ST_NUMEROFOLIO_FIRMA");

            migrationBuilder.RenameColumn(
                name: "DocumentHash",
                table: "T_FIRMA",
                newName: "ST_HASHDOCUMENTO_FIRMA");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_FIRMA",
                newName: "DT_FECHACREACION_FIRMA");

            migrationBuilder.RenameColumn(
                name: "Certificate",
                table: "T_FIRMA",
                newName: "ST_CERTIFICADO_FIRMA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_FIRMA",
                newName: "IN_COD_FIRMA");

            migrationBuilder.RenameIndex(
                name: "IX_SignatureRecords_RequestId",
                table: "T_FIRMA",
                newName: "IX_T_FIRMA_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "T_ROLCLAIM",
                newName: "IN_COD_ROL");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "T_ROLCLAIM",
                newName: "ST_VALORCLAIM_ROLCLAIM");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "T_ROLCLAIM",
                newName: "ST_TIPOCLAIM_ROLCLAIM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_ROLCLAIM",
                newName: "IN_COD_ROLCLAIM");

            migrationBuilder.RenameIndex(
                name: "IX_RolesClaims_RoleId",
                table: "T_ROLCLAIM",
                newName: "IX_T_ROLCLAIM_IN_COD_ROL");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "T_ROL",
                newName: "ST_NOMBRENORMALIZADO_ROL");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "T_ROL",
                newName: "ST_NOMBRE_ROL");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "T_ROL",
                newName: "ST_SELLOCONCURRENCIA_ROL");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_ROL",
                newName: "IN_COD_ROL");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "OldStatus",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "IN_ESTADOANTERIOR_HISTORIALESTADOSOLICITUD");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "ST_NOTAS_HISTORIALESTADOSOLICITUD");

            migrationBuilder.RenameColumn(
                name: "NewStatus",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "IN_ESTADONUEVO_HISTORIALESTADOSOLICITUD");

            migrationBuilder.RenameColumn(
                name: "ChangedBy",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "ST_CAMBIADOPOR_HISTORIALESTADOSOLICITUD");

            migrationBuilder.RenameColumn(
                name: "ChangedAt",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "DT_FECHACAMBIO_HISTORIALESTADOSOLICITUD");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "IN_COD_HISTORIALESTADOSOLICITUD");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStatusHistories_RequestId",
                table: "T_HISTORIALESTADOSOLICITUD",
                newName: "IX_T_HISTORIALESTADOSOLICITUD_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "VendorUserId",
                table: "T_SOLICITUD",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "T_SOLICITUD",
                newName: "ST_MODIFICADOPOR_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_SOLICITUD",
                newName: "DT_FECHAMODIFICACION_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "TokenExpiry",
                table: "T_SOLICITUD",
                newName: "DT_FECHAVENCIMIENTOTOKEN_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "T_SOLICITUD",
                newName: "IN_ESTADO_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "T_SOLICITUD",
                newName: "DT_FECHAENVIO_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "RequestType",
                table: "T_SOLICITUD",
                newName: "IN_TIPO_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "RequestNumber",
                table: "T_SOLICITUD",
                newName: "ST_NUMERO_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "OpenedAt",
                table: "T_SOLICITUD",
                newName: "DT_FECHAAPERTURA_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "NetcarBusinessNumber",
                table: "T_SOLICITUD",
                newName: "ST_NUMERONEGOCIONETCAR_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "T_SOLICITUD",
                newName: "BO_ELIMINADO_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "InternalNotes",
                table: "T_SOLICITUD",
                newName: "ST_NOTASINTERNAS_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "T_SOLICITUD",
                newName: "DT_FECHAVENCIMIENTO_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "CurrentStep",
                table: "T_SOLICITUD",
                newName: "IN_PASOACTUAL_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "T_SOLICITUD",
                newName: "ST_CREADOPOR_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_SOLICITUD",
                newName: "DT_FECHACREACION_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                table: "T_SOLICITUD",
                newName: "DT_FECHACOMPLETADA_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "ClientToken",
                table: "T_SOLICITUD",
                newName: "ST_TOKENCLIENTE_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "ClientPhone",
                table: "T_SOLICITUD",
                newName: "ST_TELEFONOCLIENTE_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "T_SOLICITUD",
                newName: "IN_COD_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "ClientEmail",
                table: "T_SOLICITUD",
                newName: "ST_EMAILCLIENTE_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_SOLICITUD",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_VendorUserId",
                table: "T_SOLICITUD",
                newName: "IX_T_SOLICITUD_IN_COD_USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_Status",
                table: "T_SOLICITUD",
                newName: "IX_T_SOLICITUD_IN_ESTADO_SOLICITUD");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_RequestNumber",
                table: "T_SOLICITUD",
                newName: "IX_T_SOLICITUD_ST_NUMERO_SOLICITUD");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_CreatedAt",
                table: "T_SOLICITUD",
                newName: "IX_T_SOLICITUD_DT_FECHACREACION_SOLICITUD");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ClientToken",
                table: "T_SOLICITUD",
                newName: "IX_T_SOLICITUD_ST_TOKENCLIENTE_SOLICITUD");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ClientId",
                table: "T_SOLICITUD",
                newName: "IX_T_SOLICITUD_IN_COD_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "VinculoType",
                table: "T_DECLARACIONPEP",
                newName: "ST_TIPOVINCULO_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_DECLARACIONPEP",
                newName: "DT_FECHAMODIFICACION_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "SignatureUserAgent",
                table: "T_DECLARACIONPEP",
                newName: "ST_USERAGENTFIRMA_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "SignatureIpAddress",
                table: "T_DECLARACIONPEP",
                newName: "ST_DIRECCIONIPFIRMA_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "SignatureIdNumber",
                table: "T_DECLARACIONPEP",
                newName: "ST_NUMEROIDENTIFICACIONFIRMA_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "SignatureFullName",
                table: "T_DECLARACIONPEP",
                newName: "ST_NOMBRECOMPLETOFIRMA_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "SignatureDateTime",
                table: "T_DECLARACIONPEP",
                newName: "DT_FECHAFIRMA_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_DECLARACIONPEP",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "PepReasonType",
                table: "T_DECLARACIONPEP",
                newName: "IN_MOTIVOPEP_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "PepReasonOther",
                table: "T_DECLARACIONPEP",
                newName: "ST_MOTIVOPEPOTRO_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "PepName",
                table: "T_DECLARACIONPEP",
                newName: "ST_NOMBREPEP_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "T_DECLARACIONPEP",
                newName: "ST_NACIONALIDAD_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "T_DECLARACIONPEP",
                newName: "ST_INSTITUCION_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "IdNumber",
                table: "T_DECLARACIONPEP",
                newName: "ST_NUMEROIDENTIFICACION_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "DeclaresPEP",
                table: "T_DECLARACIONPEP",
                newName: "BO_DECLARAPEP_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "DeclarationDate",
                table: "T_DECLARACIONPEP",
                newName: "DT_FECHADECLARACION_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "DeclarantName",
                table: "T_DECLARACIONPEP",
                newName: "ST_NOMBREDECLARANTE_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_DECLARACIONPEP",
                newName: "DT_FECHACREACION_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "AcceptsUnderOath",
                table: "T_DECLARACIONPEP",
                newName: "BO_ACEPTABAJOJURAMENTO_DECLARACIONPEP");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_DECLARACIONPEP",
                newName: "IN_COD_DECLARACIONPEP");

            migrationBuilder.RenameIndex(
                name: "IX_PepDeclarations_RequestId",
                table: "T_DECLARACIONPEP",
                newName: "IX_T_DECLARACIONPEP_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "T_NOTIFICACION",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "T_NOTIFICACION",
                newName: "IN_TIPO_NOTIFICACION");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_NOTIFICACION",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "ReadAt",
                table: "T_NOTIFICACION",
                newName: "DT_FECHALECTURA_NOTIFICACION");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "T_NOTIFICACION",
                newName: "ST_MENSAJE_NOTIFICACION");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "T_NOTIFICACION",
                newName: "BO_LEIDA_NOTIFICACION");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_NOTIFICACION",
                newName: "DT_FECHACREACION_NOTIFICACION");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_NOTIFICACION",
                newName: "IN_COD_NOTIFICACION");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "T_NOTIFICACION",
                newName: "IX_T_NOTIFICACION_IN_COD_USUARIO_BO_LEIDA_NOTIFICACION");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "T_NOTIFICACION",
                newName: "IX_T_NOTIFICACION_IN_COD_USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_CreatedAt",
                table: "T_NOTIFICACION",
                newName: "IX_T_NOTIFICACION_DT_FECHACREACION_NOTIFICACION");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "DT_FECHAMODIFICACION_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "RUT",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_RUT_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_TELEFONO_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "LegalRepresentativeName",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_NOMBREREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "LegalRepresentativeIdNumber",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_NUMEROIDENTIFICACIONREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "EntityTypeOther",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_TIPOENTIDADOTRO_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "IN_TIPOENTIDAD_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "DT_FECHACREACION_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "CountryOfIncorporation",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_PAISCONSTITUCION_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_CIUDAD_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "BusinessName",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_RAZONSOCIAL_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "ST_DIRECCION_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "IN_COD_DECLARACIONPERSONAJURIDICA");

            migrationBuilder.RenameIndex(
                name: "IX_LegalEntityDeclarations_RequestId",
                table: "T_DECLARACIONPERSONAJURIDICA",
                newName: "IX_T_DECLARACIONPERSONAJURIDICA_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "DT_FECHACARGA_ALMACENAMIENTOARCHIVO");

            migrationBuilder.RenameColumn(
                name: "StorageProvider",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "IN_PROVEEDORALMACENAMIENTO_ALMACENAMIENTOARCHIVO");

            migrationBuilder.RenameColumn(
                name: "StoragePath",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "ST_RUTAALMACENAMIENTO_ALMACENAMIENTOARCHIVO");

            migrationBuilder.RenameColumn(
                name: "StorageKey",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "ST_CLAVEALMACENAMIENTO_ALMACENAMIENTOARCHIVO");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "ST_METADATOS_ALMACENAMIENTOARCHIVO");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "IN_COD_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "IN_COD_ALMACENAMIENTOARCHIVO");

            migrationBuilder.RenameIndex(
                name: "IX_FileStorageRecords_DocumentId",
                table: "T_ALMACENAMIENTOARCHIVO",
                newName: "IX_T_ALMACENAMIENTOARCHIVO_IN_COD_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_CONTROLADOREFECTIVO",
                newName: "DT_FECHAMODIFICACION_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "T_CONTROLADOREFECTIVO",
                newName: "IN_ORDEN_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_CONTROLADOREFECTIVO",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "PepDetail",
                table: "T_CONTROLADOREFECTIVO",
                newName: "ST_DETALLEPEP_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "ParticipationPercentage",
                table: "T_CONTROLADOREFECTIVO",
                newName: "NU_PORCENTAJEPARTICIPACION_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "IsPEP",
                table: "T_CONTROLADOREFECTIVO",
                newName: "BO_ESPEP_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "IdNumber",
                table: "T_CONTROLADOREFECTIVO",
                newName: "ST_NUMEROIDENTIFICACION_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "T_CONTROLADOREFECTIVO",
                newName: "ST_NOMBRECOMPLETO_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_CONTROLADOREFECTIVO",
                newName: "DT_FECHACREACION_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "T_CONTROLADOREFECTIVO",
                newName: "ST_PAIS_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "ControlDescription",
                table: "T_CONTROLADOREFECTIVO",
                newName: "ST_DESCRIPCIONCONTROL_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "T_CONTROLADOREFECTIVO",
                newName: "ST_CIUDAD_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "T_CONTROLADOREFECTIVO",
                newName: "ST_DIRECCION_CONTROLADOREFECTIVO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_CONTROLADOREFECTIVO",
                newName: "IN_COD_CONTROLADOREFECTIVO");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveControllers_RequestId",
                table: "T_CONTROLADOREFECTIVO",
                newName: "IX_T_CONTROLADOREFECTIVO_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "T_DOCUMENTO",
                newName: "IN_VERSION_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "UploadedByIp",
                table: "T_DOCUMENTO",
                newName: "ST_IPCARGA_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "T_DOCUMENTO",
                newName: "DT_FECHACARGA_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "StoredFileName",
                table: "T_DOCUMENTO",
                newName: "ST_NOMBREARCHIVOALMACENADO_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "StorageProvider",
                table: "T_DOCUMENTO",
                newName: "IN_PROVEEDORALMACENAMIENTO_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "StoragePath",
                table: "T_DOCUMENTO",
                newName: "ST_RUTAALMACENAMIENTO_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_DOCUMENTO",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "OriginalFileName",
                table: "T_DOCUMENTO",
                newName: "ST_NOMBREARCHIVOORIGINAL_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "MimeType",
                table: "T_DOCUMENTO",
                newName: "ST_TIPOMIME_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "T_DOCUMENTO",
                newName: "BO_ACTIVO_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "FileSizeBytes",
                table: "T_DOCUMENTO",
                newName: "IN_TAMANOBYTES_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeOther",
                table: "T_DOCUMENTO",
                newName: "ST_TIPOOTRO_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "T_DOCUMENTO",
                newName: "IN_TIPO_DOCUMENTO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_DOCUMENTO",
                newName: "IN_COD_DOCUMENTO");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_RequestId",
                table: "T_DOCUMENTO",
                newName: "IX_T_DOCUMENTO_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_PERSONADECLARADA",
                newName: "DT_FECHAMODIFICACION_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "T_PERSONADECLARADA",
                newName: "IN_ORDEN_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_PERSONADECLARADA",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "RelationshipTypeOther",
                table: "T_PERSONADECLARADA",
                newName: "ST_TIPORELACIONOTRO_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "RelationshipType",
                table: "T_PERSONADECLARADA",
                newName: "IN_TIPORELACION_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "PepTypeName",
                table: "T_PERSONADECLARADA",
                newName: "ST_NOMBRETIPOPEP_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "PepType",
                table: "T_PERSONADECLARADA",
                newName: "IN_TIPOPEP_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "PepRelationship",
                table: "T_PERSONADECLARADA",
                newName: "ST_RELACIONPEP_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "PepPosition",
                table: "T_PERSONADECLARADA",
                newName: "ST_CARGOPEP_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "PepObservation",
                table: "T_PERSONADECLARADA",
                newName: "ST_OBSERVACIONPEP_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "PepInstitution",
                table: "T_PERSONADECLARADA",
                newName: "ST_INSTITUCIONPEP_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "ParticipationPercentage",
                table: "T_PERSONADECLARADA",
                newName: "NU_PORCENTAJEPARTICIPACION_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "IsPEP",
                table: "T_PERSONADECLARADA",
                newName: "BO_ESPEP_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "IsEffectiveController",
                table: "T_PERSONADECLARADA",
                newName: "BO_ESCONTROLADOREFECTIVO_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "IdNumber",
                table: "T_PERSONADECLARADA",
                newName: "ST_NUMEROIDENTIFICACION_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "HasMinTenPercentParticipation",
                table: "T_PERSONADECLARADA",
                newName: "BO_PARTICIPACIONMINDIEZPORCIENTO_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "HandlesCashOrFunds",
                table: "T_PERSONADECLARADA",
                newName: "BO_MANEJAEFECTIVOFONDOS_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "T_PERSONADECLARADA",
                newName: "ST_NOMBRECOMPLETO_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "EffectiveControlDescription",
                table: "T_PERSONADECLARADA",
                newName: "ST_DESCRIPCIONCONTROLEFECTIVO_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_PERSONADECLARADA",
                newName: "DT_FECHACREACION_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "T_PERSONADECLARADA",
                newName: "ST_PAIS_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "T_PERSONADECLARADA",
                newName: "ST_CIUDAD_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "T_PERSONADECLARADA",
                newName: "ST_DIRECCION_PERSONADECLARADA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_PERSONADECLARADA",
                newName: "IN_COD_PERSONADECLARADA");

            migrationBuilder.RenameIndex(
                name: "IX_DeclaredPersons_RequestId",
                table: "T_PERSONADECLARADA",
                newName: "IX_T_PERSONADECLARADA_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_DECLARANTE",
                newName: "DT_FECHAMODIFICACION_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "SignatureUserAgent",
                table: "T_DECLARANTE",
                newName: "ST_USERAGENTFIRMA_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "SignatureIpAddress",
                table: "T_DECLARANTE",
                newName: "ST_DIRECCIONIPFIRMA_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "SignatureIdNumber",
                table: "T_DECLARANTE",
                newName: "ST_NUMEROIDENTIFICACIONFIRMA_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "SignatureFullName",
                table: "T_DECLARANTE",
                newName: "ST_NOMBRECOMPLETOFIRMA_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "SignatureDateTime",
                table: "T_DECLARANTE",
                newName: "DT_FECHAFIRMA_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_DECLARANTE",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "RelationshipWithLegalEntity",
                table: "T_DECLARANTE",
                newName: "ST_RELACIONPERSONAJURIDICA_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "PlaceOfOrigin",
                table: "T_DECLARANTE",
                newName: "ST_LUGARORIGEN_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "T_DECLARANTE",
                newName: "ST_TELEFONO_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "NationalityType",
                table: "T_DECLARANTE",
                newName: "IN_TIPONACIONALIDAD_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "LastName2",
                table: "T_DECLARANTE",
                newName: "ST_APELLIDOMATERNO_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "LastName1",
                table: "T_DECLARANTE",
                newName: "ST_APELLIDOPATERNO_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "IdNumber",
                table: "T_DECLARANTE",
                newName: "ST_NUMEROIDENTIFICACION_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "T_DECLARANTE",
                newName: "ST_NOMBRE_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "T_DECLARANTE",
                newName: "ST_EMAIL_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "DeclaresUnderOath",
                table: "T_DECLARANTE",
                newName: "BO_DECLARABAJOJURAMENTO_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "DeclarationDate",
                table: "T_DECLARANTE",
                newName: "DT_FECHADECLARACION_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_DECLARANTE",
                newName: "DT_FECHACREACION_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "T_DECLARANTE",
                newName: "ST_CIUDAD_DECLARANTE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_DECLARANTE",
                newName: "IN_COD_DECLARANTE");

            migrationBuilder.RenameIndex(
                name: "IX_Declarants_RequestId",
                table: "T_DECLARANTE",
                newName: "IX_T_DECLARANTE_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "T_CLIENTE",
                newName: "ST_MODIFICADOPOR_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_CLIENTE",
                newName: "DT_FECHAMODIFICACION_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "RUT",
                table: "T_CLIENTE",
                newName: "ST_RUT_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "T_CLIENTE",
                newName: "BO_ACTIVO_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "T_CLIENTE",
                newName: "ST_CREADOPOR_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_CLIENTE",
                newName: "DT_FECHACREACION_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "BusinessName",
                table: "T_CLIENTE",
                newName: "ST_RAZONSOCIAL_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_CLIENTE",
                newName: "IN_COD_CLIENTE");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_RUT",
                table: "T_CLIENTE",
                newName: "IX_T_CLIENTE_ST_RUT_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "T_BENEFICIARIOFINAL",
                newName: "DT_FECHAMODIFICACION_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "T_BENEFICIARIOFINAL",
                newName: "IN_ORDEN_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_BENEFICIARIOFINAL",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "PepDetail",
                table: "T_BENEFICIARIOFINAL",
                newName: "ST_DETALLEPEP_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "ParticipationPercentage",
                table: "T_BENEFICIARIOFINAL",
                newName: "NU_PORCENTAJEPARTICIPACION_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "IsPEP",
                table: "T_BENEFICIARIOFINAL",
                newName: "BO_ESPEP_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "IsEffectiveControl",
                table: "T_BENEFICIARIOFINAL",
                newName: "BO_ESCONTROLEFECTIVO_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "IsBeneficialOwner",
                table: "T_BENEFICIARIOFINAL",
                newName: "BO_ESBENEFICIARIOFINAL_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "IdNumber",
                table: "T_BENEFICIARIOFINAL",
                newName: "ST_NUMEROIDENTIFICACION_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "T_BENEFICIARIOFINAL",
                newName: "ST_NOMBRECOMPLETO_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_BENEFICIARIOFINAL",
                newName: "DT_FECHACREACION_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "T_BENEFICIARIOFINAL",
                newName: "ST_PAIS_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "T_BENEFICIARIOFINAL",
                newName: "ST_CIUDAD_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "T_BENEFICIARIOFINAL",
                newName: "ST_DIRECCION_BENEFICIARIOFINAL");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_BENEFICIARIOFINAL",
                newName: "IN_COD_BENEFICIARIOFINAL");

            migrationBuilder.RenameIndex(
                name: "IX_BeneficialOwners_RequestId",
                table: "T_BENEFICIARIOFINAL",
                newName: "IX_T_BENEFICIARIOFINAL_IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "T_AUDITORIA",
                newName: "ST_NOMBREUSUARIO_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "T_AUDITORIA",
                newName: "IN_COD_USUARIO");

            migrationBuilder.RenameColumn(
                name: "UserAgent",
                table: "T_AUDITORIA",
                newName: "ST_USERAGENT_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_AUDITORIA",
                newName: "IN_COD_SOLICITUD");

            migrationBuilder.RenameColumn(
                name: "OldValues",
                table: "T_AUDITORIA",
                newName: "ST_VALORESANTERIORES_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "NewValues",
                table: "T_AUDITORIA",
                newName: "ST_VALORESNUEVOS_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "T_AUDITORIA",
                newName: "ST_DIRECCIONIP_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "T_AUDITORIA",
                newName: "ST_TIPOENTIDAD_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "T_AUDITORIA",
                newName: "ST_CODENTIDAD_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "T_AUDITORIA",
                newName: "DT_FECHACREACION_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "T_AUDITORIA",
                newName: "ST_ACCION_AUDITORIA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "T_AUDITORIA",
                newName: "IN_COD_AUDITORIA");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_RequestId",
                table: "T_AUDITORIA",
                newName: "IX_T_AUDITORIA_IN_COD_SOLICITUD");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_CreatedAt",
                table: "T_AUDITORIA",
                newName: "IX_T_AUDITORIA_DT_FECHACREACION_AUDITORIA");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "T_USUARIO",
                column: "ST_NOMBREUSUARIONORMALIZADO_USUARIO",
                unique: true,
                filter: "[ST_NOMBREUSUARIONORMALIZADO_USUARIO] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "T_ROL",
                column: "ST_NOMBRENORMALIZADO_ROL",
                unique: true,
                filter: "[ST_NOMBRENORMALIZADO_ROL] IS NOT NULL");

            // PK y FK: renombrar con sp_rename (sin DROP/CREATE)
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_UsuariosTokens]', N'PK_T_USUARIOTOKEN', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_UsuariosRoles]', N'PK_T_USUARIOROL', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_UsuariosLogins]', N'PK_T_USUARIOLOGIN', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_UsuariosClaims]', N'PK_T_USUARIOCLAIM', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_Usuarios]', N'PK_T_USUARIO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_TaxFolderAnalyses]', N'PK_T_ANALISISCARPETATRIBUTARIA', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_TaxFolderAlerts]', N'PK_T_ALERTACARPETATRIBUTARIA', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_SignatureRecords]', N'PK_T_FIRMA', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_RolesClaims]', N'PK_T_ROLCLAIM', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_Roles]', N'PK_T_ROL', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_RequestStatusHistories]', N'PK_T_HISTORIALESTADOSOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_Requests]', N'PK_T_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_PepDeclarations]', N'PK_T_DECLARACIONPEP', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_Notifications]', N'PK_T_NOTIFICACION', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_LegalEntityDeclarations]', N'PK_T_DECLARACIONPERSONAJURIDICA', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_FileStorageRecords]', N'PK_T_ALMACENAMIENTOARCHIVO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_EffectiveControllers]', N'PK_T_CONTROLADOREFECTIVO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_Documents]', N'PK_T_DOCUMENTO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_DeclaredPersons]', N'PK_T_PERSONADECLARADA', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_Declarants]', N'PK_T_DECLARANTE', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_Clients]', N'PK_T_CLIENTE', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_BeneficialOwners]', N'PK_T_BENEFICIARIOFINAL', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_AuditLogs]', N'PK_T_AUDITORIA', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_AuditLogs_Requests_RequestId]', N'FK_T_AUDITORIA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_BeneficialOwners_Requests_RequestId]', N'FK_T_BENEFICIARIOFINAL_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_Declarants_Requests_RequestId]', N'FK_T_DECLARANTE_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_DeclaredPersons_Requests_RequestId]', N'FK_T_PERSONADECLARADA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_Documents_Requests_RequestId]', N'FK_T_DOCUMENTO_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_EffectiveControllers_Requests_RequestId]', N'FK_T_CONTROLADOREFECTIVO_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_FileStorageRecords_Documents_DocumentId]', N'FK_T_ALMACENAMIENTOARCHIVO_T_DOCUMENTO_IN_COD_DOCUMENTO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_LegalEntityDeclarations_Requests_RequestId]', N'FK_T_DECLARACIONPERSONAJURIDICA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_PepDeclarations_Requests_RequestId]', N'FK_T_DECLARACIONPEP_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_Requests_Clients_ClientId]', N'FK_T_SOLICITUD_T_CLIENTE_IN_COD_CLIENTE', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_Requests_Usuarios_VendorUserId]', N'FK_T_SOLICITUD_T_USUARIO_IN_COD_USUARIO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_RequestStatusHistories_Requests_RequestId]', N'FK_T_HISTORIALESTADOSOLICITUD_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_RolesClaims_Roles_RoleId]', N'FK_T_ROLCLAIM_T_ROL_IN_COD_ROL', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_SignatureRecords_Requests_RequestId]', N'FK_T_FIRMA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_TaxFolderAlerts_TaxFolderAnalyses_TaxFolderAnalysisId]', N'FK_T_ALERTACARPETATRIBUTARIA_T_ANALISISCARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_TaxFolderAnalyses_Requests_RequestId]', N'FK_T_ANALISISCARPETATRIBUTARIA_T_SOLICITUD_IN_COD_SOLICITUD', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_UsuariosClaims_Usuarios_UserId]', N'FK_T_USUARIOCLAIM_T_USUARIO_IN_COD_USUARIO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_UsuariosLogins_Usuarios_UserId]', N'FK_T_USUARIOLOGIN_T_USUARIO_IN_COD_USUARIO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_UsuariosRoles_Usuarios_UserId]', N'FK_T_USUARIOROL_T_USUARIO_IN_COD_USUARIO', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_UsuariosRoles_Roles_RoleId]', N'FK_T_USUARIOROL_T_ROL_IN_COD_ROL', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_UsuariosTokens_Usuarios_UserId]', N'FK_T_USUARIOTOKEN_T_USUARIO_IN_COD_USUARIO', N'OBJECT';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "T_USUARIO");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "T_ROL");

            migrationBuilder.RenameTable(
                name: "T_USUARIOTOKEN",
                newName: "UsuariosTokens");

            migrationBuilder.RenameTable(
                name: "T_USUARIOROL",
                newName: "UsuariosRoles");

            migrationBuilder.RenameTable(
                name: "T_USUARIOLOGIN",
                newName: "UsuariosLogins");

            migrationBuilder.RenameTable(
                name: "T_USUARIOCLAIM",
                newName: "UsuariosClaims");

            migrationBuilder.RenameTable(
                name: "T_USUARIO",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "T_SOLICITUD",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "T_ROLCLAIM",
                newName: "RolesClaims");

            migrationBuilder.RenameTable(
                name: "T_ROL",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "T_PERSONADECLARADA",
                newName: "DeclaredPersons");

            migrationBuilder.RenameTable(
                name: "T_NOTIFICACION",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "T_HISTORIALESTADOSOLICITUD",
                newName: "RequestStatusHistories");

            migrationBuilder.RenameTable(
                name: "T_FIRMA",
                newName: "SignatureRecords");

            migrationBuilder.RenameTable(
                name: "T_DOCUMENTO",
                newName: "Documents");

            migrationBuilder.RenameTable(
                name: "T_DECLARANTE",
                newName: "Declarants");

            migrationBuilder.RenameTable(
                name: "T_DECLARACIONPERSONAJURIDICA",
                newName: "LegalEntityDeclarations");

            migrationBuilder.RenameTable(
                name: "T_DECLARACIONPEP",
                newName: "PepDeclarations");

            migrationBuilder.RenameTable(
                name: "T_CONTROLADOREFECTIVO",
                newName: "EffectiveControllers");

            migrationBuilder.RenameTable(
                name: "T_CLIENTE",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "T_BENEFICIARIOFINAL",
                newName: "BeneficialOwners");

            migrationBuilder.RenameTable(
                name: "T_AUDITORIA",
                newName: "AuditLogs");

            migrationBuilder.RenameTable(
                name: "T_ANALISISCARPETATRIBUTARIA",
                newName: "TaxFolderAnalyses");

            migrationBuilder.RenameTable(
                name: "T_ALMACENAMIENTOARCHIVO",
                newName: "FileStorageRecords");

            migrationBuilder.RenameTable(
                name: "T_ALERTACARPETATRIBUTARIA",
                newName: "TaxFolderAlerts");

            migrationBuilder.RenameColumn(
                name: "ST_VALOR_USUARIOTOKEN",
                table: "UsuariosTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRE_USUARIOTOKEN",
                table: "UsuariosTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ST_PROVEEDORLOGIN_USUARIOTOKEN",
                table: "UsuariosTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "UsuariosTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ROL",
                table: "UsuariosRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "UsuariosRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_T_USUARIOROL_IN_COD_ROL",
                table: "UsuariosRoles",
                newName: "IX_UsuariosRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREVISIBLEPROVEEDOR_USUARIOLOGIN",
                table: "UsuariosLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "UsuariosLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ST_CLAVEPROVEEDOR_USUARIOLOGIN",
                table: "UsuariosLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "ST_PROVEEDORLOGIN_USUARIOLOGIN",
                table: "UsuariosLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "IX_T_USUARIOLOGIN_IN_COD_USUARIO",
                table: "UsuariosLogins",
                newName: "IX_UsuariosLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "ST_VALORCLAIM_USUARIOCLAIM",
                table: "UsuariosClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "ST_TIPOCLAIM_USUARIOCLAIM",
                table: "UsuariosClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "UsuariosClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIOCLAIM",
                table: "UsuariosClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_USUARIOCLAIM_IN_COD_USUARIO",
                table: "UsuariosClaims",
                newName: "IX_UsuariosClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "ST_TELEFONO_USUARIO",
                table: "Usuarios",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ST_SELLOSEGURIDAD_USUARIO",
                table: "Usuarios",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "ST_SELLOCONCURRENCIA_USUARIO",
                table: "Usuarios",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "ST_RUT_USUARIO",
                table: "Usuarios",
                newName: "Rut");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRE_USUARIO",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREUSUARIO_USUARIO",
                table: "Usuarios",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREUSUARIONORMALIZADO_USUARIO",
                table: "Usuarios",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRECOMPLETO_USUARIO",
                table: "Usuarios",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "ST_HASHCONTRASENA_USUARIO",
                table: "Usuarios",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "ST_EMAIL_USUARIO",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ST_EMAILNORMALIZADO_USUARIO",
                table: "Usuarios",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "ST_CODVENDEDOR_USUARIO",
                table: "Usuarios",
                newName: "VendorCode");

            migrationBuilder.RenameColumn(
                name: "ST_APELLIDOPATERNO_USUARIO",
                table: "Usuarios",
                newName: "ApellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "ST_APELLIDOMATERNO_USUARIO",
                table: "Usuarios",
                newName: "ApellidoMaterno");

            migrationBuilder.RenameColumn(
                name: "IN_CANTIDADACCESOSFALLIDOS_USUARIO",
                table: "Usuarios",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAULTIMOLOGIN_USUARIO",
                table: "Usuarios",
                newName: "LastLoginAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAFINBLOQUEO_USUARIO",
                table: "Usuarios",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_USUARIO",
                table: "Usuarios",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_TELEFONOCONFIRMADO_USUARIO",
                table: "Usuarios",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "BO_EMAILCONFIRMADO_USUARIO",
                table: "Usuarios",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "BO_DOBLEFACTORHABILITADO_USUARIO",
                table: "Usuarios",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "BO_BLOQUEOHABILITADO_USUARIO",
                table: "Usuarios",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "BO_ACTIVO_USUARIO",
                table: "Usuarios",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ST_TOKENCLIENTE_SOLICITUD",
                table: "Requests",
                newName: "ClientToken");

            migrationBuilder.RenameColumn(
                name: "ST_TELEFONOCLIENTE_SOLICITUD",
                table: "Requests",
                newName: "ClientPhone");

            migrationBuilder.RenameColumn(
                name: "ST_NUMERO_SOLICITUD",
                table: "Requests",
                newName: "RequestNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NUMERONEGOCIONETCAR_SOLICITUD",
                table: "Requests",
                newName: "NetcarBusinessNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOTASINTERNAS_SOLICITUD",
                table: "Requests",
                newName: "InternalNotes");

            migrationBuilder.RenameColumn(
                name: "ST_MODIFICADOPOR_SOLICITUD",
                table: "Requests",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "ST_EMAILCLIENTE_SOLICITUD",
                table: "Requests",
                newName: "ClientEmail");

            migrationBuilder.RenameColumn(
                name: "ST_CREADOPOR_SOLICITUD",
                table: "Requests",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "IN_TIPO_SOLICITUD",
                table: "Requests",
                newName: "RequestType");

            migrationBuilder.RenameColumn(
                name: "IN_PASOACTUAL_SOLICITUD",
                table: "Requests",
                newName: "CurrentStep");

            migrationBuilder.RenameColumn(
                name: "IN_ESTADO_SOLICITUD",
                table: "Requests",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "Requests",
                newName: "VendorUserId");

            migrationBuilder.RenameColumn(
                name: "IN_COD_CLIENTE",
                table: "Requests",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAVENCIMIENTO_SOLICITUD",
                table: "Requests",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAVENCIMIENTOTOKEN_SOLICITUD",
                table: "Requests",
                newName: "TokenExpiry");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_SOLICITUD",
                table: "Requests",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAENVIO_SOLICITUD",
                table: "Requests",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_SOLICITUD",
                table: "Requests",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACOMPLETADA_SOLICITUD",
                table: "Requests",
                newName: "CompletedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAAPERTURA_SOLICITUD",
                table: "Requests",
                newName: "OpenedAt");

            migrationBuilder.RenameColumn(
                name: "BO_ELIMINADO_SOLICITUD",
                table: "Requests",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "Requests",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_SOLICITUD_ST_TOKENCLIENTE_SOLICITUD",
                table: "Requests",
                newName: "IX_Requests_ClientToken");

            migrationBuilder.RenameIndex(
                name: "IX_T_SOLICITUD_ST_NUMERO_SOLICITUD",
                table: "Requests",
                newName: "IX_Requests_RequestNumber");

            migrationBuilder.RenameIndex(
                name: "IX_T_SOLICITUD_IN_ESTADO_SOLICITUD",
                table: "Requests",
                newName: "IX_Requests_Status");

            migrationBuilder.RenameIndex(
                name: "IX_T_SOLICITUD_IN_COD_USUARIO",
                table: "Requests",
                newName: "IX_Requests_VendorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_T_SOLICITUD_IN_COD_CLIENTE",
                table: "Requests",
                newName: "IX_Requests_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_T_SOLICITUD_DT_FECHACREACION_SOLICITUD",
                table: "Requests",
                newName: "IX_Requests_CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ST_VALORCLAIM_ROLCLAIM",
                table: "RolesClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "ST_TIPOCLAIM_ROLCLAIM",
                table: "RolesClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ROL",
                table: "RolesClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ROLCLAIM",
                table: "RolesClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_ROLCLAIM_IN_COD_ROL",
                table: "RolesClaims",
                newName: "IX_RolesClaims_RoleId");

            migrationBuilder.RenameColumn(
                name: "ST_SELLOCONCURRENCIA_ROL",
                table: "Roles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRE_ROL",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRENORMALIZADO_ROL",
                table: "Roles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ROL",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ST_TIPORELACIONOTRO_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "RelationshipTypeOther");

            migrationBuilder.RenameColumn(
                name: "ST_RELACIONPEP_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "PepRelationship");

            migrationBuilder.RenameColumn(
                name: "ST_PAIS_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "ST_OBSERVACIONPEP_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "PepObservation");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACION_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "IdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRETIPOPEP_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "PepTypeName");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRECOMPLETO_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "ST_INSTITUCIONPEP_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "PepInstitution");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCION_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "ST_DESCRIPCIONCONTROLEFECTIVO_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "EffectiveControlDescription");

            migrationBuilder.RenameColumn(
                name: "ST_CIUDAD_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "ST_CARGOPEP_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "PepPosition");

            migrationBuilder.RenameColumn(
                name: "NU_PORCENTAJEPARTICIPACION_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "ParticipationPercentage");

            migrationBuilder.RenameColumn(
                name: "IN_TIPORELACION_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "RelationshipType");

            migrationBuilder.RenameColumn(
                name: "IN_TIPOPEP_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "PepType");

            migrationBuilder.RenameColumn(
                name: "IN_ORDEN_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "DeclaredPersons",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_PARTICIPACIONMINDIEZPORCIENTO_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "HasMinTenPercentParticipation");

            migrationBuilder.RenameColumn(
                name: "BO_MANEJAEFECTIVOFONDOS_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "HandlesCashOrFunds");

            migrationBuilder.RenameColumn(
                name: "BO_ESPEP_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "IsPEP");

            migrationBuilder.RenameColumn(
                name: "BO_ESCONTROLADOREFECTIVO_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "IsEffectiveController");

            migrationBuilder.RenameColumn(
                name: "IN_COD_PERSONADECLARADA",
                table: "DeclaredPersons",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_PERSONADECLARADA_IN_COD_SOLICITUD",
                table: "DeclaredPersons",
                newName: "IX_DeclaredPersons_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_MENSAJE_NOTIFICACION",
                table: "Notifications",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "IN_TIPO_NOTIFICACION",
                table: "Notifications",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "Notifications",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHALECTURA_NOTIFICACION",
                table: "Notifications",
                newName: "ReadAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_NOTIFICACION",
                table: "Notifications",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_LEIDA_NOTIFICACION",
                table: "Notifications",
                newName: "IsRead");

            migrationBuilder.RenameColumn(
                name: "IN_COD_NOTIFICACION",
                table: "Notifications",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_NOTIFICACION_IN_COD_USUARIO_BO_LEIDA_NOTIFICACION",
                table: "Notifications",
                newName: "IX_Notifications_UserId_IsRead");

            migrationBuilder.RenameIndex(
                name: "IX_T_NOTIFICACION_IN_COD_USUARIO",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_T_NOTIFICACION_DT_FECHACREACION_NOTIFICACION",
                table: "Notifications",
                newName: "IX_Notifications_CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ST_NOTAS_HISTORIALESTADOSOLICITUD",
                table: "RequestStatusHistories",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "ST_CAMBIADOPOR_HISTORIALESTADOSOLICITUD",
                table: "RequestStatusHistories",
                newName: "ChangedBy");

            migrationBuilder.RenameColumn(
                name: "IN_ESTADONUEVO_HISTORIALESTADOSOLICITUD",
                table: "RequestStatusHistories",
                newName: "NewStatus");

            migrationBuilder.RenameColumn(
                name: "IN_ESTADOANTERIOR_HISTORIALESTADOSOLICITUD",
                table: "RequestStatusHistories",
                newName: "OldStatus");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "RequestStatusHistories",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACAMBIO_HISTORIALESTADOSOLICITUD",
                table: "RequestStatusHistories",
                newName: "ChangedAt");

            migrationBuilder.RenameColumn(
                name: "IN_COD_HISTORIALESTADOSOLICITUD",
                table: "RequestStatusHistories",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_HISTORIALESTADOSOLICITUD_IN_COD_SOLICITUD",
                table: "RequestStatusHistories",
                newName: "IX_RequestStatusHistories_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_USERAGENT_FIRMA",
                table: "SignatureRecords",
                newName: "UserAgent");

            migrationBuilder.RenameColumn(
                name: "ST_URL_FIRMA",
                table: "SignatureRecords",
                newName: "SignatureUrl");

            migrationBuilder.RenameColumn(
                name: "ST_RUTAPDFFIRMADO_FIRMA",
                table: "SignatureRecords",
                newName: "SignedPdfPath");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACIONFIRMANTE_FIRMA",
                table: "SignatureRecords",
                newName: "SignerIdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROFOLIO_FIRMA",
                table: "SignatureRecords",
                newName: "FolioNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREFIRMANTE_FIRMA",
                table: "SignatureRecords",
                newName: "SignerName");

            migrationBuilder.RenameColumn(
                name: "ST_HASHDOCUMENTO_FIRMA",
                table: "SignatureRecords",
                newName: "DocumentHash");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCIONIP_FIRMA",
                table: "SignatureRecords",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "ST_CODTRANSACCIONPROVEEDOR_FIRMA",
                table: "SignatureRecords",
                newName: "ProviderTransactionId");

            migrationBuilder.RenameColumn(
                name: "ST_CERTIFICADO_FIRMA",
                table: "SignatureRecords",
                newName: "Certificate");

            migrationBuilder.RenameColumn(
                name: "IN_TIPO_FIRMA",
                table: "SignatureRecords",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "IN_ESTADO_FIRMA",
                table: "SignatureRecords",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "SignatureRecords",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_FIRMA",
                table: "SignatureRecords",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAFIRMA_FIRMA",
                table: "SignatureRecords",
                newName: "SignedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_FIRMA",
                table: "SignatureRecords",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "IN_COD_FIRMA",
                table: "SignatureRecords",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_FIRMA_IN_COD_SOLICITUD",
                table: "SignatureRecords",
                newName: "IX_SignatureRecords_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_TIPOOTRO_DOCUMENTO",
                table: "Documents",
                newName: "DocumentTypeOther");

            migrationBuilder.RenameColumn(
                name: "ST_TIPOMIME_DOCUMENTO",
                table: "Documents",
                newName: "MimeType");

            migrationBuilder.RenameColumn(
                name: "ST_RUTAALMACENAMIENTO_DOCUMENTO",
                table: "Documents",
                newName: "StoragePath");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREARCHIVOORIGINAL_DOCUMENTO",
                table: "Documents",
                newName: "OriginalFileName");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREARCHIVOALMACENADO_DOCUMENTO",
                table: "Documents",
                newName: "StoredFileName");

            migrationBuilder.RenameColumn(
                name: "ST_IPCARGA_DOCUMENTO",
                table: "Documents",
                newName: "UploadedByIp");

            migrationBuilder.RenameColumn(
                name: "IN_VERSION_DOCUMENTO",
                table: "Documents",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "IN_TIPO_DOCUMENTO",
                table: "Documents",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "IN_TAMANOBYTES_DOCUMENTO",
                table: "Documents",
                newName: "FileSizeBytes");

            migrationBuilder.RenameColumn(
                name: "IN_PROVEEDORALMACENAMIENTO_DOCUMENTO",
                table: "Documents",
                newName: "StorageProvider");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "Documents",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACARGA_DOCUMENTO",
                table: "Documents",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "BO_ACTIVO_DOCUMENTO",
                table: "Documents",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IN_COD_DOCUMENTO",
                table: "Documents",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_DOCUMENTO_IN_COD_SOLICITUD",
                table: "Documents",
                newName: "IX_Documents_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_USERAGENTFIRMA_DECLARANTE",
                table: "Declarants",
                newName: "SignatureUserAgent");

            migrationBuilder.RenameColumn(
                name: "ST_TELEFONO_DECLARANTE",
                table: "Declarants",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "ST_RELACIONPERSONAJURIDICA_DECLARANTE",
                table: "Declarants",
                newName: "RelationshipWithLegalEntity");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACION_DECLARANTE",
                table: "Declarants",
                newName: "IdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACIONFIRMA_DECLARANTE",
                table: "Declarants",
                newName: "SignatureIdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRE_DECLARANTE",
                table: "Declarants",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRECOMPLETOFIRMA_DECLARANTE",
                table: "Declarants",
                newName: "SignatureFullName");

            migrationBuilder.RenameColumn(
                name: "ST_LUGARORIGEN_DECLARANTE",
                table: "Declarants",
                newName: "PlaceOfOrigin");

            migrationBuilder.RenameColumn(
                name: "ST_EMAIL_DECLARANTE",
                table: "Declarants",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCIONIPFIRMA_DECLARANTE",
                table: "Declarants",
                newName: "SignatureIpAddress");

            migrationBuilder.RenameColumn(
                name: "ST_CIUDAD_DECLARANTE",
                table: "Declarants",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "ST_APELLIDOPATERNO_DECLARANTE",
                table: "Declarants",
                newName: "LastName1");

            migrationBuilder.RenameColumn(
                name: "ST_APELLIDOMATERNO_DECLARANTE",
                table: "Declarants",
                newName: "LastName2");

            migrationBuilder.RenameColumn(
                name: "IN_TIPONACIONALIDAD_DECLARANTE",
                table: "Declarants",
                newName: "NationalityType");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "Declarants",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_DECLARANTE",
                table: "Declarants",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAFIRMA_DECLARANTE",
                table: "Declarants",
                newName: "SignatureDateTime");

            migrationBuilder.RenameColumn(
                name: "DT_FECHADECLARACION_DECLARANTE",
                table: "Declarants",
                newName: "DeclarationDate");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_DECLARANTE",
                table: "Declarants",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_DECLARABAJOJURAMENTO_DECLARANTE",
                table: "Declarants",
                newName: "DeclaresUnderOath");

            migrationBuilder.RenameColumn(
                name: "IN_COD_DECLARANTE",
                table: "Declarants",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_DECLARANTE_IN_COD_SOLICITUD",
                table: "Declarants",
                newName: "IX_Declarants_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_TIPOENTIDADOTRO_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "EntityTypeOther");

            migrationBuilder.RenameColumn(
                name: "ST_TELEFONO_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "ST_RUT_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "RUT");

            migrationBuilder.RenameColumn(
                name: "ST_RAZONSOCIAL_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "BusinessName");

            migrationBuilder.RenameColumn(
                name: "ST_PAISCONSTITUCION_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "CountryOfIncorporation");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACIONREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "LegalRepresentativeIdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "LegalRepresentativeName");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCION_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "ST_CIUDAD_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "IN_TIPOENTIDAD_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "EntityType");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "LegalEntityDeclarations",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "IN_COD_DECLARACIONPERSONAJURIDICA",
                table: "LegalEntityDeclarations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_DECLARACIONPERSONAJURIDICA_IN_COD_SOLICITUD",
                table: "LegalEntityDeclarations",
                newName: "IX_LegalEntityDeclarations_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_USERAGENTFIRMA_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "SignatureUserAgent");

            migrationBuilder.RenameColumn(
                name: "ST_TIPOVINCULO_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "VinculoType");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACION_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "IdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACIONFIRMA_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "SignatureIdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREPEP_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "PepName");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREDECLARANTE_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "DeclarantName");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRECOMPLETOFIRMA_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "SignatureFullName");

            migrationBuilder.RenameColumn(
                name: "ST_NACIONALIDAD_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "Nationality");

            migrationBuilder.RenameColumn(
                name: "ST_MOTIVOPEPOTRO_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "PepReasonOther");

            migrationBuilder.RenameColumn(
                name: "ST_INSTITUCION_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "Institution");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCIONIPFIRMA_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "SignatureIpAddress");

            migrationBuilder.RenameColumn(
                name: "IN_MOTIVOPEP_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "PepReasonType");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "PepDeclarations",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAFIRMA_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "SignatureDateTime");

            migrationBuilder.RenameColumn(
                name: "DT_FECHADECLARACION_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "DeclarationDate");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_DECLARAPEP_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "DeclaresPEP");

            migrationBuilder.RenameColumn(
                name: "BO_ACEPTABAJOJURAMENTO_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "AcceptsUnderOath");

            migrationBuilder.RenameColumn(
                name: "IN_COD_DECLARACIONPEP",
                table: "PepDeclarations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_DECLARACIONPEP_IN_COD_SOLICITUD",
                table: "PepDeclarations",
                newName: "IX_PepDeclarations_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_PAIS_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACION_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "IdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRECOMPLETO_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCION_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "ST_DETALLEPEP_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "PepDetail");

            migrationBuilder.RenameColumn(
                name: "ST_DESCRIPCIONCONTROL_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "ControlDescription");

            migrationBuilder.RenameColumn(
                name: "ST_CIUDAD_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "NU_PORCENTAJEPARTICIPACION_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "ParticipationPercentage");

            migrationBuilder.RenameColumn(
                name: "IN_ORDEN_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "EffectiveControllers",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_ESPEP_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "IsPEP");

            migrationBuilder.RenameColumn(
                name: "IN_COD_CONTROLADOREFECTIVO",
                table: "EffectiveControllers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_CONTROLADOREFECTIVO_IN_COD_SOLICITUD",
                table: "EffectiveControllers",
                newName: "IX_EffectiveControllers_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_RUT_CLIENTE",
                table: "Clients",
                newName: "RUT");

            migrationBuilder.RenameColumn(
                name: "ST_RAZONSOCIAL_CLIENTE",
                table: "Clients",
                newName: "BusinessName");

            migrationBuilder.RenameColumn(
                name: "ST_MODIFICADOPOR_CLIENTE",
                table: "Clients",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "ST_CREADOPOR_CLIENTE",
                table: "Clients",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_CLIENTE",
                table: "Clients",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_CLIENTE",
                table: "Clients",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_ACTIVO_CLIENTE",
                table: "Clients",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IN_COD_CLIENTE",
                table: "Clients",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLIENTE_ST_RUT_CLIENTE",
                table: "Clients",
                newName: "IX_Clients_RUT");

            migrationBuilder.RenameColumn(
                name: "ST_PAIS_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "ST_NUMEROIDENTIFICACION_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "IdNumber");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBRECOMPLETO_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCION_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "ST_DETALLEPEP_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "PepDetail");

            migrationBuilder.RenameColumn(
                name: "ST_CIUDAD_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "NU_PORCENTAJEPARTICIPACION_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "ParticipationPercentage");

            migrationBuilder.RenameColumn(
                name: "IN_ORDEN_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "BeneficialOwners",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAMODIFICACION_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "BO_ESPEP_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "IsPEP");

            migrationBuilder.RenameColumn(
                name: "BO_ESCONTROLEFECTIVO_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "IsEffectiveControl");

            migrationBuilder.RenameColumn(
                name: "BO_ESBENEFICIARIOFINAL_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "IsBeneficialOwner");

            migrationBuilder.RenameColumn(
                name: "IN_COD_BENEFICIARIOFINAL",
                table: "BeneficialOwners",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_BENEFICIARIOFINAL_IN_COD_SOLICITUD",
                table: "BeneficialOwners",
                newName: "IX_BeneficialOwners_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_VALORESNUEVOS_AUDITORIA",
                table: "AuditLogs",
                newName: "NewValues");

            migrationBuilder.RenameColumn(
                name: "ST_VALORESANTERIORES_AUDITORIA",
                table: "AuditLogs",
                newName: "OldValues");

            migrationBuilder.RenameColumn(
                name: "ST_USERAGENT_AUDITORIA",
                table: "AuditLogs",
                newName: "UserAgent");

            migrationBuilder.RenameColumn(
                name: "ST_TIPOENTIDAD_AUDITORIA",
                table: "AuditLogs",
                newName: "EntityType");

            migrationBuilder.RenameColumn(
                name: "ST_NOMBREUSUARIO_AUDITORIA",
                table: "AuditLogs",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCIONIP_AUDITORIA",
                table: "AuditLogs",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "ST_CODENTIDAD_AUDITORIA",
                table: "AuditLogs",
                newName: "EntityId");

            migrationBuilder.RenameColumn(
                name: "ST_ACCION_AUDITORIA",
                table: "AuditLogs",
                newName: "Action");

            migrationBuilder.RenameColumn(
                name: "IN_COD_USUARIO",
                table: "AuditLogs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "AuditLogs",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACREACION_AUDITORIA",
                table: "AuditLogs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "IN_COD_AUDITORIA",
                table: "AuditLogs",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_AUDITORIA_IN_COD_SOLICITUD",
                table: "AuditLogs",
                newName: "IX_AuditLogs_RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_T_AUDITORIA_DT_FECHACREACION_AUDITORIA",
                table: "AuditLogs",
                newName: "IX_AuditLogs_CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ST_TEXTOBRUTO_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "RawText");

            migrationBuilder.RenameColumn(
                name: "ST_RUTEXTRAIDO_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "ExtractedRut");

            migrationBuilder.RenameColumn(
                name: "ST_REPRESENTANTELEGALEXTRAIDO_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "ExtractedLegalRep");

            migrationBuilder.RenameColumn(
                name: "ST_RAZONSOCIALEXTRAIDA_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "ExtractedBusinessName");

            migrationBuilder.RenameColumn(
                name: "ST_FECHAEMISIONEXTRAIDA_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "ExtractedIssuedDate");

            migrationBuilder.RenameColumn(
                name: "ST_DIRECCIONEXTRAIDA_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "ExtractedAddress");

            migrationBuilder.RenameColumn(
                name: "ST_ACTIVIDADEXTRAIDA_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "ExtractedActivity");

            migrationBuilder.RenameColumn(
                name: "IN_COD_SOLICITUD",
                table: "TaxFolderAnalyses",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHAANALISIS_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "AnalyzedAt");

            migrationBuilder.RenameColumn(
                name: "BO_LEGIBLE_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "WasReadable");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAnalyses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_ANALISISCARPETATRIBUTARIA_IN_COD_SOLICITUD",
                table: "TaxFolderAnalyses",
                newName: "IX_TaxFolderAnalyses_RequestId");

            migrationBuilder.RenameColumn(
                name: "ST_RUTAALMACENAMIENTO_ALMACENAMIENTOARCHIVO",
                table: "FileStorageRecords",
                newName: "StoragePath");

            migrationBuilder.RenameColumn(
                name: "ST_METADATOS_ALMACENAMIENTOARCHIVO",
                table: "FileStorageRecords",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "ST_CLAVEALMACENAMIENTO_ALMACENAMIENTOARCHIVO",
                table: "FileStorageRecords",
                newName: "StorageKey");

            migrationBuilder.RenameColumn(
                name: "IN_PROVEEDORALMACENAMIENTO_ALMACENAMIENTOARCHIVO",
                table: "FileStorageRecords",
                newName: "StorageProvider");

            migrationBuilder.RenameColumn(
                name: "IN_COD_DOCUMENTO",
                table: "FileStorageRecords",
                newName: "DocumentId");

            migrationBuilder.RenameColumn(
                name: "DT_FECHACARGA_ALMACENAMIENTOARCHIVO",
                table: "FileStorageRecords",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ALMACENAMIENTOARCHIVO",
                table: "FileStorageRecords",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_ALMACENAMIENTOARCHIVO_IN_COD_DOCUMENTO",
                table: "FileStorageRecords",
                newName: "IX_FileStorageRecords_DocumentId");

            migrationBuilder.RenameColumn(
                name: "ST_SEVERIDAD_ALERTACARPETATRIBUTARIA",
                table: "TaxFolderAlerts",
                newName: "Severity");

            migrationBuilder.RenameColumn(
                name: "ST_MENSAJE_ALERTACARPETATRIBUTARIA",
                table: "TaxFolderAlerts",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "IN_TIPO_ALERTACARPETATRIBUTARIA",
                table: "TaxFolderAlerts",
                newName: "AlertType");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAlerts",
                newName: "TaxFolderAnalysisId");

            migrationBuilder.RenameColumn(
                name: "BO_RESUELTA_ALERTACARPETATRIBUTARIA",
                table: "TaxFolderAlerts",
                newName: "IsResolved");

            migrationBuilder.RenameColumn(
                name: "IN_COD_ALERTACARPETATRIBUTARIA",
                table: "TaxFolderAlerts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_T_ALERTACARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA",
                table: "TaxFolderAlerts",
                newName: "IX_TaxFolderAlerts_TaxFolderAnalysisId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuarios",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            // PK y FK: devolver nombres originales
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_USUARIOTOKEN]', N'PK_UsuariosTokens', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_USUARIOROL]', N'PK_UsuariosRoles', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_USUARIOLOGIN]', N'PK_UsuariosLogins', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_USUARIOCLAIM]', N'PK_UsuariosClaims', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_USUARIO]', N'PK_Usuarios', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_ANALISISCARPETATRIBUTARIA]', N'PK_TaxFolderAnalyses', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_ALERTACARPETATRIBUTARIA]', N'PK_TaxFolderAlerts', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_FIRMA]', N'PK_SignatureRecords', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_ROLCLAIM]', N'PK_RolesClaims', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_ROL]', N'PK_Roles', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_HISTORIALESTADOSOLICITUD]', N'PK_RequestStatusHistories', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_SOLICITUD]', N'PK_Requests', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_DECLARACIONPEP]', N'PK_PepDeclarations', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_NOTIFICACION]', N'PK_Notifications', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_DECLARACIONPERSONAJURIDICA]', N'PK_LegalEntityDeclarations', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_ALMACENAMIENTOARCHIVO]', N'PK_FileStorageRecords', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_CONTROLADOREFECTIVO]', N'PK_EffectiveControllers', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_DOCUMENTO]', N'PK_Documents', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_PERSONADECLARADA]', N'PK_DeclaredPersons', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_DECLARANTE]', N'PK_Declarants', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_CLIENTE]', N'PK_Clients', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_BENEFICIARIOFINAL]', N'PK_BeneficialOwners', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[PK_T_AUDITORIA]', N'PK_AuditLogs', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_AUDITORIA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_AuditLogs_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_BENEFICIARIOFINAL_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_BeneficialOwners_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_DECLARANTE_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_Declarants_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_PERSONADECLARADA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_DeclaredPersons_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_DOCUMENTO_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_Documents_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_CONTROLADOREFECTIVO_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_EffectiveControllers_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_ALMACENAMIENTOARCHIVO_T_DOCUMENTO_IN_COD_DOCUMENTO]', N'FK_FileStorageRecords_Documents_DocumentId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_DECLARACIONPERSONAJURIDICA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_LegalEntityDeclarations_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_DECLARACIONPEP_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_PepDeclarations_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_SOLICITUD_T_CLIENTE_IN_COD_CLIENTE]', N'FK_Requests_Clients_ClientId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_SOLICITUD_T_USUARIO_IN_COD_USUARIO]', N'FK_Requests_Usuarios_VendorUserId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_HISTORIALESTADOSOLICITUD_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_RequestStatusHistories_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_ROLCLAIM_T_ROL_IN_COD_ROL]', N'FK_RolesClaims_Roles_RoleId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_FIRMA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_SignatureRecords_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_ALERTACARPETATRIBUTARIA_T_ANALISISCARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA]', N'FK_TaxFolderAlerts_TaxFolderAnalyses_TaxFolderAnalysisId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_ANALISISCARPETATRIBUTARIA_T_SOLICITUD_IN_COD_SOLICITUD]', N'FK_TaxFolderAnalyses_Requests_RequestId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_USUARIOCLAIM_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosClaims_Usuarios_UserId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_USUARIOLOGIN_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosLogins_Usuarios_UserId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_USUARIOROL_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosRoles_Usuarios_UserId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_USUARIOROL_T_ROL_IN_COD_ROL]', N'FK_UsuariosRoles_Roles_RoleId', N'OBJECT';");
            migrationBuilder.Sql("EXEC sp_rename N'[dbo].[FK_T_USUARIOTOKEN_T_USUARIO_IN_COD_USUARIO]', N'FK_UsuariosTokens_Usuarios_UserId', N'OBJECT';");
        }
    }
}

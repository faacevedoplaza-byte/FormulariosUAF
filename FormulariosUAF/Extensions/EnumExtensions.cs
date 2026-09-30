using FormulariosUAF.Models.Enums;

namespace FormulariosUAF.Extensions;

public static class EnumExtensions
{
    public static string ToBadgeClass(this RequestStatus status) => status switch
    {
        RequestStatus.Borrador => "bg-secondary",
        RequestStatus.EnviadaAlCliente => "bg-info text-dark",
        RequestStatus.AbiertaPorCliente => "bg-primary",
        RequestStatus.CompletadaPorCliente => "bg-warning text-dark",
        RequestStatus.EnRevision => "bg-warning text-dark",
        RequestStatus.Observada => "bg-danger",
        RequestStatus.CorregidaPorCliente => "bg-info text-dark",
        RequestStatus.CorreccionSolicitada => "bg-warning text-dark",
        RequestStatus.Aprobada => "bg-success",
        RequestStatus.Rechazada => "bg-danger",
        RequestStatus.Vencida => "bg-dark",
        _ => "bg-secondary"
    };

    public static string ToDisplayString(this RequestType type) => type switch
    {
        RequestType.ClienteNuevo => "Cliente Nuevo",
        RequestType.TransaccionUnica => "Transacción Única",
        RequestType.ActualizacionDatos => "Actualización de Datos",
        RequestType.ActualizacionSinCambios => "Actualización sin Cambios",
        _ => type.ToString()
    };

    public static string ToDisplayString(this EntityType type) => type switch
    {
        EntityType.Anonima => "Sociedad Anónima Cerrada (S.A.)",
        EntityType.Colectiva => "Sociedad Colectiva Comercial",
        EntityType.EnComandita => "Sociedad en Comandita",
        EntityType.Limitada => "Sociedad de Responsabilidad Limitada (Ltda.)",
        EntityType.EIRL => "Empresa Individual de Responsabilidad Limitada (EIRL)",
        EntityType.SPA => "Sociedad por Acciones (SpA)",
        EntityType.PersonaNatural => "Persona Natural",
        EntityType.Otra => "Otra",
        _ => type.ToString()
    };

    public static string ToDisplayString(this DocumentType type) => type switch
    {
        DocumentType.CarpetaTributaria => "Carpeta Tributaria",
        DocumentType.CedulaRepresentanteLegal => "Cédula de Identidad Representante Legal",
        DocumentType.EscriturasOPoderes => "Escrituras o Poderes",
        DocumentType.OtroDocumento => "Otro Documento",
        _ => type.ToString()
    };

    public static string ToDisplayString(this NationalityType type) => type switch
    {
        NationalityType.Chilena => "Chilena",
        NationalityType.ExtranjeroResidente => "Extranjero Residente",
        NationalityType.ExtranjeroNoResidente => "Extranjero No Residente",
        _ => type.ToString()
    };

    public static string ToDisplayString(this PepReason reason) => reason switch
    {
        PepReason.Titular => "Titular",
        PepReason.Asociado => "Asociado",
        PepReason.Parentesco => "Parentesco",
        PepReason.Otro => "Otro",
        _ => reason.ToString()
    };

    public static string ToDisplayString(this RelationshipType type) => type switch
    {
        RelationshipType.BeneficiarioFinal => "Beneficiario Final",
        RelationshipType.Socio => "Socio",
        RelationshipType.Accionista => "Accionista",
        RelationshipType.RepresentanteLegal => "Representante Legal",
        RelationshipType.ControladorEfectivo => "Controlador Efectivo",
        RelationshipType.Apoderado => "Apoderado",
        RelationshipType.Otro => "Otro",
        _ => type.ToString()
    };

    public static string ToDisplayString(this PepType type) => type switch
    {
        PepType.Titular => "Titular",
        PepType.Asociado => "Asociado",
        PepType.Parentesco => "Parentesco",
        PepType.ConyugeOConviviente => "Cónyuge / Conviviente Civil",
        PepType.PactoActuacionConjunta => "Pacto de Actuación Conjunta",
        PepType.Otro => "Otro",
        _ => type.ToString()
    };

    public static string ToDisplayString(this RequestStatus status, bool includeNew = true) => status switch
    {
        RequestStatus.Borrador => "Borrador",
        RequestStatus.EnviadaAlCliente => "Enviada al Cliente",
        RequestStatus.AbiertaPorCliente => "Abierta por Cliente",
        RequestStatus.CompletadaPorCliente => "Completada por Cliente",
        RequestStatus.EnRevision => "En Revisión",
        RequestStatus.Observada => "Observada",
        RequestStatus.CorregidaPorCliente => "Corregida por Cliente",
        RequestStatus.CorreccionSolicitada => "Corrección Solicitada",
        RequestStatus.Aprobada => "Aprobada",
        RequestStatus.Rechazada => "Rechazada",
        RequestStatus.Vencida => "Vencida",
        _ => status.ToString()
    };
}

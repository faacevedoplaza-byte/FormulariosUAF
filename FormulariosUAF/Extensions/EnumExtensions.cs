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

    // Ayuda para el vendedor al elegir el tipo en /Vendedor/Crear.
    public static string ToDescription(this RequestType type) => type switch
    {
        RequestType.ClienteNuevo => "La empresa compra por primera vez o inicia una relación comercial con nosotros. Debe declarar a todos sus beneficiarios finales desde cero.",
        RequestType.TransaccionUnica => "Operación puntual, sin relación comercial permanente (por ejemplo, una empresa que compra un solo vehículo). La declaración aplica solo a esta operación.",
        RequestType.ActualizacionDatos => "El cliente ya declaró antes, pero algo cambió: socios, porcentajes de propiedad, representante o condición PEP. Debe declarar nuevamente con la información corregida.",
        RequestType.ActualizacionSinCambios => "El cliente ya declaró antes y no ha cambiado nada. Podrá reiterar su declaración anterior (indicando su N° y fecha) sin volver a ingresar los beneficiarios.",
        _ => string.Empty
    };

    // Caso típico, para que el vendedor elija el tipo correcto.
    public static string ToEjemplo(this RequestType type) => type switch
    {
        RequestType.ClienteNuevo => "Una empresa que nunca nos ha comprado y empezará a hacerlo.",
        RequestType.TransaccionUnica => "Una empresa que compra un solo vehículo y no se espera que vuelva.",
        RequestType.ActualizacionDatos => "Un cliente habitual que cambió de socios o de representante legal.",
        RequestType.ActualizacionSinCambios => "Un cliente habitual que vuelve a comprar y sigue con los mismos dueños.",
        _ => string.Empty
    };

    public static string ToIcon(this RequestType type) => type switch
    {
        RequestType.ClienteNuevo => "fa-user-plus",
        RequestType.TransaccionUnica => "fa-receipt",
        RequestType.ActualizacionDatos => "fa-pen-to-square",
        RequestType.ActualizacionSinCambios => "fa-rotate",
        _ => "fa-file"
    };

    /// <summary>
    /// Si la empresa ya tiene una solicitud previa, solo corresponde actualizar su declaración.
    /// </summary>
    public static bool PermitidoConSolicitudPrevia(this RequestType type) =>
        type is RequestType.ActualizacionDatos or RequestType.ActualizacionSinCambios;

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

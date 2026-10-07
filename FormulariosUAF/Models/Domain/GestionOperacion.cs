namespace FormulariosUAF.Models.Domain;

/// <summary>
/// Gestión de una operación RegCheq (persona natural) con firma PEP y/o DOF pendiente.
/// La operación vive en IntegracionesNetCar (DB_REGCHEQ); aquí solo se guarda quién la tomó y su bitácora.
/// Se cierra sola cuando RegCheq marca como firmados los formularios requeridos (ver Pages/Regcheq/Gestion).
/// </summary>
public class GestionOperacion
{
    public int Id { get; set; }

    /// <summary>IN_COD_OPERACION_NATURAL de DB_REGCHEQ.T_OPERACION_NATURAL.</summary>
    public int OperacionId { get; set; }

    // Copia de datos de la operación al tomarla, para listar las gestiones cerradas sin consultar RegCheq.
    public int? CodigoOperacion { get; set; }
    public string? ClienteRut { get; set; }
    public string? ClienteNombre { get; set; }

    /// <summary>Usuario que tiene la operación tomada. Null = liberada.</summary>
    public string? UsuarioId { get; set; }
    public ApplicationUser? Usuario { get; set; }
    public DateTime? FechaToma { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Cuando RegCheq dejó de tenerla como pendiente (formularios requeridos firmados).</summary>
    public DateTime? FechaCierre { get; set; }

    public ICollection<GestionOperacionNota> Notas { get; set; } = new List<GestionOperacionNota>();
}

/// <summary>Entrada de la bitácora de una gestión (texto libre del usuario o evento del sistema).</summary>
public class GestionOperacionNota
{
    public int Id { get; set; }
    public int GestionOperacionId { get; set; }
    public GestionOperacion? GestionOperacion { get; set; }
    public string? UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    /// <summary>True para eventos automáticos (tomó, liberó, reasignó, cerrada).</summary>
    public bool EsSistema { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

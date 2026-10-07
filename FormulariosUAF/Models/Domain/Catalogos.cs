namespace FormulariosUAF.Models.Domain;

// Catálogos de la declaración (T_TIPODOCUMENTO, T_PAIS, T_NACIONALIDAD).
// Sus filas iniciales se cargan desde Data/CatalogosSeed.cs vía migración.
// Orden: menor primero (Chile/Chilena/RUT van primero); a igual orden, alfabético.

public class TipoDocumento
{
    /// <summary>Id fijo del RUT (único tipo que se autoformatea como 12.345.678-9).</summary>
    public const int IdRut = 1;

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Orden { get; set; }
    public bool Activo { get; set; } = true;
}

public class Pais
{
    public const int IdChile = 37;

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Orden { get; set; }
    public bool Activo { get; set; } = true;
}

public class Nacionalidad
{
    public const int IdChilena = 37;

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Orden { get; set; }
    public bool Activo { get; set; } = true;
}

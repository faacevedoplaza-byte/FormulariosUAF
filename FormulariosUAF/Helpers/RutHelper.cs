namespace FormulariosUAF.Helpers;

/// <summary>
/// Utilidades para RUT chileno. Normaliza (sin puntos ni guión, en mayúscula)
/// y valida el dígito verificador. El RUT normalizado se usa como UserName.
/// </summary>
public static class RutHelper
{
    /// <summary>Quita puntos, guión y espacios; deja solo dígitos y K, en mayúscula. Ej: "77.706.206-9" -> "777062069".</summary>
    public static string Normalizar(string? rut)
    {
        if (string.IsNullOrWhiteSpace(rut)) return string.Empty;
        return new string(rut.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();
    }

    /// <summary>Valida el dígito verificador (módulo 11).</summary>
    public static bool EsValido(string? rut)
    {
        var n = Normalizar(rut);
        if (n.Length < 2) return false;

        var cuerpo = n[..^1];
        var dv = n[^1];
        if (!cuerpo.All(char.IsDigit)) return false;

        int suma = 0, mult = 2;
        for (int i = cuerpo.Length - 1; i >= 0; i--)
        {
            suma += (cuerpo[i] - '0') * mult;
            mult = mult == 7 ? 2 : mult + 1;
        }

        int resto = 11 - (suma % 11);
        char dvEsperado = resto == 11 ? '0' : resto == 10 ? 'K' : (char)('0' + resto);
        return dv == dvEsperado;
    }
}

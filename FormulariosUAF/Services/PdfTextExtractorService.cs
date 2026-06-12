namespace FormulariosUAF.Services;

// Extractor básico: lee el texto plano embebido en el PDF usando iText-like parsing.
// Para PDFs escaneados (imagen) el texto quedará vacío — la alerta DocumentUnreadable se generará.
public class PdfTextExtractorService : ITaxFolderTextExtractor
{
    public async Task<string> ExtractTextAsync(Stream stream, string mimeType)
    {
        if (!mimeType.Contains("pdf", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        try
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var bytes = ms.ToArray();
            // Extracción de texto plano de un PDF sin dependencias de terceros:
            // Busca cadenas entre paréntesis dentro de streams de contenido PDF.
            var text = ExtractPdfText(bytes);
            return text;
        }
        catch
        {
            return string.Empty;
        }
    }

    private static string ExtractPdfText(byte[] pdfBytes)
    {
        var sb = new System.Text.StringBuilder();
        var content = System.Text.Encoding.Latin1.GetString(pdfBytes);

        // Extraer texto de objetos BT...ET (bloques de texto PDF)
        int pos = 0;
        while (pos < content.Length)
        {
            int btIdx = content.IndexOf("BT", pos, StringComparison.Ordinal);
            if (btIdx < 0) break;
            int etIdx = content.IndexOf("ET", btIdx + 2, StringComparison.Ordinal);
            if (etIdx < 0) break;

            var block = content[btIdx..etIdx];
            // Extraer strings entre paréntesis: (texto)
            int i = 0;
            while (i < block.Length)
            {
                if (block[i] == '(')
                {
                    int end = FindClosingParen(block, i + 1);
                    if (end > i)
                    {
                        var raw = block.Substring(i + 1, end - i - 1);
                        // Decode simple PDF escape sequences
                        raw = raw.Replace("\\n", " ").Replace("\\r", " ")
                                 .Replace("\\t", " ").Replace("\\(", "(")
                                 .Replace("\\)", ")").Replace("\\\\", "\\");
                        sb.Append(raw).Append(' ');
                        i = end + 1;
                        continue;
                    }
                }
                i++;
            }
            sb.AppendLine();
            pos = etIdx + 2;
        }

        return sb.ToString();
    }

    private static int FindClosingParen(string s, int start)
    {
        int depth = 1;
        for (int i = start; i < s.Length; i++)
        {
            if (s[i] == '\\') { i++; continue; }
            if (s[i] == '(') depth++;
            else if (s[i] == ')') { depth--; if (depth == 0) return i; }
        }
        return -1;
    }
}

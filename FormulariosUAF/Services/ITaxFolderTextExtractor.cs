namespace FormulariosUAF.Services;

public interface ITaxFolderTextExtractor
{
    Task<string> ExtractTextAsync(Stream stream, string mimeType);
}

namespace FormulariosUAF.Services;

public interface IExpedienteExportService
{
    Task<byte[]> ExportarExpedienteZipAsync(Guid requestId);
    string GetNombreArchivo(string rutCliente, Guid requestId);
}

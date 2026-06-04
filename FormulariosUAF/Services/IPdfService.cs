using FormulariosUAF.Models.Domain;

namespace FormulariosUAF.Services;

public interface IPdfService
{
    byte[] GenerateBeneficialOwnerDeclarationPdf(Request request);
    byte[] GeneratePepDeclarationPdf(Request request);
    byte[] GenerateConsolidatedPdf(Request request);
}

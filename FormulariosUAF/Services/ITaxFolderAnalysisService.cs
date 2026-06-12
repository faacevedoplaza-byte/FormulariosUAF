using FormulariosUAF.Models.Domain;

namespace FormulariosUAF.Services;

public interface ITaxFolderAnalysisService
{
    Task<TaxFolderAnalysis> AnalyzeAndCompareAsync(Request request, Stream carpetaStream);
}

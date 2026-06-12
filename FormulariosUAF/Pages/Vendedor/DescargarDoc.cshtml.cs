using FormulariosUAF.Data;
using FormulariosUAF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Pages.Vendedor;

public class DescargarDocModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IFileStorageService _storage;

    public DescargarDocModel(ApplicationDbContext db, IFileStorageService storage)
    {
        _db = db;
        _storage = storage;
    }

    public async Task<IActionResult> OnGetAsync(int docId)
    {
        var doc = await _db.Documents
            .FirstOrDefaultAsync(d => d.Id == docId && d.IsActive);
        if (doc is null) return NotFound();

        var stream = await _storage.GetFileAsync(doc.StoragePath);
        return File(stream, doc.MimeType, doc.OriginalFileName);
    }
}

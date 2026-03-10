namespace Eventify.Backend.Services.Codebook;

using Eventify.Backend.Constants;
using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Response.Codebook;
using Eventify.Backend.Services;
using Eventify.Backend.Services.Default;
using Microsoft.EntityFrameworkCore;

/// <summary>Vraća stavke šifarnika prema tipu (Category → tablica Categories, itd.).</summary>
public class CodebookService : Service, ICodebookService
{
    private readonly DataContext _db;

    public CodebookService(IServiceProvider sp, DataContext db) : base(sp) => _db = db;

    public CodebookList GetAll(ECodebook codebook)
    {
        var items = codebook switch
        {
            ECodebook.Category => _db.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select((c, i) => new CodebookModel { Id = c.Id, Code = c.Name, Name = c.Name, OrdinalNo = i })
                .ToList(),
            ECodebook.EventType => GetEventTypes(),
            ECodebook.TicketType => GetTicketTypes(),
            _ => new List<CodebookModel>()
        };

        return new CodebookList { Items = items };
    }

    /// <summary>Tipovi događaja – za sada fiksna lista (može kasnije iz tablice).</summary>
    private static List<CodebookModel> GetEventTypes()
    {
        return new List<CodebookModel>
        {
            new() { Id = 1, Code = "concert", Name = "Koncert", OrdinalNo = 0 },
            new() { Id = 2, Code = "conference", Name = "Konferencija", OrdinalNo = 1 },
            new() { Id = 3, Code = "workshop", Name = "Radionica", OrdinalNo = 2 },
            new() { Id = 4, Code = "festival", Name = "Festival", OrdinalNo = 3 },
            new() { Id = 5, Code = "other", Name = "Ostalo", OrdinalNo = 4 }
        };
    }

    /// <summary>Tipovi karata – za sada fiksna lista (može kasnije iz tablice).</summary>
    private static List<CodebookModel> GetTicketTypes()
    {
        return new List<CodebookModel>
        {
            new() { Id = 1, Code = "standard", Name = "Standardna", OrdinalNo = 0 },
            new() { Id = 2, Code = "vip", Name = "VIP", OrdinalNo = 1 },
            new() { Id = 3, Code = "early", Name = "Early bird", OrdinalNo = 2 }
        };
    }
}

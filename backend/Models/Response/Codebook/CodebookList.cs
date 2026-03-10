namespace Eventify.Backend.Models.Response.Codebook;

/// <summary>Lista stavki šifarnika + datum odgovora (npr. za ETag / cache).</summary>
public class CodebookList
{
    public List<CodebookModel> Items { get; set; } = new();
    public DateTime ResponseDate { get; set; } = DateTime.UtcNow;
}

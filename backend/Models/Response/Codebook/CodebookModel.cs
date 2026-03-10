namespace Eventify.Backend.Models.Response.Codebook;

/// <summary>Jedna stavka šifarnika (za dropdown / listu opcija).</summary>
public class CodebookModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int OrdinalNo { get; set; }
}

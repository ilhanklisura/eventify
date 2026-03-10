namespace Eventify.Backend.Models.Option;

public class AppOptions
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public TokenOptions TokenOptions { get; set; } = new();
}

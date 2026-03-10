namespace Eventify.Backend.Models.Option;

public class ConnectionString
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Value { get; set; }
}

public class ConnectionStrings
{
    public string? Default { get; set; }
    public IList<ConnectionString> Strings { get; set; } = new List<ConnectionString>();

    public ConnectionString? GetDefault()
    {
        if (string.IsNullOrWhiteSpace(Default)) return null;
        return Strings?.FirstOrDefault(s =>
            s.Name.Equals(Default, StringComparison.OrdinalIgnoreCase));
    }
}

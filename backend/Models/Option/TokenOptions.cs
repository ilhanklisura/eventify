namespace Eventify.Backend.Models.Option;

public class TokenOptions
{
    public string SigningKey { get; set; } = "eventify_signing_key_at_least_32_characters_long";
    public string Issuer { get; set; } = "eventify_api";
    public int TokenDurationHours { get; set; } = 8;
}

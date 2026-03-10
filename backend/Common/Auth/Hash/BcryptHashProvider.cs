namespace Eventify.Backend.Common.Auth.Hash;

using BCryptNet = BCrypt.Net.BCrypt;

public class BcryptHashProvider : IHashProvider
{
    public string HashPassword(string password) => BCryptNet.HashPassword(password);

    public bool VerifyPassword(string password, string hash)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash)) return false;
        try { return BCryptNet.Verify(password, hash); }
        catch { return false; }
    }
}

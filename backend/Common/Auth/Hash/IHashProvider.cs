namespace Eventify.Backend.Common.Auth.Hash;

public interface IHashProvider
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}

namespace Security.Services.Security;

public class IPasswordHasher
{
    string HashPassword(string password, out string salt);
    bool verifyPassword(string password, string hash, string salt);
}
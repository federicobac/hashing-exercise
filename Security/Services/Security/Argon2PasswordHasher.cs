using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Security.Services.Security;

public class Argon2PasswordHasher : IPasswordHasher
{
    /*
     * Takes an existing salt and it returns hash.salt
     */
    public string HashPassword(string password, string salt)
    {
        Argon2id argon2 = new Argon2id(
            Encoding.UTF8.GetBytes(password));

        argon2.Salt = Convert.FromBase64String(salt);
        argon2.MemorySize = 64 * 1024;
        argon2.Iterations = 3;
        argon2.DegreeOfParallelism = 4;

        string hash = Convert.ToBase64String(argon2.GetBytes(32));

        return $"{hash}.{salt}";
    }

    /*
     * Creates random salt and then call HashPassword
     * Method used for registering a new user
     */
    public string HashAndSaltPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        return HashPassword(password, Convert.ToBase64String(salt));
    }
    
    /*
     * Login phase
     */
    public bool VerifyHashedPassword(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split('.');

        string hash = parts[0];
        string salt = parts[1];

        string newHashedPassword = HashPassword(password, salt);
        return newHashedPassword == hashedPassword;
    }
}
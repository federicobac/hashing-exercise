using DataAccess.Entities;
using DataAccess.Repositories;
using Security.Services.Security;

namespace Security.Services.Implementation;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepo,
        IPasswordHasher passwordHasher)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
    }
    public bool TryLogin(string username, string password)
    {
        User? user = _userRepo.GetUserByUsername(username);

        if (user == null)
        {
            return false;
        }

        return _passwordHasher.VerifyHashedPassword(
            password,
            user.PasswordHash
        );
    }

    public void TryRegister(string username, string password)
    {
        string passwordHash = _passwordHasher.HashAndSaltPassword(password);

        User user = new User
        {
            Username = username,
            PasswordHash = passwordHash
        };

        _userRepo.CreateUser(user);
    }
}
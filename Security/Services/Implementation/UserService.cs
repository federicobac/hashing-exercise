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
    public bool TryLogin(User user)
    {
        return _userRepo.GetUserByUsername(user.Username).Password == user.Password;
    }

    public void TryRegister(User user)
    {
        _userRepo.CreateUser(user);
    }
}
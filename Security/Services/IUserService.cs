using DataAccess.Entities;

namespace Security.Services;

public interface IUserService
{
    bool TryLogin(string username, string password);
    
    void TryRegister(string username, string password);
}
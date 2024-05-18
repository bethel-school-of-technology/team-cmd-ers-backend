using Fit_Trac.Models;

namespace Fit_Trac.Repositories;

public interface IAuthService
{
    User CreateUser(User user);
    string SignIn(string email, string password);
}
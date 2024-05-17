using BCrypt.Net;
using Fit_Trac.Migrations;
using Fit_Trac.Models;
using bcrypt = BCrypt.Net.BCrypt;

namespace Fit_Trac.Repositories;

public class AuthService : IAuthService
{
    private readonly GoalDbContext _context;

    public AuthService(GoalDbContext context)
    {
        _context = context;
    }
    public User CreateUser(User user)
    {
        //HashPassword
        var hashedPassword = bcrypt.HashPassword(user.Password);
        user.Password = hashedPassword;

        //Creates New User
        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string SignIn(string email, string password)
    {
        //Checks DB for our user when login is attempted
        var user = _context.User.SingleOrDefault(e => e.Email == email);
        var verified = false;

        if (user != null)
        {
            verified = bcrypt.Verify(password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }

        //Create and return JWT
    }
}
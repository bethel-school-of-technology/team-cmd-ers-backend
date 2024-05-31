using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Fit_Trac.Migrations;
using Fit_Trac.Models;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace Fit_Trac.Repositories;

public class AuthService : IAuthService
{
    private readonly GoalDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(GoalDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
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

    public void DeleteUserData(int userId)
    {
        var user = _context.User.SingleOrDefault(u => u.UserId == userId);

        if(user != null)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
        }
    }

    //retrieves a single user
    public UserDTO GetUserById(int userId)
    {
        var user = _context.User.SingleOrDefault(u => u.UserId == userId);
        
        UserDTO userDTO = new UserDTO{
            FirstName = user?.FirstName,
            LastName = user?.LastName,
            Email = user?.Email
        };

        return userDTO;
    }

    public string SignIn(string email, string password)
    {
        //Checks DB for our user when login is attempted
        var user = _context.User.SingleOrDefault(e => e.Email == email);
        var verified = false;

        //Updates verified if the users password matches with the hashed password
        if (user != null)
        {
            verified = bcrypt.Verify(password, user.Password);
        }

        //returns an empty string if the user doesn't exist or is unverified
        if (user == null || !verified)
        {
            return String.Empty;
        }

        //Create and return JWT
        return TokenConstruction(user);
    }

    //Updates the users information
    public User UpdateUserInfo(UserDTO updatedUser, int userId)
    {
        var user = _context.User.SingleOrDefault(u => u.UserId == userId);

        if( user != null)
        {
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            //user.Password = user.Password; //Not going to allow password updates due to length of time to include so this is just to initialize the password property to allow the change.
            _context.SaveChanges();
        }

        return user;
    }

    private string TokenConstruction(User user)
    {
        var secretString = _config.GetValue<String>("TokenSecret");
        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretString));

        var signingCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

        //Creates claims to add to our JWT/ This is information that the user has given us before
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName ?? "")
        };

        //Creates JWT Token
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCredentials
        );

        //Encodes Our Token
        var jwtCode = new JwtSecurityTokenHandler().WriteToken(jwt);

        return jwtCode;
    }
}
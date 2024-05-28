using System.Security.Claims;
using Fit_Trac.Models;
using Fit_Trac.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fit_Trac.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController: ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }
    
    [HttpPost]
    [Route("signup")]
    public ActionResult CreateUser(User user)
    {
        if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        _authService.CreateUser(user);
        return NoContent();
    }

    [HttpGet]
    [Route("signin")]
    public ActionResult<String> UserLogin(string email, string password)
    {
        //Makes sure the required information is there and if it isn't returns a badrequest code
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest();
        }

        var token = _authService.SignIn(email, password);

        //Makes sure the user supplied information is correct by returning a token from AuthService
        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpGet]
    [Route("user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetUser()
    {
        var userId = GetUserId();
        return Ok(_authService.GetUserById(userId)); 
    }

    [HttpPut]
    [Route("{email}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> UpdateUser(User user)
    {
        if(user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var userId = GetUserId();
        var updatedUser = _authService.UpdateUserInfo(user, userId);

        return Ok(updatedUser);
    }

    [HttpDelete]
    [Route("{email}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> DeleteUser(string email)
    {
        var userId = GetUserId();
        _authService.DeleteUserData(userId);
        return NoContent();
    }
}
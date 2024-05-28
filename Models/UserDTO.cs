namespace Fit_Trac.Models;

//User Data Transfer Object to transfer non sensitive data from User Model to Frontend
public class UserDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
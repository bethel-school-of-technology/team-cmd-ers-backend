using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fit_Trac.Models;

public class User
{
    [JsonIgnore]
    public int UserId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [JsonIgnore]
    public string? Password { get; set; }
    [JsonIgnore]
    public virtual ICollection<Goal>? Goal { get; set; }

}
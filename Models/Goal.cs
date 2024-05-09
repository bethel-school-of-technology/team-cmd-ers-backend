using System.ComponentModel.DataAnnotations;

namespace Fit_Trac.Models;

public class Goal
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Type { get; set; }
    [Required]
    public int GoalToReach { get; set; }
    public string DateCreated { get; set; }
    //public int UserId { get; set; }
    //public virtual User? User { get; set; }

    public Goal()
    {
        this.DateCreated = DateTime.Now.ToString("MM-dd-yyyy");
    }
    
}
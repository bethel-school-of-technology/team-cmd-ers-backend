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
    [Required]
    public int UserCurrent { get; set; } //where the user is currently on the way to acheiving their goal
    public string DateCreated { get; set; }
    //public int UserId { get; set; }
    //public virtual User? User { get; set; }

    public Goal()
    {
        this.DateCreated = DateTime.Now.ToString("MM-dd-yyyy");
    }
    
}
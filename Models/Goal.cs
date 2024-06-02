using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fit_Trac.Models;

public class Goal
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Type { get; set; }
    public string? Description { get; set; }
    [Required]
    public int GoalToReach { get; set; }
    [Required]
    public string DateCreated { get; set; }
    [Required]
    public int UserId { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
    public ICollection<DailyGoalInput>? DailyGoalInput { get; set; }

    public Goal()
    {
        DateCreated = DateTime.Now.ToString("MM-dd-yyyy");
    }
    
}
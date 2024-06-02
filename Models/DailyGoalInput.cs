using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fit_Trac.Models;

public class DailyGoalInput
{
    public int InputId { get; set; }
    [Required]
    public int ProgressInput { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int GoalId { get; set; }
    [JsonIgnore]
    public virtual Goal? Goal { get; set; }

    public DailyGoalInput()
    {
        Date = DateTime.Now;
    }
}
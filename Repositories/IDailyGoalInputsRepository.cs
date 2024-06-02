using Fit_Trac.Models;

namespace Fit_Trac.Repositories;

public interface IDailyGoalInputsRepository
{
    DailyGoalInput CreateDailyInput(DailyGoalInput goalInputs);
    IEnumerable<DailyGoalInput> GetDailyGoalInputs(int goalId);
}
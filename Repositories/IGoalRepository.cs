using Fit_Trac.Models;

namespace Fit_Trac.Repositories;

public interface IGoalRepository
{
    IEnumerable<Goal> GetAllGoals();
    Goal GetGoalById(int goalId, int userId);
    IEnumerable<Goal> GetGoalsByUserId(int userId);
    Goal CreateGoal(Goal goal);
    Goal UpdateGoal(Goal newGoal, int userId);
    void DeleteGoal(int goalId, int userId);
}
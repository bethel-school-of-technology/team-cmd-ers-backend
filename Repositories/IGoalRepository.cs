using Fit_Trac.Models;

namespace Fit_Trac.Repositories;

public interface IGoalRepository
{
    IEnumerable<Goal> GetAllGoals();
    Goal GetGoalById(int goalId);
    Goal CreateGoal(Goal goal);
    Goal UpdateGoal(Goal newGoal);
    void DeleteGoal(int goalId);
}
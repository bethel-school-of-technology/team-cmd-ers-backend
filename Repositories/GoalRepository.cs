using Fit_Trac.Migrations;
using Fit_Trac.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.ObjectPool;

namespace Fit_Trac.Repositories;

public class GoalRepository : IGoalRepository
{

    private readonly GoalDbContext _context;

    public GoalRepository(GoalDbContext context)
    {
        _context = context;
    }

    public Goal CreateGoal(Goal goal)
    {
        _context.Goal.Add(goal);
        _context.SaveChanges();
        return goal;
    }

    public void DeleteGoal(int goalId)
    {
        var goal = _context.Goal.Find(goalId);
        if(goal != null)
        {
            _context.Goal.Remove(goal);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Goal> GetAllGoals()
    {
        return _context.Goal.ToList();
    }

    public Goal GetGoalById(int goalId)
    {
        return _context.Goal.SingleOrDefault(g => g.Id == goalId);
    }

    public Goal UpdateGoal(Goal newGoal)
    {
        var ogGoal = _context.Goal.Find(newGoal.Id);
        if(ogGoal != null)
        {
            ogGoal.UserProgress = newGoal.UserProgress;
            _context.SaveChanges();
        }

        return ogGoal;
    }
}
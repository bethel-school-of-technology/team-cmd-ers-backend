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

    //Checks to see if the requesting user is authorized to delete this goal
    public void DeleteGoal(int goalId, int userId)
    {
        var goal = _context.Goal.SingleOrDefault(g => g.Id == goalId && g.UserId == userId);
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

    //Checks to see if the requesting user is authorized to access this goal
    public Goal GetGoalById(int goalId, int userId) 
    {
        return _context.Goal.SingleOrDefault(g => g.Id == goalId && g.UserId == userId);
    }

    public IEnumerable<Goal> GetGoalsByUserId(int userId)
    {
        return _context.Goal.Where(g => g.UserId == userId).ToList();
    }

    //Checks to see if the requesting user is authorized to update this goal
    public Goal UpdateGoal(Goal updatedGoal, int userId)
    {
        var ogGoal = _context.Goal.SingleOrDefault(g => g.Id == updatedGoal.Id && g.UserId == userId);
        if(ogGoal != null)
        {
            ogGoal.Description = updatedGoal.Description;
            ogGoal.Name = updatedGoal.Name;
            _context.SaveChanges();
        }

        return ogGoal;
    }
}
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

    public void DeleteGoal(int goalId) //Research how to check if the user sending the request is the owner of the goal
    {
        var goal = _context.Goal.Find(goalId);
        if(goal != null)
        {
            _context.Goal.Remove(goal);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Goal> GetAllGoals() //getallusergoals(int userId) _context.User.Include(Goal).Where(goal.userId === userId).ToList()??????
    {
        return _context.Goal.ToList();
    }

    public Goal GetGoalById(int goalId) //Research how to check if the user sending the request is the owner of the goal
    {
        return _context.Goal.SingleOrDefault(g => g.Id == goalId);
    }

    public Goal UpdateGoal(Goal updatedGoal) //Research how to check if the user sending the request is the owner of the goal
    {
        var ogGoal = _context.Goal.Find(updatedGoal.Id);
        if(ogGoal != null)
        {
            ogGoal.UserProgress = updatedGoal.UserProgress;
            ogGoal.Description = updatedGoal.Description;
            ogGoal.Name = updatedGoal.Name;
            _context.SaveChanges();
        }

        return ogGoal;
    }
}
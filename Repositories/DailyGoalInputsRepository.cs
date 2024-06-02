using Fit_Trac.Migrations;
using Fit_Trac.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Fit_Trac.Repositories;

public class DailyGoalInputsRepository : IDailyGoalInputsRepository
{
    private GoalDbContext _context;
    public DailyGoalInputsRepository(GoalDbContext context)
    {
        _context = context;
    }
    public DailyGoalInput CreateDailyInput(DailyGoalInput goalInputs)
    {
        _context.DailyGoalInputs.Add(goalInputs);
        _context.SaveChanges();
        return goalInputs;
    }

    public IEnumerable<DailyGoalInput> GetDailyGoalInputs(int goalId)
    {
        return _context.DailyGoalInputs.Where(d => d.GoalId == goalId);
    }
}
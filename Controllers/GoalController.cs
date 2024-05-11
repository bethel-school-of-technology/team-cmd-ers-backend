using Fit_Trac.Models;
using Fit_Trac.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fit_Trac.Controllers;

[ApiController]
[Route("[controller]")]
public class GoalController : ControllerBase
{
    private readonly ILogger<GoalController> _logger;
    private readonly IGoalRepository _goalRepository;

    public GoalController(ILogger<GoalController> logger, IGoalRepository repository)
    {
        _logger = logger;
        _goalRepository = repository;
    }

    //HttpGet to retieve all Goals in the Database (probably update to GetAllUserGoals when we implement the user model)
    [HttpGet]
    public ActionResult<IEnumerable<Goal>> GetAllGoals()
    {
        return Ok(_goalRepository.GetAllGoals());
    }
    //Retieves an individual goal based on goal id 
    [HttpGet]
    [Route("{goalId:int}")]
    public ActionResult<Goal> GetGoalById(int goalId)
    {
        var goal = _goalRepository.GetGoalById(goalId);
        if(goal == null)
        {
            return NotFound();
        }

        return Ok(goal);
    }
    //adds a new goal
    [HttpPost]
    public ActionResult<Goal> CreateGoal(Goal newGoal)
    {
        if(!ModelState.IsValid || newGoal == null)
        {
            return BadRequest();
        }

        var goal = _goalRepository.CreateGoal(newGoal);
        return Ok(goal);
    }
    //Updates a users goal (will need to update when we implement the user model)
    [HttpPut]
    [Route("{goalId:int}")]
    public ActionResult<Goal> UpdateGoal(Goal goal)
    {
        if(!ModelState.IsValid || goal == null)
        {
            return BadRequest();
        }

        var updatedGoal = _goalRepository.UpdateGoal(goal);
        return Ok(updatedGoal);
    }
    //Deletes a users goal 
    [HttpDelete]
    [Route("{goalId:int}")]
    public ActionResult<Goal> DeleteGoal(int goalId)
    {
        _goalRepository.DeleteGoal(goalId);
        return NoContent();
    }
}
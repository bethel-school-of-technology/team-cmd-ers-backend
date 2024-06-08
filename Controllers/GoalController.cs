using System.Security.Claims;
using Fit_Trac.Models;
using Fit_Trac.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

    //Used to get the userId from JWT token 
    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }

    //HttpGet to retieve all Goals in the Database
    [HttpGet]
    public ActionResult<IEnumerable<Goal>> GetAllGoals()
    {
        return Ok(_goalRepository.GetAllGoals());
    }

    [HttpGet]
    [Route("user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Goal>> GetGoalsByUserId()
    {
        var userId = GetUserId();
        return Ok(_goalRepository.GetGoalsByUserId(userId));
    }

    //Retieves an individual goal based on goal id 
    [HttpGet]
    [Route("{goalId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Goal> GetGoalById(int goalId)
    {
        var userId = GetUserId();
        var goal = _goalRepository.GetGoalById(goalId, userId);

        if(goal == null)
        {
            return NotFound();
        }

        return Ok(goal);
    }
    //adds a new goal
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Goal> CreateGoal(Goal newGoal)
    {
        var userId = GetUserId();
        newGoal.UserId = userId;

        if(!ModelState.IsValid || newGoal == null)
        {
            return BadRequest();
        }

        var goal = _goalRepository.CreateGoal(newGoal);
        return Ok(goal);
    }

    [HttpPut]
    [Route("{goalId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Goal> UpdateGoal(Goal goal)
    {
        if(!ModelState.IsValid || goal == null)
        {
            return BadRequest();
        }
        
        var userId = GetUserId();
        var updatedGoal = _goalRepository.UpdateGoal(goal, userId);

        return Ok(updatedGoal);
    }
    //Deletes a users goal 
    [HttpDelete]
    [Route("{goalId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Goal> DeleteGoal(int goalId)
    {
        var userId = GetUserId();
        _goalRepository.DeleteGoal(goalId, userId);
        return NoContent();
    }
}
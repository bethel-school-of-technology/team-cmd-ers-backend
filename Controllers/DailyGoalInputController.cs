using Fit_Trac.Models;
using Fit_Trac.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Fit_Trac.Controllers;

[ApiController]
[Route("[controller]")]
public class DailyGoalInputController : ControllerBase
{
    private readonly IDailyGoalInputsRepository _dailyInputRepository;
    private readonly ILogger<DailyGoalInputController> _logger;

    public DailyGoalInputController(ILogger<DailyGoalInputController> logger, IDailyGoalInputsRepository repository)
    {
        _logger = logger;
        _dailyInputRepository = repository;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<DailyGoalInput>> GetDailyGoalInputs(int goalId)
    {
        return Ok(_dailyInputRepository.GetDailyGoalInputs(goalId));
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<DailyGoalInput> CreateGoalInput(DailyGoalInput dailyGoalInput, int goalId)
    {   
        dailyGoalInput.GoalId = goalId;

        if(dailyGoalInput == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var dailyInput = _dailyInputRepository.CreateDailyInput(dailyGoalInput);
        return Ok(dailyInput);
    }
}
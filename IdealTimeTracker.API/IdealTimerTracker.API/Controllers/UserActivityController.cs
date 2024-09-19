using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.UserActivity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IdealTimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserActivityController : ControllerBase
    {
        private readonly IAdminAction _adminAction;
        private readonly ILogger<UserActivityController> _logger;
        public UserActivityController(IAdminAction adminAction, ILogger<UserActivityController> logger)
        {
            _logger = logger;
            _adminAction = adminAction;
        }

        //[HttpPost]
        //[ProducesResponseType(typeof(UserReponseDTO),StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<UserReponseDTO>> AddUser(AddUserDTO addUserDTO)
        //{
        //    var user = await _adminAction.AddUser(addUserDTO);
        //    return CreatedAtAction("AddUser", user);
        //}

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(typeof(UserActivity), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserActivity>> AddUserActivity(AddUserActivityDTO addUserActivityDTO)
        {
            _logger.LogInformation("AddUserActivity called with {0}", addUserActivityDTO);
            var userActivity = await _adminAction.AddActivity(addUserActivityDTO);
            _logger.LogInformation($"Add user activity: {userActivity}");
            return CreatedAtAction("AddUserActivity", userActivity);
        }



        [Authorize(Roles = "admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUserActivity(UpdateUserActivityDTO updateUserActivityDTO)
        {
            _logger.LogInformation("UpdateUserActivity called with {0}", updateUserActivityDTO);
            await _adminAction.UpdateActivity(updateUserActivityDTO);
            _logger.LogInformation("UpdateUserActivity completed");
            return Accepted();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<UserActivity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserActivity>>> GetAllUserActivity()
        {
            _logger.LogInformation("GetAllUserActivity called");
            var userActivities = await _adminAction.GetAllActivity();
            _logger.LogInformation($"GetAllUserActivity completed with {0}", userActivities);
            return Ok(userActivities);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [ProducesResponseType(typeof(List<UserActivity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserActivity>>> DeleteActivity([FromQuery] int id)
        {
            _logger.LogInformation("DeleteActivity called with {0}", id);
            var userActivity = await _adminAction.DeleteActivitybyId(id);
            _logger.LogInformation($"DeleteActivity completed with {0}", userActivity);
            return Ok(userActivity);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("id")]
        [ProducesResponseType(typeof(List<UserActivity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserActivity>>> GetActivity([FromQuery] int id)
        {
            _logger.LogInformation("GetActivity called with {0}", id);
            var userActivity = await _adminAction.GetActivitybyId(id);
            _logger.LogInformation($"GetActivity completed with {0}", userActivity);
            return Ok(userActivity);
        }
    }
}

using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Users;
using IdealTImeTracker.API.Models.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IdealTimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserController : ControllerBase
    {
        private readonly IUserAction _userAction;
        private readonly IAdminAction _adminAction;
        private readonly IApplicationAction _applicationAction;

        private readonly ILogger<UserController> _logger;

        public UserController(IUserAction userAction, IAdminAction adminAction, IApplicationAction applicationAction, ILogger<UserController> logger)
        {
            _adminAction = adminAction;
            _userAction = userAction;
            _logger = logger;
            _applicationAction = applicationAction;
        }

        [HttpGet("Employee")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> GetAllEmployees()
            {
            _logger.LogInformation("GetAllUser is called");
            var users = await _applicationAction.GetAllEmployees();
            _logger.LogInformation($"Get all users: {users}");
            return Ok(users);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(typeof(UserReponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserReponseDTO>> AddUser(AddUserDTO addUserDTO)
            {
            _logger.LogInformation("AddUser called with {0}", addUserDTO);
            var user = await _adminAction.AddUser(addUserDTO);
            _logger.LogInformation($"Add user: {user}");
            return CreatedAtAction("AddUser", user);

        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserLoginResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLoginResponseDTO>> Login(UserLoginDTO userLoginDTO)
        {
            _logger.LogInformation("Login called");
            var user = await _userAction.Login(userLoginDTO);
            _logger.LogInformation($"Login user: {user}");
            return Ok(user);
        }

        [Authorize]
        [HttpPut("Password")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> UpdatePassword(UpdateUserDTO updateUserDTO)
        {

            _logger.LogInformation("UpdatePassword called with {0}", updateUserDTO);
            await _userAction.UpdatePassword(updateUserDTO);
            _logger.LogInformation("UpdatePassword completed");
            return Accepted();

        }

        [Authorize(Roles = "admin")]
        [HttpPut("Report")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> UpdateReport(UpdateUserDTO updateUserDTO)
        {
            _logger.LogInformation("Update report called {0}", updateUserDTO);
            await _adminAction.UpdateReportingManager(updateUserDTO);
            _logger.LogInformation("Update report completed");
            return Accepted();

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(typeof(List<UserReponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserPaginationReponseDto>> GetAllUser([FromQuery] int pageNumber )
        {
            _logger.LogInformation("GetAllUser called");
            var users = await _adminAction.GetAllUser(pageNumber);
            _logger.LogInformation("GetAllUser completed");
            return Ok(users);
        }

        [Authorize(Roles = "manager")]
        [HttpGet("EmployeeIds")]
        [ProducesResponseType(typeof(List<UserDropDownDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserDropDownDTO>>> GetAllUserEmpId([FromQuery] string ManagerId)
        {
            _logger.LogInformation("GetAllUserEmpId called with {0}", ManagerId);
            var users = await _adminAction.GetAllUserEmpIds(ManagerId);
            _logger.LogInformation("GetAllUserEmpId completed");
            return Ok(users);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("IdsByRole")]
        [ProducesResponseType(typeof(List<UserDropDownDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserDropDownDTO>>> GetAllUserIdbyRole([FromQuery] string role)
        {
            _logger.LogInformation("GetAllUserIdbyRole called with {0}", role);
            var users = await _adminAction.GetAllUserIdbyRole(role);
            _logger.LogInformation("GetAllUserIdbyRole completed");
            return Ok(users);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("id")]
        [ProducesResponseType(typeof(List<UserReponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserReponseDTO>> GetUser([FromQuery] string id)
        {
            _logger.LogInformation("GetUser called with {0}", id);
            var user = await _adminAction.GetUserbyId(id);
            _logger.LogInformation("GetUser completed");
            return Ok(user);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("id")]
        [ProducesResponseType(typeof(List<UserReponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserReponseDTO>> DeleteUser([FromQuery] string id)
        {
            _logger.LogInformation("DeleteUser called with {0}", id);
            var user = await _adminAction.DeleteUserbyId(id);
            _logger.LogInformation("DeleteUser completed");     
            return Ok(user);
        }

    }
}


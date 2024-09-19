using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models;
using IdealTimeTracker.API.Models.DTOs.Userlog;
using IdealTImeTracker.API.Models.DTOs;
using IdealTImeTracker.API.Models.DTOs.Userlog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IdealTimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    [Authorize(Roles = "manager")]
    public class UserLogController : ControllerBase
    {
        private readonly IManagerAction _managerAction;
        private readonly IApplicationAction _applicationAction;
        private readonly ILogger<UserLogController> _logger;
        public UserLogController(IManagerAction managerAction, IApplicationAction applicationAction,ILogger<UserLogController> logger)
        {
            _applicationAction = applicationAction;
            _managerAction = managerAction;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost("Logs")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLog>> AddLogs(List<AddUserLogDTO> addUserLogDTOs)
        {
            _logger.LogInformation("AddLogs called with {0}", addUserLogDTOs);
            var user = await _applicationAction.AddLogs(addUserLogDTOs);
            _logger.LogInformation($"Add user logs: {user}");
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<UserLog>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLogPaginationResponseDTO>> GetUserLogs(GetUserLogDTO getUserLogDTO)
        {
            _logger.LogInformation("GetUserLogs called with {0}", getUserLogDTO);
            var userLogs = await _managerAction.GetAllLogsForUser(getUserLogDTO);
            _logger.LogInformation($"Get user logs: {userLogs}");
            return Ok(userLogs);
        }

        [HttpPost("excel")]
        [ProducesResponseType(typeof(List<UserLog>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserLog>>> GetUserLogsExcel(GetUserLogDTO getUserLogDTO)
        {
            _logger.LogInformation("GetUserLogs called with {0}", getUserLogDTO);
            var userLogs = await _managerAction.GetAllLogsForUserExcel(getUserLogDTO);
            _logger.LogInformation($"Get user logs: {userLogs}");
            return Ok(userLogs);
        }


        [HttpPost("consolidate")]
        [ProducesResponseType(typeof(List<UserLogConsolidatedResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLogConsolidatedPaginationResponseDTO>> GetUserConsolidatedLogs(GetUserLogDTO getUserLogDTO)
        {
            _logger.LogInformation("GetUserConsolidatedLogs called with {0}", getUserLogDTO);
            var userLogs = await _managerAction.GetConsolatedLogsForUser(getUserLogDTO);
            _logger.LogInformation($"Get user consolidated logs: {userLogs}");
            return Ok(userLogs);
        }


        [AllowAnonymous]
        [HttpPost("excel/consolidate")]
        [ProducesResponseType(typeof(List<UserLogConsolidatedResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserLogConsolidatedResponseDTO>>> GetUserConsolidatedLogsExcel(GetUserLogDTO getUserLogDTO)
        {
            _logger.LogInformation("GetUserConsolidatedLogs called with {0}", getUserLogDTO);
            var userLogs = await _managerAction.GetConsolatedLogsForUserExcel(getUserLogDTO);
            _logger.LogInformation($"Get user consolidated logs: {userLogs}");
            return Ok(userLogs);
        }


        [AllowAnonymous]
        [HttpPost("details")]
        [ProducesResponseType(typeof(List<UserLogDetailResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UserLogDetailResponseDto>>> GetDetailLogsForUser(GetUserLogDetailDto getUserLogDetailDto)
        {
            _logger.LogInformation("GetDetailLogsForUser called with {0}", getUserLogDetailDto);
            var userLogs = await _managerAction.GetDetailLogsForUser(getUserLogDetailDto);
            _logger.LogInformation($"GetDetailLogsForUser: {userLogs}");
            return Ok(userLogs);
        }


    }

}
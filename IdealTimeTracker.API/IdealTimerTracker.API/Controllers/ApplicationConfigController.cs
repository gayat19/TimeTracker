using IdealTimeTracker.API.Controllers;
using IdealTimeTracker.API.Interfaces;
using IdealTimeTracker.API.Models.DTOs.Users;
using IdealTimeTracker.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdealTImeTracker.API.Models.DTOs.ApplicationConfiguration;
using IdealTImeTracker.API.Models;
using Microsoft.AspNetCore.Cors;

namespace IdealTImeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class ApplicationConfigController : ControllerBase
    {
        private readonly IAdminAction _adminAction;
        private readonly IApplicationAction _applicationAction;

        private readonly ILogger<ApplicationConfigController> _logger;
        public ApplicationConfigController(IAdminAction adminAction,IApplicationAction applicationAction ,ILogger<ApplicationConfigController> logger)
        {
            _logger = logger;
            _adminAction = adminAction;
            _applicationAction = applicationAction;
        }


        //[Authorize(Roles = "admin")]
        [HttpPut("Config")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateConfig(UpdateConfigDTO updateConfigDTO)
        {
            _logger.LogInformation("Update Config called {0}", updateConfigDTO);
            await _adminAction.updateApplicationConfiguration(updateConfigDTO);
            _logger.LogInformation("Update Config completed");
            return Accepted();

        }

        [AllowAnonymous]
        [HttpGet("Config")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationConfigurationResponseDTO>> GetAllConfig()
        {
            _logger.LogInformation("Get Config called");
            var config = await _applicationAction.GetAllConfiguration();
            _logger.LogInformation("Get Config completed");
            return Ok(config);

        }
    }
}

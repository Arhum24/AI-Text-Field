using MedicoBilling.Models;
using MedicoBilling.Services.AI;
using MedicoBilling.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace MedicoBilling.Controllers
{
    [Route("v1/medicobilling")]
    [ApiController]
    public class MedicoBillingController : ControllerBase
    {
        private readonly ILogger<MedicoBillingController> _logger;
        private readonly IAIService _aiService;
        private readonly IUserService _userService;

        public MedicoBillingController(ILogger<MedicoBillingController> logger, IAIService aiService, IUserService userService)
        {
            _logger = logger;
            _aiService = aiService;
            _userService = userService;
        }

        [HttpGet("azalea-callback", Name = "Azalea Callback")]
        public async Task<ActionResult> GetAzaleaAuthToken(
            [FromQuery] string code, 
            [FromQuery] string state
            )
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return BadRequest("Authorization code is missing.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Azalea Callback");
                return Problem(ex.Message);
            }
        }

        [HttpPost("generate-codes", Name = "GenerateCodes")]
        public async Task<ActionResult<DiagnosisCodes>> GenerateCodes(
            [FromBody] CodeGenerate input
            )
        {
            try
            {
                DiagnosisCodes result = await _aiService.InvokeModelAsync(input);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GenerateCodes");
                return Problem(ex.Message);
            }
        }

        [HttpGet("user-history", Name = "GetAllUserHistory")]
        public ActionResult<List<string>> GetAllUserHistory(
            [FromQuery] string userId
            )
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new List<string> { "Empty UserId" };
                }

                return new List<string> { $"History for UserId: {userId}" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllUserHistory");
                return Problem();
            }
        }

        [HttpGet("codes-from-id", Name = "GetCodesFromId")]
        public ActionResult<List<string>> GetCodesFromId(
            [FromQuery] string codeId
            )
        {
            try
            {
                if (string.IsNullOrEmpty(codeId))
                {
                    return new List<string> { "Empty CodeId" };
                }

                return new List<string> { "Code1", "Code2", "Code3" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCodesFromId");
                return Problem();
            }
        }

        [HttpPost("export-codes", Name = "ExportCodes")]
        public ActionResult<List<string>> ExportCodes()
        {
            try
            {
                return new List<string> { "Code1", "Code2", "Code3" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExportCodes");
                return Problem();
            }
        }
        
        [HttpGet("health", Name = "HealthCheck")]
        public ActionResult<string> HealthCheck()
        {
            try
            {
                return "Medico Billings is Up and Running!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Medico Billings App!");
                return Problem("Error in Medico Billings App!");
            }
        }
    }
}

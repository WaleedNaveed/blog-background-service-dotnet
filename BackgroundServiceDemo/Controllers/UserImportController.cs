using BackgroundServiceDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServiceDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImportController : ControllerBase
    {
        private readonly IUserImportTrigger _userImportTrigger;
        private readonly IBGSStatusService _bGSStatusService;

        public UserImportController(IUserImportTrigger userImportTrigger, IBGSStatusService bGSStatusService)
        {
            _userImportTrigger = userImportTrigger;
            _bGSStatusService = bGSStatusService;
        }

        [HttpPost]
        public IActionResult StartUserImport()
        {
            _userImportTrigger.TriggerImport();
            return Ok("User import is started in the background");
        }

        [HttpGet("status")]
        public IActionResult GetUserImportStatus()
        {
            return Ok(new { status = _bGSStatusService.BGSStatus.ToString() });
        }
    }
}

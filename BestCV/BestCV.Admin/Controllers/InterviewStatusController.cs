using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("interview-status")]
    [Authorize]
    public class InterviewStatusController : BaseController
    {
        private readonly ILogger<InterviewStatusController> _logger;

        public InterviewStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<InterviewStatusController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

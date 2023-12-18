using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("interview-type")]
    [Authorize]
    public class InterviewTypeController : BaseController
    {
        private readonly ILogger<InterviewTypeController> _logger;

        public InterviewTypeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<InterviewTypeController>();
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

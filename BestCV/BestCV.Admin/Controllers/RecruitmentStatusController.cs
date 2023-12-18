using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("recruitment-status")]
    [Authorize]
    public class RecruitmentStatusController : BaseController
    {
        private readonly ILogger<RecruitmentStatusController> _logger;

        public RecruitmentStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RecruitmentStatusController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

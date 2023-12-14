using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("candidate-apply-job-status")]
    [Authorize]
    public class CandidateApplyJobStatusController : BaseController
    {
        private readonly ILogger<CandidateApplyJobStatusController> _logger;

        public CandidateApplyJobStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CandidateApplyJobStatusController>();
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

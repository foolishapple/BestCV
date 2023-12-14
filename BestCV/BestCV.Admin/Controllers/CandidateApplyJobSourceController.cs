using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("candidate-apply-job-source")]
    [Authorize]
    public class CandidateApplyJobSourceController : BaseController
    {
        private readonly ILogger<CandidateApplyJobSourceController> _logger;
        public CandidateApplyJobSourceController(ILogger<CandidateApplyJobSourceController> logger)
        {
            _logger = logger;
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("candidate")]
    [Authorize]
    public class CandidateController : BaseController
    {
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CandidateController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

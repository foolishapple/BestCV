using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("top-job-urgent")]
    [Authorize]
    public class TopJobUrgentController : BaseController
    {
        private readonly ILogger<TopJobUrgentController> logger;

        public TopJobUrgentController(ILogger<TopJobUrgentController> _logger)
        {
            logger = _logger;
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("occupation")]
    [Authorize]
    public class OccupationController : BaseController
    {
        private readonly ILogger<OccupationController> _logger;
        public OccupationController(ILogger<OccupationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("list")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

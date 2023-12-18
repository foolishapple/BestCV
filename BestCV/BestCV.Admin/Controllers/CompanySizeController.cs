using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("company-size")]
    [Authorize]

    public class CompanySizeController : BaseController
    {
        private readonly ILogger<CompanySizeController> _logger;

        public CompanySizeController(ILogger<CompanySizeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

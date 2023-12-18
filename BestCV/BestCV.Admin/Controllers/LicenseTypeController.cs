using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BestCV.Admin.Controllers
{
    [Route("license-type")]
    [Authorize]
    public class LicenseTypeController : BaseController
    {
        private readonly ILogger<LicenseTypeController> _logger;
        public LicenseTypeController(ILogger<LicenseTypeController> logger)
        {
            _logger = logger;
        }
        [Route("list")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

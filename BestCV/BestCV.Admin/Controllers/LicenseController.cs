using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BestCV.Admin.Controllers
{
    [Route("license")]
    [Authorize]
    public class LicenseController : BaseController
    {
        private readonly ILogger<LicenseController> _logger;
        public LicenseController(ILogger<LicenseController> logger)
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

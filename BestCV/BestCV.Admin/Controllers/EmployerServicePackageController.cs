using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("employer-service-package")]
    [Authorize]
    public class EmployerServicePackageController : BaseController
    {
        private readonly ILogger<EmployerServicePackageController> _logger;
        public EmployerServicePackageController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmployerServicePackageController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

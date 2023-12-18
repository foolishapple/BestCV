using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("system-config")]
    [Authorize]
    public class SystemConfigController : BaseController
    {
        private readonly ILogger<SystemConfigController> logger;

        public SystemConfigController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<SystemConfigController>();
        }

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

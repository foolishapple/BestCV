using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("menu-type")]
    [Authorize]
    public class MenuTypeController : BaseController
    {
        private readonly ILogger<MenuTypeController> logger;

        public MenuTypeController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<MenuTypeController>();
        }

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

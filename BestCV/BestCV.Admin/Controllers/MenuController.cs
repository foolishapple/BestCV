using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BestCV.Admin.Controllers
{
    [Route("menu")]
    [Authorize]
    public class MenuController : BaseController
    {
		private readonly ILogger<MenuController> _logger;
        public MenuController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MenuController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }

        [Route("menu-home-page")]
        public IActionResult AdminHomePageList()
        {
            return View();
        }
    }
}

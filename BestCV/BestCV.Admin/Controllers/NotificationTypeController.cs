using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("notification-type")]
    [Authorize]
    public class NotificationTypeController : BaseController
    {
        private readonly ILogger<NotificationTypeController> _logger;

        public NotificationTypeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<NotificationTypeController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

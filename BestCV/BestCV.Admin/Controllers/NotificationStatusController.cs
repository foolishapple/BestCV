using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("notification-status")]
    [Authorize]
    public class NotificationStatusController : BaseController
    {
        private readonly ILogger<NotificationStatusController> _logger;

        public NotificationStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<NotificationStatusController>();
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

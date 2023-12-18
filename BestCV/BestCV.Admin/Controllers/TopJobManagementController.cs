using BestCV.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("top-job-management")]
    [Authorize]
    public class TopJobManagementController : BaseController
    {
        private readonly ILogger<TopJobManagementController> _logger;
        public TopJobManagementController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TopJobManagementController>();

        }

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("employer")]
    [Authorize]
    public class EmployerController : BaseController
    {
        private readonly ILogger<EmployerController> _logger;

        public EmployerController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmployerController>();
        }


        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

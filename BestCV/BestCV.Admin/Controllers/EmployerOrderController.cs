using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("employer-order")]
    [Authorize]
    public class EmployerOrderController : BaseController
    {
        private readonly ILogger<EmployerOrderController> _logger;

        public EmployerOrderController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmployerOrderController>();
        }

       
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

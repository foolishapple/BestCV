using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("salary-type")]
    [Authorize]
    public class SalaryTypeController : BaseController
    {
        private readonly ILogger<SalaryTypeController> _logger;
        public SalaryTypeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SalaryTypeController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

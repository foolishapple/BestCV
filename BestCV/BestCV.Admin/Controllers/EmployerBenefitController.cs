using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("employer-benefit")]

    public class EmployerBenefitController : BaseController
    {
        private readonly ILogger<EmployerBenefitController> _logger;
        public EmployerBenefitController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmployerBenefitController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }

}

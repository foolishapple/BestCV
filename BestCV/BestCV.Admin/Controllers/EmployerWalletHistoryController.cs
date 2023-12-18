using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("employer-wallet-history")]
    [Authorize]
    public class EmployerWalletHistoryController : BaseController
    {
        private readonly ILogger<EmployerWalletHistoryController> _logger;
        public EmployerWalletHistoryController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmployerWalletHistoryController>();

        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

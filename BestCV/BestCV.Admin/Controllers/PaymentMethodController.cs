using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("payment-method")]
    [Authorize]
    public class PaymentMethodController : BaseController
	{
		private readonly ILogger<PaymentMethodController> _logger;
		public PaymentMethodController(ILogger<PaymentMethodController> logger)
		{
			_logger = logger;
		}
		[Route("list")]
		public IActionResult AdminList()
		{
			return View();
		}
	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("admin-account")]
	[Authorize]
	public class AdminAccountController : BaseController
	{
		[Route("list")]
		public IActionResult List()
		{
			return View();
		}
	}
}

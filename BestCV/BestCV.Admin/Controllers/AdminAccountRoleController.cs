using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("admin-account-role")]
    [Authorize]
    public class AdminAccountRoleController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("benefit")]
    [Authorize]
    public class BenefitController : BaseController
    {
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

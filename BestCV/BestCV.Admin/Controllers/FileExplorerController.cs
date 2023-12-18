using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("file-explorer")]
    [Authorize]
    public class FileExplorerController : BaseController
    {
        [Route("demo")]
        public IActionResult Demo()
        {
            return View();
        }
    }
}

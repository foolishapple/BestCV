using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("candidate-CV-PDF-type")]
    [Authorize]
    public class CandidateCVPDFTypesController : BaseController
    {
        private readonly ILogger<CandidateCVPDFTypesController> _logger;

        public CandidateCVPDFTypesController(ILogger<CandidateCVPDFTypesController> logger)
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

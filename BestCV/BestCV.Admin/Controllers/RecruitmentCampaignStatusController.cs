using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
    [Route("recruitment-campaign-status")]
    [Authorize]
    public class RecruitmentCampaignStatusController : BaseController
    {
        private readonly ILogger<RecruitmentCampaignStatusController> _logger;

        public RecruitmentCampaignStatusController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RecruitmentCampaignStatusController>();
        }
        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }
    }
}

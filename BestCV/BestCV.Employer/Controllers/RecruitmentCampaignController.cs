using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Employer.Controllers
{
    [Route("recruitment-campaign")]
    public class RecruitmentCampaignController : BaseController
    {
        private readonly IRecruitmentCampaignService _service;
        private readonly ILogger _logger;
        public RecruitmentCampaignController(IRecruitmentCampaignService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<RecruitmentCampaignController>();
        }

        [Route("dashboard/{id}")]
        public async Task<IActionResult> Dashboard(long id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                if (response.IsSucceeded)
                {
                    ViewBag.RecruitmentCampaignId = id;
                    return View();
                }
                return StatusCode(404);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get recruitment campaign dashboard page");
                return StatusCode(404);
            }
        }
    }
}

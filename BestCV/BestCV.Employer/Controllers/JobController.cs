using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.Employer.Controllers
{
    [Route("tin-tuyen-dung")]
    public class JobController :  BaseController
    {
        private readonly ILogger _logger;
        private IJobService _service;

        public JobController(IJobService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<JobController>();
        }

        [Route("dashboard/{id}")]
        public IActionResult Dashboard(long id)
        {
            ViewBag.JobId = id;
            return View();
        }
        [Route("dang-tin")]
        public IActionResult Post()
        {
            return View();
        }
        [Route("so-sanh-cv/{candidateApplyJobId}/{jobId}")]
        public IActionResult CompareCandidate(long candidateApplyJobId, long jobId)
        {
            ViewBag.JobId = jobId;
            ViewBag.CandidateApplyJobId = candidateApplyJobId;
            return View();
        }
    }
}

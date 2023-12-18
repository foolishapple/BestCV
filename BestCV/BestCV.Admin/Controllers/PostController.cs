using BestCV.Application.Models.Post;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.Admin.Controllers
{
	[Route("post")]
    [Authorize]
    public class PostController : BaseController
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService postService;

        public PostController(ILoggerFactory loggerFactory, IPostService postService)
        {
            _logger = loggerFactory.CreateLogger<PostController>();
			this.postService = postService;

		}

        [Route("list")]
        public IActionResult AdminList()
        {
            return View();
        }


        [HttpGet("add")]
		[HttpGet("update/{id}")]
		public async Task<IActionResult> Attach(int id)
		{
			try
			{
				if (id != 0)
				{
					var post = await postService.GetByIdAsync(id);
					if (post == null)
					{
						return RedirectToAction("Error404", "Home");
					}
				}
				return View(new PostDTO()
				{
					Id = id
				});
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to get attach post view");
				return BadRequest();
			}
		}
	}
}

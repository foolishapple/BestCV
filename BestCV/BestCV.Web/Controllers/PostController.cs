using BestCV.Application.Services.Interfaces;
using BestCV.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace BestCV.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService postService;
        private readonly IConfiguration _configuration;
        public PostController(ILogger<PostController> logger, IConfiguration configuration, IPostService _postService)
        {
            _logger = logger;
            postService = _postService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("danh-sach-bai-viet")]
        [Route("danh-muc/{slug}")]
        [Route("tags/{slug}")]
        public async Task<IActionResult> Post(string keyword, string slug, int? tagId, int? postCategoryId, string postCategoryName)
        {
            var categoryStrId = 0;
            ViewBag.TagId = tagId != null ? tagId : 0;
            var tagStrId = 0;
            var route = Request.Path.Value;
            if (route != null)
            {
                var rootRoute = route.Split('/')[1];
                if (rootRoute.Equals("danh-muc"))
                {
                    var categoryArr = slug.Split('-');
                    categoryStrId = Convert.ToInt32(categoryArr[categoryArr.Length - 1]);
                    var PostCategoryName = await postService.GetCategoryAsync(categoryStrId);
                    if (PostCategoryName != null)
                    {
                        ViewBag.PostCategoryName = PostCategoryName.Name;
                    }
                }
                else if (rootRoute.Equals("tags"))
                {
                    var tagArr = slug.Split('-');
                    tagStrId = Convert.ToInt32(tagArr[tagArr.Length - 1]);
                    var TagName = await postService.GetTagAsync(tagStrId);
                    if (TagName != null)
                    {
                        ViewBag.TagName = TagName.Name;
                        ViewBag.TagId = TagName.Id;

                    }
                }
            }

            ViewBag.Keyword = keyword != null ? keyword : "";
            ViewBag.PostCategoryId = postCategoryId != null ? postCategoryId : categoryStrId;
            
            return View();

            //var postCategoryArr = slugCategoryId.Split('-');
            //var categoryId = Convert.ToInt32(postCategoryArr[postCategoryArr.Length - 1]);
            //var post = postService.GetByIdAsync(categoryId);
            //if (post == null)
            //{
            //    return View("NotFoundFEPage");
            //}
            //if (slugCategory != null)
            //{
            //    var slugCategoryArr = slugCategory.Split('-');
            //    var slugTagsArr = slugTags.Split("-");

            //    if (slugCategoryArr.Length > 0)
            //    {
            //        var postId = Convert.ToInt32(slugCategoryArr[slugCategoryArr.Length - 1]);
            //        var post = postService.GetByIdAsync(postId);
            //        if (post != null)
            //        {
            //            return View();
            //        }
            //    }
            //}

        }

        [Route("bai-viet")]
        public IActionResult PostDetail()
        {
            return View();
        }

        [Route("bai-viet/{slug}")]
        public async Task<IActionResult> DetailPostOnHomePage(string slug)
        {
            var postArr = slug.Split('-');
            var postId = Convert.ToInt32(postArr[postArr.Length - 1]);
            var post = await postService.GetByIdAsync(postId);
            if (post == null)
            {
                return View("NotFoundFEPage");
            }

            return View("PostDetail", post.Resources);
        }
    }
}

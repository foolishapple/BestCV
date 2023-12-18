
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace BestCV.API.Controllers
{
    public class BaseController : Controller
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            if (!ModelState.IsValid)//valid model state
            {
                var errors = ModelState.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage);
                filterContext.Result = Ok(BestCVResponse.BadRequest(errors));
            }

            await base.OnActionExecutionAsync(filterContext, next);

           
        }
    }
}

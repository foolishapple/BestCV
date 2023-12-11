using BestCV.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BestCV.Web.Controllers
{
    public static class ControllerHelper
    {
        /// <summary>
        /// Return logger in user info
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLoggedInUserInfo(this ControllerBase controller, string key)
        {
            try
            {
                if (controller.HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    return identity.FindFirst(key)?.Value;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static long GetLoggedInUserId(this ControllerBase controller)
        {
            return Convert.ToInt64(GetLoggedInUserInfo(controller, ClaimNames.ID));
        }
    }
}

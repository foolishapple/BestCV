using BestCV.Domain.Constants;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BestCV.Application.Utilities.SignalRs.Extensions
{
    public static class HubExtension
    {
        public static string GetLoggedInUserInfo(this Hub hub, string key)
        {
            try
            {
                if (hub.Context?.User?.Identity is ClaimsIdentity identity)
                {
                    return identity.FindFirst(key)?.Value;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static long GetLoggedInUserId(this Hub hub)
        {
            return Convert.ToInt64(GetLoggedInUserInfo(hub, ClaimNames.ID));
        }
    }
}

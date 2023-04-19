using Microsoft.AspNetCore.Http;
using Server.Common;
using System.Security.Claims;

namespace Server.Application
{
    public class SessionData : ISessionData
    {
        public SessionData(IHttpContextAccessor accessor)
        {
            if (accessor.HttpContext == null || accessor.HttpContext.User == null)
            {
                return;
            }

            UserId = int.Parse(accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Username = accessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }

        public int UserId { get; }

        public string Username { get; }
    }
}

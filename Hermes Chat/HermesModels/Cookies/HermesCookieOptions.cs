using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HermesModels.Cookies
{
    public class HermesCookieOptions
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        public AuthenticationProperties AuthenticationProperties { get; set; }
    }
}
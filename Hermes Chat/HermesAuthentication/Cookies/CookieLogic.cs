using AuthenAuthorAccount.Interfaces;
using HermesModels.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenAuthorAccount.Cookies
{
    public class CookieLogic : ICookieLogic 
    {
        public HermesCookieOptions GetCookieOptions(Dictionary<string, string> valuePairs)
        {
            var claims = new List<Claim>();

            foreach (var pair in valuePairs)
            {
                claims.Add(new Claim(pair.Key, pair.Value));
            }

            return CraftCookie(claims);
        }

        public HermesCookieOptions GetDefaultCookieOptions(string username)
        {
            var claims = new List<Claim>()
            {
                new Claim("Username", username),
            };

            return CraftCookie(claims);
        }

        private HermesCookieOptions CraftCookie(List<Claim> claims)
        {
            return new HermesCookieOptions()
            {
                ClaimsPrincipal = new ClaimsPrincipal(GetClaimsIdentity(claims)),
                AuthenticationProperties = GetAuthenticationProperties(),
            };
        }

        private ClaimsIdentity GetClaimsIdentity(List<Claim> claims)
        {
            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        private AuthenticationProperties GetAuthenticationProperties()
        {
            return new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = false,
            };
        }
    }
}
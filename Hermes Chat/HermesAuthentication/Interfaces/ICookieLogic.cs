using HermesModels.Cookies;
using System.Collections.Generic;

namespace AuthenAuthorAccount.Interfaces
{
    public interface ICookieLogic
    {
        HermesCookieOptions GetCookieOptions(Dictionary<string, string> valuePairs);

        HermesCookieOptions GetDefaultCookieOptions(string username);
    }
}
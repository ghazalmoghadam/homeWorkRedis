using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Presentation.WebApi.Filters
{
    public class WriteAuthorizeFilterAttribute : Attribute, IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string SessionIdValue = null;
            context.HttpContext.Request.Cookies.TryGetValue("SessionIdKey",out SessionIdValue);

            using (IRedisClient client = new RedisClient())
            {
                var cacheData = client.Get<List<Dictionary<string, string>>>($"{SessionIdValue}");
                if (cacheData == null)
                    context.Result = new StatusCodeResult(401); // 302: redirect to login page

                else
                {
                    string writeClaimValue = null;
                    foreach (var claim in cacheData)
                    {
                        if (claim.ContainsKey("CanWrite"))
                            writeClaimValue = claim["CanWrite"];
                    }

                    if (writeClaimValue == "false")
                        context.Result = new StatusCodeResult(403);
                }
                
            }
        }
    }
}

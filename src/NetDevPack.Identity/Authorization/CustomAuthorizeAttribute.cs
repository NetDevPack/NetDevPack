using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace NetDevPack.Identity.Authorization
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequerimentClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
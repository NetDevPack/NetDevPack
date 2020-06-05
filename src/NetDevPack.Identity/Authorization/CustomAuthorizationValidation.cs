using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NetDevPack.Identity.Authorization
{
    public class CustomAuthorizationValidation
    {
        public static bool UserHasValidClaim(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(',').Contains(claimValue));
        }

    }
}
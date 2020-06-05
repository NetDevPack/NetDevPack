using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;

namespace AspNetCore.Jwt.Sample.Controllers
{
    [Authorize]
    [Route("api/sample")]
    public class SampleController : MainController
    {
        [HttpGet("read")]
        [CustomAuthorize("Sample","Read")]
        public IActionResult SampleActionRead()
        {
            return CustomResponse("You have permission to get this!");
        }

        [HttpGet("list")]
        [CustomAuthorize("Sample", "List")]
        public IActionResult SampleActionList()
        {
            return CustomResponse("You have permission to get this!");
        }
    }
}
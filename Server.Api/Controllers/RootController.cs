using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Api.Controllers
{
    [AllowAnonymous]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Server is running.");
        }
    }
}

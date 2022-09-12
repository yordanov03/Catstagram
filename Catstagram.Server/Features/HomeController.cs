using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{
    public class HomeController : Features
    {
        [Authorize]
        public ActionResult Get()
        {
            return Ok("works");
        }
    }
}

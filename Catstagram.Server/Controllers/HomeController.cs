using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {   
        public IActionResult Get()
        {
            return Ok("works");
        }
    }
}

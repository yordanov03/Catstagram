using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public abstract class ApiController : ControllerBase
    {
    }
}

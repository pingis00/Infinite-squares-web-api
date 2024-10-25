using Microsoft.AspNetCore.Mvc;

namespace InfiniteSquaresWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SquaresController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}

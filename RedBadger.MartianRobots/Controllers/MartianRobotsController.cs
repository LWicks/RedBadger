using Microsoft.AspNetCore.Mvc;

namespace RedBadger.MartianRobots.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MartianRobotsController : ControllerBase
{
    // POST api/martianrobots
    [HttpPost]
    public ActionResult<string> SimulateMartianRobotMovement()
    {
        string result = "OK";
        return Ok(result);
    }
}
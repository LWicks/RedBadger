using Microsoft.AspNetCore.Mvc;
using RedBadger.MartianRobots.Interfaces;
using RedBadger.MartianRobots.Models;

namespace RedBadger.MartianRobots.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MartianRobotsController : ControllerBase
{
    private readonly IMartianRobotsService _marsRoverService;

    public MartianRobotsController(IMartianRobotsService marsRoverService)
    {
        _marsRoverService = marsRoverService;
    }

    // POST api/martianrobots
    [HttpPost]
    public ActionResult<string> SimulateRoverMovement([FromBody] MartianRobotsRequest request)
    {
        if (string.IsNullOrEmpty(request.GridDimensions) || string.IsNullOrEmpty(request.InitialPosition) || string.IsNullOrEmpty(request.Instructions))
        {
            return BadRequest("Invalid input data.");
        }

        string result = _marsRoverService.SimulateRoverMovement(request.GridDimensions, request.InitialPosition, request.Instructions);
        return Ok(result);
    }
}
namespace RedBadger.MartianRobots.Interfaces;

public interface IMartianRobotsService
{
    string SimulateRoverMovement(string gridDimensions, string initialPosition, string instructions);
}
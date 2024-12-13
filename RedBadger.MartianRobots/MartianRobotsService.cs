using RedBadger.MartianRobots.Interfaces;

namespace RedBadger.MartianRobots;

public class MartianRobotsService : IMartianRobotsService
{
    public const string DirectionsOrder = "NESW"; // Clockwise order

    private readonly Dictionary<char, (int dx, int dy)> _directions = new()
    {
        { 'N', (0, 1) },  // North
        { 'E', (1, 0) },  // East
        { 'S', (0, -1) }, // South
        { 'W', (-1, 0) }  // West
    };

    public string SimulateRoverMovement(string gridDimensions, string initialPosition, string instructions)
    {
        // Parse grid dimensions
        string[] gridParts = gridDimensions.Split(' ');
        int gridWidth = int.Parse(gridParts[0]);
        int gridHeight = int.Parse(gridParts[1]);

        // Parse initial robot position and orientation
        string[] positionParts = initialPosition.Split(' ');
        int x = int.Parse(positionParts[0]);
        int y = int.Parse(positionParts[1]);
        char orientation = char.Parse(positionParts[2]);

        HashSet<string> lostPositions = []; // To track scent
        bool isLost = false;

        foreach (char command in instructions) // Commands to be considered for future via configuration entry?
        {
            if (command is 'L' or 'R')
            {
                // Turn the robot
                orientation = Turn(orientation, command);
            }
            else if (command == 'F')
            {
                // Move the robot
                int newX = x + _directions[orientation].dx;
                int newY = y + _directions[orientation].dy;

                // Check if the robot moves off the grid
                if (newX < 0 || newX > gridWidth || newY < 0 || newY > gridHeight)
                {
                    // Check if the robot has been lost at this position before
                    string currentPosition = $"{x} {y} {orientation}";
                    if (!lostPositions.Add(currentPosition))
                    {
                        // Ignore the move if the scent exists
                        continue;
                    }

                    // Robot gets lost, record the scent
                    isLost = true;
                    break;
                }

                // Update robot's position
                x = newX;
                y = newY;
            }
        }

        // Return the final position and orientation
        return isLost ? $"{x} {y} {orientation} LOST" : $"{x} {y} {orientation}";
    }

    // Method to turn the robot based on the current orientation and command (L or R)
    public char Turn(char currentOrientation, char command)
    {
        int index = DirectionsOrder.IndexOf(currentOrientation);

        index = command switch
        {
            'L' => (index + 3) % 4,
            'R' => (index + 1) % 4,
            _ => index
        };

        return DirectionsOrder[index];
    }
}
namespace RedBadger.MartianRobots.Tests
{
    public class MartianRobotsServiceTests
    {
        private readonly MartianRobotsService _marsRoverService;

        public MartianRobotsServiceTests()
        {
            _marsRoverService = new MartianRobotsService();
        }

        [Theory]
        [InlineData("5 3", "1 1 E", "RFRFRFRF", "1 1 E")]
        [InlineData("5 3", "3 2 N", "FRRFLLFFRRFLL", "3 3 N LOST")]
        [InlineData("5 3", "0 3 W", "LLFFFLFLFL", "3 3 N LOST")]
        public void SimulateRoverMovement_ShouldReturnCorrectFinalPosition(string gridDimensions, string initialPosition, string instructions, string expected)
        {
            // Act
            string result = _marsRoverService.SimulateRoverMovement(gridDimensions, initialPosition, instructions);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData('N', 'L', 'W')]
        [InlineData('E', 'L', 'N')]
        [InlineData('S', 'L', 'E')]
        [InlineData('W', 'L', 'S')]
        [InlineData('N', 'R', 'E')]
        [InlineData('E', 'R', 'S')]
        [InlineData('S', 'R', 'W')]
        [InlineData('W', 'R', 'N')]
        public void Turn_ShouldReturnCorrectOrientation(char currentOrientation, char command, char expectedOrientation)
        {
            // Act
            char result = _marsRoverService.Turn(currentOrientation, command);

            // Assert
            Assert.Equal(expectedOrientation, result);
        }
    }
}
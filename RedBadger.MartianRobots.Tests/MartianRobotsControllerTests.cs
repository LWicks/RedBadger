using Microsoft.AspNetCore.Mvc;
using Moq;
using RedBadger.MartianRobots.Controllers;
using RedBadger.MartianRobots.Interfaces;
using RedBadger.MartianRobots.Models;

namespace RedBadger.MartianRobots.Tests
{
    public class MartianRobotsControllerTests
    {
        private readonly Mock<IMartianRobotsService> _mockMarsRoverService;
        private readonly MartianRobotsController _controller;

        public MartianRobotsControllerTests()
        {
            _mockMarsRoverService = new Mock<IMartianRobotsService>();
            _controller = new MartianRobotsController(_mockMarsRoverService.Object);
        }

        [Theory]
        [InlineData("5 3", "1 1 E", "RFRFRFRF", "1 1 E")]
        [InlineData("5 3", "3 2 N", "FRRFLLFFRRFLL", "3 3 N LOST")]
        [InlineData("5 3", "0 3 W", "LLFFFLFLFL", "2 3 S")]
        public void SimulateRoverMovement_ShouldReturnOkResult(string gridDimensions, string initialPosition, string instructions, string expected)
        {
            // Arrange
            _mockMarsRoverService
                .Setup(service => service.SimulateRoverMovement(gridDimensions, initialPosition, instructions))
                .Returns(expected);

            var request = new MartianRobotsRequest
            {
                GridDimensions = gridDimensions,
                InitialPosition = initialPosition,
                Instructions = instructions
            };

            // Act
            var result = _controller.SimulateRoverMovement(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public void SimulateRoverMovement_ShouldReturnBadRequest_WhenInputIsInvalid()
        {
            // Arrange
            var request = new MartianRobotsRequest
            {
                GridDimensions = "",
                InitialPosition = "",
                Instructions = ""
            };

            // Act
            var result = _controller.SimulateRoverMovement(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid input data.", badRequestResult.Value);
        }
    }
}

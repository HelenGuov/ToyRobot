using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using ToyRobot.Models;
using ToyRobot.Services;

namespace ToyRobot.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(0,0)]
    [TestCase(0,5)]
    [TestCase(5,0)]
    public void GivenValidLocation_WhenPlaceRobot_ThenReturnPlacedRobot(int x, int y) 
    {
        var tableTop = new TableTop()
        {
            Width = 5,
            Height = 5
        };

        var robotService = new RobotService(tableTop);
        var actualRobot = robotService.PlaceRobot(x, y, Direction.North); 
        actualRobot.IsPlaced.Should().Be(true);
        actualRobot.XLocation.Should().Be(x);
        actualRobot.YLocation.Should().Be(y);
        actualRobot.Direction.Should().Be(Direction.North);
    }
    
    [TestCase(-1,0)]
    [TestCase(0,-1)]
    [TestCase(-1,-1)]
    [TestCase(6,0)]
    [TestCase(0,6)]
    public void GivenInvalidValidLocation_WhenPlaceRobot_ThenReturnPlacedRobot(int x, int y) 
    {
        var tableTop = new TableTop()
        {
            Width = 5,
            Height = 5
        };

        var robotService = new RobotService(tableTop);
        var actualRobot = robotService.PlaceRobot(x, y, Direction.West); 
        actualRobot.IsPlaced.Should().Be(false);
        actualRobot.XLocation.Should().Be(0);
        actualRobot.YLocation.Should().Be(0);
        actualRobot.Direction.Should().Be(Direction.North);
    }

    [TestCase(0,0, "North", 0,1,"North")]
    [TestCase(0,0, "East", 1,0,"East")]
    [TestCase(0,1, "South", 0,0,"South")]
    [TestCase(1,0, "West", 0,0,"West")]
    public void GivenValidMove_WhenMoveRobot_ThenReturnValidLocationsOnTableTop(
        int inputXLocation, int inputYLocation, string inputDirection, 
        int expectedXLocation, int expectedYLocation, string expectedDirection)
    {
        var inDirection = Enum.Parse<Direction>(inputDirection);
        var outDirection = Enum.Parse<Direction>(expectedDirection);
        
        var tableTop = new TableTop
        {
            Width = 5,
            Height = 5
        };

        var robot = new Robot()
        {
            XLocation = inputXLocation,
            YLocation = inputYLocation,
            Direction = inDirection,
            IsPlaced = true
        }; 
        
        var robotService = new RobotService(tableTop);
        var actualRobot = robotService.MoveRobot(robot);

        actualRobot.XLocation.Should().Be(expectedXLocation);
        actualRobot.YLocation.Should().Be(expectedYLocation);
        actualRobot.Direction.Should().Be(outDirection);
    }
    
    [TestCase(0,0, "West", 0,0, "West")]
    [TestCase(0,0, "South", 0,0,"South")]
    [TestCase(0,5, "North", 0,5,"North")]
    [TestCase(5,0, "East", 5,0,"East")]
    public void GivenInValidMove_WhenMoveRobot_ThenIgnoreNextLocationOutsideOfTableTop(
        int inputXLocation, int inputYLocation, string inputDirection, 
        int expectedXLocation, int expectedYLocation, string expectedDirection)
    {
        var inDirection = Enum.Parse<Direction>(inputDirection);
        var outDirection = Enum.Parse<Direction>(expectedDirection);
        
        var tableTop = new TableTop
        {
            Width = 5,
            Height = 5
        };

        var robot = new Robot()
        {
            XLocation = inputXLocation,
            YLocation = inputYLocation,
            Direction = inDirection,
            IsPlaced = true
        }; 
        
        var robotService = new RobotService(tableTop);
        var actualRobot = robotService.MoveRobot(robot);

        actualRobot.XLocation.Should().Be(expectedXLocation);
        actualRobot.YLocation.Should().Be(expectedYLocation);
        actualRobot.Direction.Should().Be(outDirection);
    }
    
    [Test]
    public void GivenNotPlacedRobot_WhenMoveRobot_ThenReturnDefaultRobotWithoutMovement()
    {
        var tableTop = new TableTop
        {
            Width = 5,
            Height = 5
        };

        var robot = new Robot()
        {
            XLocation = 1,
            YLocation = 1,
            Direction = Direction.East,
            IsPlaced = false
        }; 
        
        var robotService = new RobotService(tableTop);
        var actualRobot = robotService.MoveRobot(robot);
        actualRobot.IsPlaced.Should().Be(false);
        actualRobot.XLocation.Should().Be(0);
        actualRobot.YLocation.Should().Be(0);
        actualRobot.Direction.Should().Be(Direction.North);
    }

    [TestCase("North", "Left", "West")]
    [TestCase("North", "Right", "East")]
    [TestCase("East", "Left", "North")]
    [TestCase("East", "Right", "South")]
    [TestCase("South", "Left", "East")]
    [TestCase("South", "Right", "West")]
    [TestCase("West", "Left", "South")]
    [TestCase("West", "Right", "North")]
    public void GivenPlacedRobot_WhenTurnRobot_ReturnsRobotInNewDirection(string currentDirection, string rotation, string expectedDirection)
    {
        var inDirection = Enum.Parse<Direction>(currentDirection);
        var outDirection = Enum.Parse<Direction>(expectedDirection);
        var inRotation = Enum.Parse<Rotation>(rotation);
        
        var tableTop = new TableTop()
        {
            Width = 5,
            Height = 5
        }; 
        
        var robot = new Robot()
        {
            XLocation = 0,
            YLocation = 0,
            Direction = inDirection,
            IsPlaced = true
        };

        var robotService = new RobotService(tableTop); 
        var actualRobot = robotService.TurnRobot(robot, inRotation);
        actualRobot.Direction.Should().Be(outDirection);
    }
}
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
            Direction = inDirection
        }; 
        
        var robotService = new RobotService(tableTop);
        var actualRobot = robotService.MoveRobot(robot);

        actualRobot.XLocation.Should().Be(expectedXLocation);
        actualRobot.YLocation.Should().Be(expectedYLocation);
        actualRobot.Direction.Should().Be(outDirection);
    }
    
}
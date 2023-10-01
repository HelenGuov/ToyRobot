using ToyRobot.Models;

namespace ToyRobot.Services;

public class RobotService
{
    private readonly TableTop _tableTop;
    private readonly Dictionary<Direction, int> _robotMovements;

    public RobotService(TableTop tableTop)
    {
        _tableTop = tableTop;
        _robotMovements = new Dictionary<Direction, int>()
        {
            {Direction.North, 1},
            {Direction.East, 1},
            {Direction.South, -1},
            {Direction.West, -1}
        };
    }

    public Robot PlaceRobot(int x, int y, Direction direction)
    {
        return new Robot()
        {
            XLocation = x,
            YLocation = y,
            Direction = direction
        };
    }

    public Robot MoveRobot(Robot robot)
    {
        if (_robotMovements.ContainsKey(robot.Direction))
        {
            if (robot.Direction == Direction.North || robot.Direction == Direction.South)
            {
                var newYLocation = CalculateNextLocation(robot.YLocation, _tableTop.Height, robot.Direction);
                return robot with {YLocation = newYLocation};
            }

            if (robot.Direction == Direction.East || robot.Direction == Direction.West)
            {
                var newXLocation = CalculateNextLocation(robot.XLocation, _tableTop.Width, robot.Direction);
                return robot with {XLocation = newXLocation};
            }

        }

        Console.WriteLine("Invalid direction"); 
        return new Robot(); 
    }
    
    private int CalculateNextLocation(int currrentLocation, int maxLocation, Direction direction)
    {
        var newLocation = currrentLocation + _robotMovements[direction];
        if (newLocation >= 0 && newLocation <= maxLocation)
            return newLocation;
        return currrentLocation; 
    }
}
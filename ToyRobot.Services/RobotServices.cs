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
                return robot with {YLocation = robot.YLocation + _robotMovements[robot.Direction]};

            return robot with {XLocation = robot.XLocation + _robotMovements[robot.Direction] };
        }

        Console.WriteLine("Invalid direction"); 
        return new Robot(); 
    }
}
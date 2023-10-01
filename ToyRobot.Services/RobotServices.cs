using ToyRobot.Models;

namespace ToyRobot.Services;

public class RobotService
{
    private readonly TableTop _tableTop;
    private readonly Dictionary<Direction, int> _robotMovements;
    private readonly Dictionary<Direction, DirectionNeighbour> _directionNeighbour;

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

        _directionNeighbour = new Dictionary<Direction, DirectionNeighbour>()
        {
            {Direction.North, new DirectionNeighbour() {Direction = Direction.North, LeftNeighbour = Direction.West, RightNeighbour = Direction.East}},
            {Direction.East, new DirectionNeighbour() {Direction = Direction.East, LeftNeighbour = Direction.North, RightNeighbour = Direction.South}},
            {Direction.South, new DirectionNeighbour() {Direction = Direction.South, LeftNeighbour = Direction.East, RightNeighbour = Direction.West}},
            {Direction.West, new DirectionNeighbour() {Direction = Direction.West, LeftNeighbour = Direction.South, RightNeighbour = Direction.North}}
        }; 
    }

    public Robot PlaceRobot(int x, int y, Direction direction)
    {
        var isValidXLocation = IsValidLocation(x, _tableTop.Width);
        var isValidYLocation = IsValidLocation(y, _tableTop.Height);

        if (isValidXLocation && isValidYLocation)
        {
            return new Robot()
            {
                XLocation = x,
                YLocation = y,
                Direction = direction, 
                IsPlaced = true
            };
        }

        return new Robot(); 
    }

    public Robot TurnRobot(Robot robot, Rotation rotation)
    {
        if (!robot.IsPlaced) return robot; 

        var directionNeighbour = _directionNeighbour[robot.Direction];
        if (rotation == Rotation.Left)
            return robot with {Direction = directionNeighbour.LeftNeighbour};
        
        return robot with {Direction = directionNeighbour.RightNeighbour};
    }

    public Robot MoveRobot(Robot robot)
    {
        if (_robotMovements.ContainsKey(robot.Direction) && robot.IsPlaced)
        {
            if (robot.Direction is Direction.North or Direction.South)
            {
                var newYLocation = CalculateNextLocation(robot.YLocation, _tableTop.Height, robot.Direction);
                return robot with {YLocation = newYLocation};
            }
            
            if (robot.Direction is Direction.East or Direction.West)
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
        if (IsValidLocation(newLocation, maxLocation))
            return newLocation;
        return currrentLocation; 
    }

    private bool IsValidLocation(int location, int maxLocation) => (location >= 0 && location <= maxLocation); 
}
namespace ToyRobot.Models;

public enum Direction
{
    North,
    East,
    South,
    West
}

public enum Rotation
{
    Left, 
    Right
}

public record DirectionNeighbour {
    public Direction Direction { get; init; }
    public Direction LeftNeighbour { get; init; }
    public Direction RightNeighbour { get; init; }
}
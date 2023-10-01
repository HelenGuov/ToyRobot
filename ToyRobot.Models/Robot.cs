namespace ToyRobot.Models;

public record Robot
{
    public int XLocation { get; init; }
    public int YLocation { get; init; }
    public Direction Direction { get; init; }
    public bool IsPlaced { get; init; }
}


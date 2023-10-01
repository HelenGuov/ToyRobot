// See https://aka.ms/new-console-template for more information

using System.Globalization;
using ToyRobot.Models;
using ToyRobot.Services;

internal class Program
{
    //Assuming the input file is in the correct format
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter full path to input file: ");
        string? filePath = Console.ReadLine();

        if (filePath is null)
        {
            Console.WriteLine("Invalid file path");
            return;
        }

        using StreamReader reader = new StreamReader(filePath);
        string? line;
        Robot robot = new Robot();
        var tableTop = new TableTop()
        {
            Width = 5, Height = 5 
        }; 
        var robotService = new RobotService(tableTop);

        Console.WriteLine($"Table top size. Height: {tableTop.Height}, Width: {tableTop.Width}");
        while ((line = reader.ReadLine()) != null)
        {
            var command = line.Split(" ");
            var commandType = command[0];

            if (commandType == "PLACE")
            {
                var placeInputs = command[1].Split(",");
                var xLocation = int.Parse(placeInputs[0]);
                var yLocation = int.Parse(placeInputs[1]);
                var directionText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(placeInputs[2].ToLower());
                var direction = Enum.Parse<Direction>(directionText);

                robot = robotService.PlaceRobot(xLocation, yLocation, direction);
            }
            else if (commandType == "MOVE")
            {
                robot = robotService.MoveRobot(robot);
            }
            else if (commandType is "LEFT" or "RIGHT")
            {
                var turnText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(commandType.ToLower());
                var rotation = Enum.Parse<Rotation>(turnText);
                robot = robotService.TurnRobot(robot, rotation);
            }
            
            Console.WriteLine(line);
            if (commandType == "REPORT")
            {
                if (robot.IsPlaced)
                    Console.WriteLine($"=> {robot.XLocation},{robot.YLocation},{robot.Direction.ToString().ToUpper()}");
                else 
                    Console.WriteLine("Ignore command(s): Robot not placed, invalid moves or turns");
                robot = new Robot(); 
            }
        }
    }
}



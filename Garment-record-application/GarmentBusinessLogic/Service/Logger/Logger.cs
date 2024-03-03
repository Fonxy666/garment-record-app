namespace GarmentBusinessLogic.Service.Logger;

public class Logger : ILogger
{
    public void ShowText(string message)
    {
        Console.WriteLine(message);
    }

    public void OneLine(string message)
    {
        Console.Write(message);
    }

    public void ErrorLog(string message)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }

    public string Input()
    {
        var title = Console.ReadLine() ?? "";
        return title;
    }
}
namespace GarmentBusinessLogic.Service.Logger;

public interface ILogger
{
    public void ShowText(string message);
    public void OneLine(string message);
    public void ErrorLog(string message);
    public string Input();
}
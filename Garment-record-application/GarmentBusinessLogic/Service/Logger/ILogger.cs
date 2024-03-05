namespace GarmentBusinessLogic.Service.Logger;

public interface ILogger
{
    public void ShowText(string message);
    public void ShowTextInputInTheSameRow(string message);
    public void ErrorLog(string message);
    public string Input();
}
using GarmentBusinessLogic.Service;
using GarmentBusinessLogic.Service.Logger;

namespace GarmentBusinessLogic.Ui;

public class GarmentConsoleUi
{
    private readonly ILogger _logger;
    private readonly IGarmentService _garmentService;
    public GarmentConsoleUi(ILogger logger, IGarmentService garmentService)
    {
        _logger = logger;
        _garmentService = garmentService;
    }
    public void Run()
    {
        _logger.ShowText("Welcome to the Garment application.");
        DisplayMenu();

        var inputCode = 0;
        while (inputCode != 6)
        {
            inputCode = GetCode();
            switch (inputCode)
            {
                case 1:
                    DisplayAllGarment();
                    break;
                case 2:
                    Console.WriteLine("search garment.");
                    break;
                case 3:
                    Console.WriteLine("update garment.");
                    break;
                case 4:
                    Console.WriteLine("sort garments.");
                    break;
                case 5:
                    Console.WriteLine("delete");
                    break;
            }
        }
    }

    private void DisplayMenu()
    {
        _logger.ShowText("1 - Display all garment.");
        _logger.ShowText("2 - Search by id.");
        _logger.ShowText("3 - Update a garment record.");
        _logger.ShowText("4 - Sort:");
        _logger.ShowText("5 - Delete a garment.");
        _logger.ShowText("6 - Exit");
    }
    
    private int GetCode()
    {
        return int.Parse(_logger.Input());
    }

    private void DisplayAllGarment()
    {
        var garments = _garmentService.LoadFromFile("GarmentData.json");
        if (garments == null)
        {
            _logger.ErrorLog("There are no garments yet.");
            return;
        }
        
        foreach (var garment in _garmentService.LoadFromFile("GarmentData.json"))
        {
            _logger.ShowText($"Id: {garment.Id}");
            _logger.ShowText($"Brand name: {garment.BrandName}");
            _logger.ShowText($"Color: {garment.Color}");
            _logger.ShowText($"Size: {garment.Size}");
            _logger.ShowText($"Purchase time: {garment.Purchase}");
            _logger.ShowText("------------------------------------------");
        }
    }
}
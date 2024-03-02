using GarmentBusinessLogic.Service;
using GarmentBusinessLogic.Service.Logger;
using GarmentRecordLibrary.Model;
using GarmentRecordLibrary.Model.Enum;

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
        while (inputCode != 7)
        {
            inputCode = GetCode();
            switch (inputCode)
            {
                case 1:
                    DisplayAllGarment();
                    break;
                case 2:
                    AddGarment();
                    break;
                case 3:
                    SearchById();
                    break;
                case 4:
                    UpdateGarment();
                    break;
                case 5:
                    Console.WriteLine("sort garments.");
                    break;
                case 6:
                    DeleteGarment();
                    break;
            }
        }
    }

    private void DisplayMenu()
    {
        _logger.ShowText("1 - Display all garment.");
        _logger.ShowText("2 - Add new garment.");
        _logger.ShowText("3 - Search by id.");
        _logger.ShowText("4 - Update a garment record.");
        _logger.ShowText("5 - Sort:");
        _logger.ShowText("6 - Delete a garment.");
        _logger.ShowText("7 - Exit");
    }
    
    private int GetCode()
    {
        return int.Parse(_logger.Input());
    }

    private void ShowGarment(Garment garment)
    {
        _logger.ShowText("------------------------------------------");
        _logger.ShowText($"Id: {garment.Id}");
        _logger.ShowText($"Brand name: {garment.BrandName}");
        _logger.ShowText($"Color: {garment.Color}");
        _logger.ShowText($"Size: {garment.Size}");
        _logger.ShowText($"Purchase time: {garment.Purchase}");
        _logger.ShowText("------------------------------------------");
    }

    private void DisplayAllGarment()
    {
        var garments = _garmentService.GarmentList;
        if (garments!.Count == 0)
        {
            _logger.ErrorLog("There are no garments yet.");
            return;
        }
        
        foreach (var garment in garments)
        {
            ShowGarment(garment);
        }
    }

    private void AddGarment()
    {
        var newGarment = CreateGarment();
        _garmentService.AddGarment(newGarment);
    }

    private Garment CreateGarment()
    {
        _logger.ShowText("At the end of the lines, give us the details you want to add to the new garment.");
        _logger.OneLine("Brand name: ");
        var newGarment = new Garment
        {
            Id = (uint)_garmentService.GarmentList!.Count + 1
        };
        newGarment.BrandName = _logger.Input();
        _logger.OneLine("Color: ");
        newGarment.Color = _logger.Input();
        
        _logger.ShowText("Size:");
        _logger.OneLine("It can be: 'XS', 'S', 'M', 'L', 'XL', 'XXL', don't forget to use uppercase. The standard value is 'XS'.");
        var garmentSizeInput = _logger.Input();
        
        if (Enum.TryParse(garmentSizeInput, out GarmentSize garmentSize))
        {
            newGarment.Size = garmentSize;
        }
        else
        {
            _logger.ErrorLog("Parse failed. The Size got default value, later on you can update it.");
        }
        
        _logger.OneLine("Purchase date('yyyy-mm-dd'), or if you hit enter, it will save today: ");
        var purchaseDateInput = _logger.Input();
        if (DateTime.TryParse(purchaseDateInput, out var purchaseDate))
        {
            newGarment.Purchase = purchaseDate;
        }
        else
        {
            _logger.ErrorLog("Parse failed. Purchase date is set to today.");
        }

        return newGarment;
    }

    private void SearchById()
    {
        _logger.OneLine("Give us the id: ");
        var input = _logger.Input();
        if (uint.TryParse(input, out var parsedId))
        {
            var garment = _garmentService.SearchGarment(parsedId);
            ShowGarment(garment);
        }
        else
        {
            _logger.ErrorLog("Invalid input. Please enter a valid number.");
        }
    }

    private void UpdateGarment()
    {
        _logger.OneLine("Give us the garment id which you want to update: ");
        var input = _logger.Input();
        if (uint.TryParse(input, out var parsedId))
        {
            var newGarment = CreateGarment();
            newGarment.Id = parsedId;
            _garmentService.UpdateGarment(parsedId, newGarment);
        }
        else
        {
            _logger.ErrorLog("Update failed. There was an error updating the garment.");
        }
    }

    private void DeleteGarment()
    {
        _logger.OneLine("Give us the garment id which you want to delete: ");
        var input = _logger.Input();
        if (uint.TryParse(input, out var parsedId))
        {
            _garmentService.DeleteGarment(parsedId);
        }
        else
        {
            _logger.ErrorLog("Delete failed. There was an error updating the garment.");
        }
    }
}
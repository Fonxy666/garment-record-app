﻿using GarmentBusinessLogic.Service.Logger;
using GarmentRecordLibrary.Model;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace GarmentBusinessLogic.Service;

public class GarmentService : IGarmentService
{
    public IList<Garment>? GarmentList { get; set; }
    private readonly string _jsonPath;
    private readonly string _idJsonPath;
    private readonly ILogger _logger;

    public GarmentService(string jsonPath, ILogger logger)
    {
        _jsonPath = jsonPath;
        _idJsonPath = "GarmentId.json";
        _logger = logger;
        GarmentList = LoadGarmentFromFile(jsonPath);
    }

    private IList<Garment> LoadGarmentFromFile(string path)
    {
        if (!File.Exists(path))
        {
            var emptyGarmentList = new List<Garment>();
            var emptyJson = JsonConvert.SerializeObject(emptyGarmentList);
            File.WriteAllText(path, emptyJson);
            return emptyGarmentList;
        }
        
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<Garment>>(json)!;
    }

    private uint LoadIdFromFile(string path)
    {
        if (!File.Exists(path))
        {
            var freshIdFile = new Id(1);
            var emptyJson = JsonConvert.SerializeObject(freshIdFile);
            File.WriteAllText(path, emptyJson);
            return freshIdFile.NewId;
        }
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<Id>(json)!.NewId;
    }

    public void AddGarment(Garment garment)
    {
        garment.Id = LoadIdFromFile(_idJsonPath);
        GarmentList!.Add(garment);
        try
        {
            SaveToFile(true);
            _logger.ShowText("New garment successfully added.");
        }
        catch (Exception ex)
        {
            _logger.ErrorLog($"Error saving to file: {ex.Message}");
        }
    }

    private void SaveToFile(bool updateId)
    {
        try
        {
            var updatedJson = JsonConvert.SerializeObject(GarmentList, Formatting.Indented);
            File.WriteAllText(_jsonPath, updatedJson);
            if (updateId)
            {
                var updatedId = JsonConvert.SerializeObject(new Id(LoadIdFromFile(_idJsonPath) + 1), Formatting.Indented);
                File.WriteAllText(_idJsonPath, updatedId);
            }
        }
        catch (Exception ex)
        {
            _logger.ErrorLog($"Error saving to file: {ex.Message}");
        }
    }

    public void ResetGarmentListToDefault()
    {
        GarmentList = LoadGarmentFromFile(_jsonPath);
    }

    public void UpdateGarment(uint oldGarmentId, Garment newGarment)
    {
        var existingGarment = GarmentList!.FirstOrDefault(garment => garment.Id == oldGarmentId);
        var index = GarmentList!.IndexOf(existingGarment!);
        if (existingGarment != null)
        {
            GarmentList[index] = newGarment;
            SaveToFile(false);
            _logger.ShowText($"Garment with ID {oldGarmentId} updated successfully.");
        }
        else
        {
            _logger.ErrorLog($"Garment with ID {oldGarmentId} not found.");
        }
    }

    public void DeleteGarment(uint garmentId)
    {
        var existingGarment = GarmentList!.FirstOrDefault(garment => garment.Id == garmentId);
        
        if (existingGarment != null)
        {
            GarmentList!.Remove(existingGarment);
            SaveToFile(false);
            _logger.ShowText($"Garment with ID {garmentId} deleted successfully.");
        }
        else
        {
            _logger.ErrorLog($"Garment with ID {garmentId} not found.");
        }
    }

    public Garment SearchGarment(uint garmentId)
    {
        return GarmentList!.FirstOrDefault(garment => garment.Id == garmentId)!;
    }

    public void SortGarments(string sortType)
    {
        switch (sortType)
        {
            case "id":
                GarmentList = GarmentList!.OrderBy(garment => garment.Id).ToList();
                SaveToFile(false);
                _logger.ShowText("Garments sorted by Id successfully.");
                break;
            
            case "name":
                GarmentList = GarmentList!.OrderBy(garment => garment.BrandName).ToList();
                SaveToFile(false);
                _logger.ShowText("Garments sorted by Brand name successfully.");
                break;
            
            case "color":
                GarmentList = GarmentList!.OrderBy(garment => garment.Color).ToList();
                SaveToFile(false);
                _logger.ShowText("Garments sorted by Color successfully.");
                break;
            
            case "purchase":
                GarmentList = GarmentList!.OrderBy(garment => garment.Purchase).ToList();
                SaveToFile(false);
                _logger.ShowText("Garments sorted by Purchase date successfully.");
                break;
            
            case "size":
                GarmentList = GarmentList!.OrderBy(garment => garment.Size).ToList();
                SaveToFile(false);
                _logger.ShowText("Garments sorted by Size successfully.");
                break;
        }
    }
}
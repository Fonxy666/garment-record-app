using GarmentBusinessLogic.Service.Logger;
using GarmentRecordLibrary.Model;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace GarmentBusinessLogic.Service;

public class GarmentService : IGarmentService
{
    public IList<Garment>? GarmentList { get; set; }
    private readonly string _jsonPath;
    private readonly ILogger _logger;

    public GarmentService(string jsonPath, ILogger logger)
    {
        _jsonPath = jsonPath;
        _logger = logger;
        GarmentList = LoadFromFile(jsonPath);
    }

    private IList<Garment> LoadFromFile(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<Garment>>(json)!;
    }

    public void AddGarment(Garment garment)
    {
        GarmentList!.Add(garment);
        try
        {
            SaveToFile();
            _logger.ShowText("New garment successfully added.");
        }
        catch (Exception ex)
        {
            _logger.ErrorLog($"Error saving to file: {ex.Message}");
        }
    }

    private void SaveToFile()
    {
        try
        {
            var updatedJson = JsonConvert.SerializeObject(GarmentList, Formatting.Indented);
            File.WriteAllText(_jsonPath, updatedJson);
        }
        catch (Exception ex)
        {
            _logger.ErrorLog($"Error saving to file: {ex.Message}");
        }
    }

    public void ResetGarmentListToDefault()
    {
        GarmentList = LoadFromFile(_jsonPath);
    }

    public void UpdateGarment(uint oldGarmentId, Garment newGarment)
    {
        var existingGarment = GarmentList!.FirstOrDefault(garment => garment.Id == oldGarmentId);
        var index = GarmentList!.IndexOf(existingGarment!);
        if (existingGarment != null)
        {
            GarmentList[index] = newGarment;
            SaveToFile();
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
            SaveToFile();
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

    public void SortGarments()
    {
        throw new NotImplementedException();
    }
}
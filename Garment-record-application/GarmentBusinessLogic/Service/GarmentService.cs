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

    public IList<Garment> LoadFromFile(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<Garment>>(json)!;
    }

    public bool AddGarment(Garment garment)
    {
        GarmentList!.Add(garment);
        try
        {
            SaveToFile();
            _logger.ShowText("New garment successfully added.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.ErrorLog($"Error saving to file: {ex.Message}");
            return false;
        }
    }

    public bool SaveToFile()
    {
        try
        {
            var updatedJson = JsonConvert.SerializeObject(GarmentList, Formatting.Indented);
            File.WriteAllText(_jsonPath, updatedJson);
            return true;
        }
        catch (Exception ex)
        {
            _logger.ErrorLog($"Error saving to file: {ex.Message}");
            return false;
        }
    }

    public void ResetGarmentListToDefault()
    {
        GarmentList = LoadFromFile(_jsonPath);
    }

    public bool UpdateGarment(uint oldGarmentId, Garment newGarment)
    {
        throw new NotImplementedException();
    }

    public bool DeleteGarment(uint garmentId)
    {
        throw new NotImplementedException();
    }

    public Garment SearchGarment(uint garmentId)
    {
        return GarmentList!.FirstOrDefault(garment => garment.Id == garmentId)!;
    }

    public bool SortGarments()
    {
        throw new NotImplementedException();
    }
}
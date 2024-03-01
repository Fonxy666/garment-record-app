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
    }

    public IList<Garment> LoadFromFile(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<Garment>>(json)!;
    }

    public bool AddGarment(Garment garment)
    {
        throw new NotImplementedException();
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

    public bool UpdateGarment(string oldGarmentId, Garment newGarment)
    {
        throw new NotImplementedException();
    }

    public bool DeleteGarment(string garmentId)
    {
        throw new NotImplementedException();
    }

    public bool SearchGarment(string garmentId)
    {
        throw new NotImplementedException();
    }

    public bool SortGarments()
    {
        throw new NotImplementedException();
    }
}
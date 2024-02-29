using System.Text.Json;
using System.Text.Json.Serialization;
using Garment_record_application.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Garment_record_application.ViewModel;

public class GarmentViewModel : NotifyPropertyChangedHandler
{
    public List<Garment> Garments { get; set; }
    public GarmentViewModel()
    {
        GetJsonData();
    }

    private void GetJsonData()
    {
        var filePath = @"SourceDataFile\GarmentData.json";

        var json = System.IO.File.ReadAllText(filePath);
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        Garments = JsonSerializer.Deserialize<List<Garment>>(json, options)!;
    }
}
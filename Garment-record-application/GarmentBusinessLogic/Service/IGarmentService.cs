using GarmentRecordLibrary.Model;

namespace GarmentBusinessLogic.Service;

public interface IGarmentService
{
    public IList<Garment>? GarmentList { get; set; }
    IList<Garment> LoadFromFile(string path);
    bool AddGarment(Garment garment);
    bool SaveToFile();
    void ResetGarmentListToDefault();
    bool UpdateGarment(string oldGarmentId, Garment newGarment);
    bool DeleteGarment(string garmentId);
    bool SearchGarment(string garmentId);
    bool SortGarments();
}
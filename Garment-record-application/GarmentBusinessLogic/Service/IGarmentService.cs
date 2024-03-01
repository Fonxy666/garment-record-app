using GarmentRecordLibrary.Model;

namespace GarmentBusinessLogic.Service;

public interface IGarmentService
{
    public IList<Garment>? GarmentList { get; set; }
    IList<Garment> LoadFromFile(string path);
    bool AddGarment(Garment garment);
    bool SaveToFile();
    void ResetGarmentListToDefault();
    bool UpdateGarment(uint oldGarmentId, Garment newGarment);
    bool DeleteGarment(uint garmentId);
    Garment SearchGarment(uint garmentId);
    bool SortGarments();
}
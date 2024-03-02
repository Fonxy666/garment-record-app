using GarmentRecordLibrary.Model;

namespace GarmentBusinessLogic.Service;

public interface IGarmentService
{
    public IList<Garment>? GarmentList { get; set; }
    void AddGarment(Garment garment);
    void ResetGarmentListToDefault();
    void UpdateGarment(uint oldGarmentId, Garment newGarment);
    void DeleteGarment(uint garmentId);
    Garment SearchGarment(uint garmentId);
    void SortGarments();
}
using GarmentRecordLibrary.Model.Enum;
using GarmentRecordLibrary.ViewModel;

namespace GarmentRecordLibrary.Model;

public class Garment : NotifyPropertyChangedHandler
{
    private uint _id;
    public uint Id
    {
        get => _id;
        set { _id = value; NotifyPropertyChanged("Id"); }
    }
    
    private string? _brandName;
    public string? BrandName
    {
        get => _brandName;
        set { _brandName = value; NotifyPropertyChanged("BrandName"); }
    }
    
    private DateTime _purchase = DateTime.Now;
    public DateTime Purchase
    {
        get => _purchase;
        set { _purchase = value; NotifyPropertyChanged("Purchase"); }
    }

    private string? _color;
    public string? Color
    {
        get => _color;
        set { _color = value; NotifyPropertyChanged("Color"); }
    }

    private GarmentSize _size;
    public GarmentSize Size
    {
        get => _size;
        set { _size = value; NotifyPropertyChanged("Size"); }
    }
}
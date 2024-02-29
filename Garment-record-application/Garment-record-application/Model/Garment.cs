using Garment_record_application.Model.Enum;
using Garment_record_application.ViewModel;

namespace Garment_record_application.Model;

public class Garment : NotifyPropertyChangedHandler
{
    public Garment(uint id, string brandName, DateTime purchase, string color, GarmentSize size)
    {
        Id = id;
        BrandName = brandName;
        Purchase = purchase;
        Color = color;
        Size = size;
    }
    
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
    
    private DateTime _purchase;
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
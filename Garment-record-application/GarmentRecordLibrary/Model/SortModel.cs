using GarmentRecordLibrary.ViewModel;

namespace GarmentRecordLibrary.Model;

public class SortModel : NotifyPropertyChangedHandler
{
    private bool _id;
    private bool _brandName;
    private bool _color;
    private bool _purchase;
    private bool _size;

    public bool Id
    {
        get => _id;
        set
        {
            if (_id != value)
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }
    }
    
    public bool BrandName
    {
        get => _brandName;
        set
        {
            if (_brandName != value)
            {
                _brandName = value;
                NotifyPropertyChanged(nameof(BrandName));
            }
        }
    }
    
    public bool Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                NotifyPropertyChanged(nameof(Color));
            }
        }
    }
    
    public bool Purchase
    {
        get => _purchase;
        set
        {
            if (_purchase != value)
            {
                _purchase = value;
                NotifyPropertyChanged(nameof(Purchase));
            }
        }
    }
    
    public bool Size
    {
        get => _size;
        set
        {
            if (_size != value)
            {
                _size = value;
                NotifyPropertyChanged(nameof(Size));
            }
        }
    }
}
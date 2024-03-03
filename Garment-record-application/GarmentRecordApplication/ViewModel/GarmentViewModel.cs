using System.Windows;
using System.Windows.Input;
using Garment_record_application.ICommandUpdater;
using GarmentBusinessLogic.Service;
using GarmentBusinessLogic.Service.Logger;
using GarmentRecordLibrary.Model;
using GarmentRecordLibrary.ViewModel;

namespace Garment_record_application.ViewModel;

public class GarmentViewModel : NotifyPropertyChangedHandler
{
    private ICommand _showDataCommand;
    private ICommand _addCommand;
    private ICommand _updateCommand;
    private ICommand _deleteCommand;
    private ILogger _logger;
    
    private readonly IGarmentService _garmentService;
    
    private string _filterText;
    public string FilterText
    {
        get => _filterText; 
        set
        {
            if (_filterText != value)
            {
                _filterText = value;
                FilterGarments();
            }
        }
    }
    
    private Garment _selectedGarment;
    public Garment SelectedGarment
    {
        get => _selectedGarment;
        set { _selectedGarment = value; NotifyPropertyChanged("SelectedGarment"); }
    }
    
    public IList<Garment> Garments { get; set; }
    public GarmentViewModel()
    {
        _logger = new Logger();
        _garmentService = new GarmentService("GarmentData.json", _logger);
        GetJsonData();
        SelectedGarment = new Garment();
        
    }

    public ICommand ShowDataCommand
    {
        get
        {
            if (_showDataCommand == null)
            {
                _showDataCommand = new RelayCommand(param => GetJsonData(), null);
            }

            return _showDataCommand;
        }
    }
    
    private void GetJsonData()
    {
        _garmentService.ResetGarmentListToDefault();
        Garments = _garmentService.GarmentList!;
        NotifyPropertyChanged("Garments");
    }

    public ICommand AddCommand
    {
        get
        {
            if (_addCommand == null)
            {
                _addCommand = new RelayCommand(param => AddJsonData(), null);
            }

            return _addCommand;
        }
    }

    private void AddJsonData()
    {
        if (Garments == null)
        {
            Garments = new List<Garment>();
        }

        SelectedGarment.Id = Garments.Count > 0 ? Garments.LastOrDefault()?.Id + 1 ?? 1 : 1;

        if (SelectedGarment.Id <= 0)
        {
            MessageBox.Show("Invalid ID. Aborting addition.");
            return;
        }

        if (!EmptyInput())
        {
            _garmentService.AddGarment(SelectedGarment);
            MessageBox.Show("New garment successfully added.");
            GetJsonData();
            
            SelectedGarment = new Garment();
        }
    }

    private bool EmptyInput()
    {
        if (string.IsNullOrEmpty(SelectedGarment.BrandName))
        {
            MessageBox.Show("Brand name cannot be empty.");
            return true;
        }

        if (string.IsNullOrEmpty(SelectedGarment.Color))
        {
            MessageBox.Show("Color cannot be empty.");
            return true;
        }

        return false;
    }

    public ICommand UpdateCommand
    {
        get
        {
            if (_updateCommand == null)
            {
                _updateCommand = new RelayCommand(param => UpdateJsonData(), null);
            }

            return _updateCommand;
        }
    }

    private void UpdateJsonData()
    {
        if (!EmptyInput())
        {
            _garmentService.UpdateGarment(_selectedGarment.Id, SelectedGarment);
            MessageBox.Show("Garment data updated successfully.");
            GetJsonData();
            
            SelectedGarment = new Garment();
        }
    }

    public ICommand DeleteCommand
    {
        get
        {
            if (_deleteCommand == null)
            {
                _deleteCommand = new RelayCommand(param => DeleteGarment(), null);
            }

            return _deleteCommand;
        }
    }

    private void DeleteGarment()
    {
        var deletedId = _selectedGarment.Id;
        
        _garmentService.DeleteGarment(deletedId);
        MessageBox.Show("Given garment data is deleted successfully.");
        GetJsonData();
        
        SelectedGarment = new Garment();
    }

    private void FilterGarments()
    {
        GetJsonData();
        Garments = Garments.Where(garment => garment.BrandName!.Contains(_filterText)).ToList();
        NotifyPropertyChanged("Garments");
    }
}
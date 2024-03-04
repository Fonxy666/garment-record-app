using System.Windows;
using System.Windows.Input;
using Garment_record_application.ICommandUpdater;
using Garment_record_application.View;
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
    private ICommand _sortCommand;
    private ILogger _logger;
    public SortModel GarmentSortModel { get; set; }
    
    private readonly IGarmentService _garmentService;
    
    private string _filterByIdText;
    public string FilterById
    {
        get => _filterByIdText; 
        set
        {
            if (_filterByIdText != value)
            {
                _filterByIdText = value;
                FilterGarmentsById();
            }
        }
    }
    
    private string _filterByNameText;
    public string FilterByName
    {
        get => _filterByNameText; 
        set
        {
            if (_filterByNameText != value)
            {
                _filterByNameText = value;
                FilterGarmentsByName();
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
        GarmentSortModel = new SortModel();
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
                _addCommand = new RelayCommand(param => ShowGarmentModal(true), null);
            }

            return _addCommand;
        }
    }
    
    public void ShowGarmentModal(bool insNew)
    {
        var addGarmentWindow = new GarmentWindow(this, insNew)
        {
            DataContext = SelectedGarment
        };
        addGarmentWindow.ShowDialog();
    }

    public void AddJsonData()
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
                _updateCommand = new RelayCommand(param => ShowGarmentModal(false), null);
            }

            return _updateCommand;
        }
    }

    public void UpdateJsonData()
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

    private void FilterGarmentsById()
    {
        GetJsonData();
        Garments = Garments.Where(garment => garment.BrandName!.Contains(_filterByIdText)).ToList();
        NotifyPropertyChanged("Garments");
    }
    
    private void FilterGarmentsByName()
    {
        GetJsonData();
        Garments = Garments.Where(garment => garment.BrandName!.Contains(_filterByNameText)).ToList();
        NotifyPropertyChanged("Garments");
    }
    
    public ICommand SortCommand
    {
        get
        {
            if (_sortCommand == null)
            {
                _sortCommand = new RelayCommand(param => SortGarments(param as string), null);
            }

            return _sortCommand;
        }
    }

    private void SortGarments(string param)
    {
        GarmentSortModel.Id = false;
        GarmentSortModel.BrandName = false;
        GarmentSortModel.Color = false;
        GarmentSortModel.Purchase = false;
        GarmentSortModel.Size = false;
        
        switch (param)
        {
            case "id":
                GarmentSortModel.Id = true;
                _garmentService.SortGarments(param);
                break;
            
            case "name":
                GarmentSortModel.BrandName = true;
                _garmentService.SortGarments(param);
                break;
            
            case "color":
                GarmentSortModel.Color = true;
                _garmentService.SortGarments(param);
                break;
            
            case "purchase":
                GarmentSortModel.Purchase = true;
                _garmentService.SortGarments(param);
                break;
            
            case "size":
                GarmentSortModel.Size = true;
                _garmentService.SortGarments(param);
                break;
        }

        GetJsonData();
    }
}
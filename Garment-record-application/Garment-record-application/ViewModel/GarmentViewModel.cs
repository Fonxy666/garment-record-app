using System.IO;
using System.Windows;
using System.Windows.Input;
using Garment_record_application.ICommandUpdater;
using Garment_record_application.Model;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Garment_record_application.ViewModel;

public class GarmentViewModel : NotifyPropertyChangedHandler
{
    private ICommand _showDataCommand;
    private ICommand _addCommand;
    private ICommand _updateCommand;
    private ICommand _deleteCommand;
    private Garment _selectedGarment;

    public Garment SelectedGarment
    {
        get => _selectedGarment;
        set { _selectedGarment = value; NotifyPropertyChanged("SelectedGarment"); }
    }
    
    public List<Garment> Garments { get; set; }
    public GarmentViewModel()
    {
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
        var json = File.ReadAllText("GarmentData.json");
        Garments = JsonConvert.DeserializeObject<List<Garment>>(json)!;
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
            Garments = new List<Garment>();

        if (SelectedGarment == null)
        {
            SelectedGarment = new();
            MessageBox.Show("SelectedGarment is null. Aborting addition.");
            return;
        }

        SelectedGarment.Id = Garments.Count > 0 ? Garments.LastOrDefault()?.Id + 1 ?? 1 : 1;

        if (SelectedGarment.Id <= 0)
        {
            MessageBox.Show("Invalid ID. Aborting addition.");
            return;
        }

        if (!EmptyInput())
        {
            Garments.Add(SelectedGarment);

            var newJsonData = JsonConvert.SerializeObject(Garments, Formatting.Indented);
            File.WriteAllText("GarmentData.json", newJsonData);
            MessageBox.Show("Given garment data is added successfully...");
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
            var updatedJson = JsonConvert.SerializeObject(Garments, Formatting.Indented);
            File.WriteAllText("GarmentData.json", updatedJson);
            MessageBox.Show("Given garment data is updated successfully...");
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
        Garments.Remove(_selectedGarment);
        
        for (var i = (int)deletedId - 1; i < Garments.Count; i++)
        {
            Garments[i].Id = (uint)(i + 1);
        }
        
        
        var updatedJson = JsonConvert.SerializeObject(Garments, Formatting.Indented);
        File.WriteAllText("GarmentData.json", updatedJson);
        MessageBox.Show("Given garment data is deleted successfully...");
        GetJsonData();
        
        SelectedGarment = new Garment();
    }
}
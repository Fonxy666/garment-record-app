﻿using System.IO;
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
        SelectedGarment.Id = Garments.Count > 0 ? Garments[^1].Id + 1 : 1;
        Garments.Add(SelectedGarment);

        var newJsonData = JsonConvert.SerializeObject(Garments.ToList(), Formatting.Indented);
        File.WriteAllText("GarmentData.json", newJsonData);
    }
}
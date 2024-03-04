using System.Windows;
using Garment_record_application.ViewModel;

namespace Garment_record_application.View;

public partial class GarmentWindow : Window
{
    private readonly GarmentViewModel _viewModel;
    private readonly bool _isNewItem;
    
    public string AddButtonContent => _isNewItem ? "Add" : "Update";
    public GarmentWindow(GarmentViewModel viewModel, bool isNew)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _isNewItem = isNew;
        DataContext = this;
    }
    
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel != null)
        {
            if (_isNewItem)
            {
                _viewModel.AddJsonData();
            }
            else
            {
                _viewModel.UpdateJsonData();
            }

            Close();
        }
    }
}
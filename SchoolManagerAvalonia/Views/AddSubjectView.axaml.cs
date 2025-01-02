using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaDialogs.Views;
using SchoolManagerModel.Utils;
using SchoolManagerViewModel;

namespace SchoolManagerAvalonia;

public partial class AddSubjectView : UserControl
{
    public AddSubjectView()
    {
        InitializeComponent();
        CreateViewModel();
        LoadViewModelData();
    }
    
    private async void LoadViewModelData()
    {
        var viewmodel = GetViewModel();
        var task1 = viewmodel.LoadClasses();
        var task2 = viewmodel.LoadTeachers();

        await Task.WhenAll(task1, task2);
    }
    
    private void CreateViewModel()
    {
        var resourceManager = UIResourceFactory.GetNewResource();
        var viewModel = GetViewModel();
        viewModel.SuccessfulAdd = async () =>
        {
            CreateViewModel();
            LoadViewModelData();
            
            SingleActionDialog dialog = new()
            {
                Message = resourceManager.GetStringOrDefault("SuccessfullyAdded"),
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };
        
        viewModel.FailedAdd = async (string message) =>
        {
            SingleActionDialog dialog = new()
            {
                Message = message,
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };
        
        DataContext = viewModel;
    }

    private AddSubjectViewModel GetViewModel()
    {
        return DataContext as AddSubjectViewModel ?? new AddSubjectViewModel();
    }
}
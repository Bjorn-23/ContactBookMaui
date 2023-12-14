using ContactBookMaui.ViewModels;

namespace ContactBookMaui;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    // Check and request permissions
    //    await CheckAndRequestPermissions();
    //}

    //private async Task CheckAndRequestPermissions()
    //{
    //    try
    //    {
    //        var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
    //        if (status != PermissionStatus.Granted)
    //        {
    //            // Permission is not granted, request it
    //            status = await Permissions.RequestAsync<Permissions.StorageWrite>();
    //        }

    //        if (status == PermissionStatus.Granted)
    //        {
    //            // Permission is granted, proceed with file access
    //            // ...
                
    //        }
    //        else
    //        {
    //            // Permission is denied
    //            // Handle accordingly (show a message, disable features, etc.)
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle exceptions, e.g., if permission checking or requesting fails
    //        Console.WriteLine($"Exception: {ex.Message}");
    //    }
    //}

}

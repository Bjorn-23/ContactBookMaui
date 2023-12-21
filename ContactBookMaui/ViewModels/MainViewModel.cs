using CommunityToolkit.Mvvm.ComponentModel;
using ContactBook_Shared.Models;
using ContactBook_Shared.Interfaces;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IPContactServices _pContactServices;

    public MainViewModel(IPContactServices pContactServices)
    {
        _pContactServices = pContactServices;
        PContactList = _pContactServices.GetAllContactsFromList();
        UpdateContactList();
    }

    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

    /// <summary>
    /// Updates (PContactlist) in methods after that method modifies it.
    /// </summary>
    public void UpdateContactList()
    {
        try
        {
            PContactList = _pContactServices.GetAllContactsFromList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
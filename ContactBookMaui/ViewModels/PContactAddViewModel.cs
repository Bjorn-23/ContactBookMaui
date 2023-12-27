using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContactBookMaui.ViewModels;

public partial class PContactAddViewModel : ObservableObject
{
    private readonly IPContactServices _pContactServices;

    public PContactAddViewModel(IPContactServices pContactServices)
    {
        _pContactServices = pContactServices;
        PContactList = _pContactServices.GetAllContactsFromList();
        UpdateContactList();
    }

    /// <summary>
    /// Form for filling in new details via (ContactAddPage )or edit details on (ContactUpdatePage)
    /// </summary>
    [ObservableProperty]
    private PContact _registrationForm = new();

    /// <summary>
    /// Main List for storing (PContacts) while application is running.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

    /// <summary>
    /// Adds new (PContact) to PContactList
    /// </summary>
    [RelayCommand]
    public void AddContactToList()
    {
        if (RegistrationForm != null && !string.IsNullOrWhiteSpace(RegistrationForm.Email))
        {
            var result = _pContactServices.AddContactToList(RegistrationForm);
            if (result)
            {
                UpdateContactList();
                RegistrationForm = new();
                _ = NavigateToListContact();
            }
        }
    }

    /// <summary>
    /// Link To (ContactListPage), used in (AddContactToList)
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private static async Task NavigateToListContact()
    {
        await Shell.Current.GoToAsync("//ContactListPage");
    }

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

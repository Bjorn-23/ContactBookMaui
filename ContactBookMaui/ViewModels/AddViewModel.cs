using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContactBookMaui.ViewModels;

public partial class AddViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    public AddViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
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
            var result = _contactRepository.AddContactToList(RegistrationForm);
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
    private async Task NavigateToListContact()
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
            PContactList = new ObservableCollection<IPContact>(_contactRepository.GetAllContactsFromList().Select(contact => contact).ToList());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}

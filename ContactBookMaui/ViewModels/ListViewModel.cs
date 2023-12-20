using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    public ListViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        _contactRepository.PContactListUpdated += (sender, e) =>
        {
            PContactList = new ObservableCollection<IPContact>(_contactRepository.GetAllContactsFromList().Select(contact => contact).ToList());
        };
        UpdateContactList();
    }

    /// <summary>
    /// Main List for storing (PContacts) while application is running.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

    /// <summary>
    /// Passes information from the (PContact) associated with the "Edit" button pressed to (ContactUpdatedPage) and navigates there
    /// </summary>
    /// <param name="contactToUpdate">(PContact) parameters</param>
    /// <returns></returns>
    [RelayCommand]
    public async Task NavigateToUpdateContact(IPContact contactToUpdate)
    {
        IPContact contact = new PContact()
        {
            FirstName = contactToUpdate.FirstName,
            LastName = contactToUpdate.LastName,
            Email = contactToUpdate.Email,
            Address = contactToUpdate.Address,
            PhoneNumber = contactToUpdate.PhoneNumber,
        };

        var parameters = new ShellNavigationQueryParameters
        {
            {"PContact", contact }
        };

        await Shell.Current.GoToAsync("//ContactUpdatePage", parameters);
    }

    /// <summary>
    /// Passes information from the (PContact) associated with the "X" button pressed to (ContactDeletePage) and navigates there
    /// </summary>
    /// <param name="contactToDelete">(PContact) parameters</param>
    /// <returns></returns>
    [RelayCommand]
    public async Task NavigateToDeleteContact(IPContact contactToDelete)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"PContact", contactToDelete }
        };

        await Shell.Current.GoToAsync("//ContactDeletePage", parameters);
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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContactBookMaui.ViewModels;

public partial class PContactListViewModel : ObservableObject
{
    private readonly IPContactServices _pContactServices;

    public PContactListViewModel(IPContactServices pContactServices)
    {
        _pContactServices = pContactServices;
        _pContactServices.PContactListUpdated += (sender, e) =>
        {
            PContactList = _pContactServices.GetAllContactsFromList();
        };
        UpdateContactList();
    }

    /// <summary>
    /// Main List for storing (PContacts) while application is running.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

    /// <summary>
    /// Passes information, from the (PContact) associated with the "Edit" button that was pressed, to (ContactUpdatedPage) and navigates there
    /// </summary>
    /// <param name="contactToUpdate">(PContact) parameters</param>
    /// <returns></returns>
    [RelayCommand]
    public static async Task NavigateToUpdateContact(IPContact contactToUpdate)
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
    /// Passes information, from the (PContact) associated with the "X" button that was pressed, to (ContactDeletePage) and navigates there
    /// </summary>
    /// <param name="contactToDelete">(PContact) parameters</param>
    /// <returns></returns>
    [RelayCommand]
    public static async Task NavigateToDeleteContact(IPContact contactToDelete)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"PContact", contactToDelete }
        };

        await Shell.Current.GoToAsync("//ContactDeletePage", parameters);
    }

    /// <summary>
    /// Passes information, from the (PContact) associated with the "List" button that was pressed, to (ContactDetailsPage) and navigates there.
    /// </summary>
    /// <param name="contactDetails">(PContact) parameters</param>
    /// <returns></returns>
    [RelayCommand]
    public static async Task NavigateToContactDetails(IPContact contactDetails)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"PContact", contactDetails }
        };

        await Shell.Current.GoToAsync("//ContactDetailsPage", parameters);
    }

    [RelayCommand]
    public static async Task NavigateToAddWithNoData()
    {
        await Shell.Current.GoToAsync("//ContactAddPage");
    }

    [RelayCommand]
    public static async Task NavigateToDetailsWithNoData()
    {
        await Shell.Current.GoToAsync("//ContactDetailsPage");
    }

    [RelayCommand]
    public static async Task NavigateToUpdateWithNoData()
    {
        await Shell.Current.GoToAsync("//ContactUpdatePage");
    }

    [RelayCommand]
    public static async Task NavigateToDeleteWithNoData()
    {
        await Shell.Current.GoToAsync("//ContactDeletePage");
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

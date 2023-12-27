using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContactBookMaui.ViewModels;

public partial class PContactDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly IPContactServices _pContactServices;

    public PContactDetailsViewModel(IPContactServices pContactServices)
    {
        _pContactServices = pContactServices;
        _pContactServices.PContactListUpdated += (sender, e) =>
        {
            PContactList = _pContactServices.GetAllContactsFromList();
        };
    }
    /// <summary>
    /// Form for filling in new details via (ContactAddPage )or edit details on (ContactUpdatePage)
    /// </summary>
    [ObservableProperty]
    private PContact _registrationForm = new();

    /// <summary>
    /// Form accepting string of email to find and display details of contacts in (PContactList) when updating (ContactUpdatePage) or deleting (ContactDeletePage) contacts.
    /// </summary>
    [ObservableProperty]
    private PContact _emailOfContactToUpdateOrDelete = new();

    /// <summary>
    /// Main List for storing (PContacts) while application is running.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

    /// <summary>
    /// List that displays the details associated with the email used in form (_emailOfContactToUpdateOrDelete)
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IPContact> _singlePContactByEmail = [];

    /// <summary>
    /// List displaying updated contact details after using (ContactUpdatePage)
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IPContact> _updatedContactByEmail = [];

    /// <summary>
    /// Creates new ObservableCollection (_singlePContactByEmail) from email input in form (_emailOfContactToUpdateOrDelete) and the method (GetContactFromListByEmail)
    /// </summary>
    /// <param name="contactToUpdate">Email param from IPContact</param>
    [RelayCommand]
    public void GetContactByEmailButton(IPContact contactToUpdate)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(contactToUpdate.Email))
            {
                SinglePContactByEmail = _pContactServices.GetContactFromListByEmail(contactToUpdate);
                UpdatedContactByEmail = [];

                if (SinglePContactByEmail.Count == 0)
                {
                    PContactDetailsViewModel.ErrorOnUpDateAlert(ErrorCodes.NotFound);
                    ClearDataOnScreen();
                }
            }
            else
            {
                PContactDetailsViewModel.ErrorOnUpDateAlert(ErrorCodes.BadRequest);
                ClearDataOnScreen();

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }


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
    /// Link To (ContactListPage), used in (AddContactToList)
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private static async Task NavigateToListContact()
    {
        await Shell.Current.GoToAsync("//ContactListPage");
    }

    /// <summary>
    /// Takes params via Edit button from list on (ContactListPage) and passes them to (GetContactByEmailButton). Also Prepoulates (RegistrationForm) on (ContactUpdatePage) with (contactToUpdate)
    /// </summary>
    /// <param name="query">(PContact) data</param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            var contactToUpdate = (query["PContact"] as PContact)!;
            GetContactByEmailButton(contactToUpdate);
            RegistrationForm = contactToUpdate;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Displays error messages when (GetContactByEmailButton) has wrong or missing input
    /// </summary>
    /// <param name="errorCode">Enums representing error message in a clear way.</param>
    private static async void ErrorOnUpDateAlert(ErrorCodes errorCode)
    {
        switch (errorCode)
        {
            case ErrorCodes.BadRequest:
                await Shell.Current.DisplayAlert("400 Bad Request - No Email provided", "Please Enter An Email", "Continue")!;
                break;
            case ErrorCodes.NotFound:
                await Shell.Current.DisplayAlert("404 Not Found - No Contact with that Email", "Please Enter A Valid Email", "Continue")!;
                break;

        }
    }

    /// <summary>
    /// Clears form data and textmessages displayed on (ContactUpdatePage)
    /// </summary>
    private void ClearDataOnScreen()
    {
        SinglePContactByEmail = [];
        UpdatedContactByEmail = [];
        RegistrationForm = new();
    }
}

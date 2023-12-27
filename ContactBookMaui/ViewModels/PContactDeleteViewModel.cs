using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContactBookMaui.ViewModels;

public partial class PContactDeleteViewModel : ObservableObject, IQueryAttributable
{
    private readonly IPContactServices _pContactServices;

    public PContactDeleteViewModel(IPContactServices pContactServices)
    {
        _pContactServices = pContactServices;
        _pContactServices.PContactListUpdated += (sender, e) =>
        {
            PContactList = _pContactServices.GetAllContactsFromList();
        };
        UpdateContactList();
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
    /// List displaying a custom text after updating a contact or deleting a contact.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<string> _statusUpdateText = [];

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

                if (StatusUpdateText.Any())
                {
                    StatusUpdateText.RemoveAt(0);
                }
                else if (SinglePContactByEmail.Count == 0)
                {
                    ErrorOnUpDateAlert(ErrorCodes.NotFound);
                    ClearDataOnScreen();
                }
            }
            else
            {
                ErrorOnUpDateAlert(ErrorCodes.BadRequest);
                ClearDataOnScreen();

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// /// Sends (contactToDelete = SinglePContact) to the method (DeleteContactByEmail) to remove it from the List.
    /// </summary>
    [RelayCommand]
    public void RemoveContactByEmail()
    {
        try
        {
            if (RegistrationForm != null && !string.IsNullOrWhiteSpace(RegistrationForm.Email))
            {
                IPContact contactToDelete = SinglePContactByEmail.FirstOrDefault()!;
                string displayText = "Has been deleted.";

                if (contactToDelete != null)
                {
                    var result = _pContactServices.DeleteContactByEmail(contactToDelete);
                    if (result)
                    {
                        StatusUpdateText.Add(displayText);
                        UpdateContactList();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Cancels deleting contact and returns to (ContactListPage)
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task CancelAndNavigateToListContact()
    {
        ClearDataOnScreen();
        await Shell.Current.GoToAsync("//ContactListPage");
    }

    /// <summary>
    /// Displays error messages when (GetContactByEmailButton) has wrong or missing input
    /// </summary>
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
    /// <summary>
    /// Takes params via "X" button from list on (ContactListPage) and passes them to (GetContactByEmailButton). Also Prepoulates (RegistrationForm) on (ContactDeletePage) with (contactToDelete)
    /// </summary>
    /// <param name="query">(PContact) data</param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            var contactToDelete = (query["PContact"] as PContact)!;
            GetContactByEmailButton(contactToDelete);
            RegistrationForm = contactToDelete;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

    }

    /// <summary>
    /// Clears form data and textmessages displayed on (ContactUpdatePage)
    /// </summary>
    private void ClearDataOnScreen()
    {
        SinglePContactByEmail = [];
        RegistrationForm = new();
        if (StatusUpdateText.Any())
        {
            StatusUpdateText.RemoveAt(0);
        }
    }
}

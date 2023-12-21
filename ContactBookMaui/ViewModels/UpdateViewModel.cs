using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using ContactBookMaui.Pages;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;
internal enum ErrorCodes
{
    BadRequest, 
    NotFound,
}
public partial class UpdateViewModel : ObservableObject, IQueryAttributable
{
    private readonly IContactRepository _contactRepository;

    public UpdateViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        //_contactRepository.PContactListUpdated += (sender, e) =>
        //{
        //    PContactList = new ObservableCollection<IPContact>(_contactRepository.GetAllContactsFromList().Select(contact => contact).ToList());
        //};
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
    /// List displaying updated contact details after using (ContactUpdatePage)
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<IPContact> _updatedContactByEmail = [];

    /// <summary>
    /// List displaying a custom text after updating a contact or deleting a contact.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<string> _statusUpdateText = new ObservableCollection<string>();


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
                SinglePContactByEmail = new ObservableCollection<IPContact>(_contactRepository.GetContactFromListByEmail(contactToUpdate).Select(contact => contact).ToList()) ?? [];
                UpdatedContactByEmail = [];
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
    /// Sends (contactToDelete = SinglePContact) and (updatedContact = _registrationForm) to the method (UpdateContactToListByEmail)
    /// </summary>
    /// <param name="updatedContact">Input parameters from (RegistrationForm)</param>
    [RelayCommand]
    public void UpdateContactButton(PContact updatedContact)
    {
        if (RegistrationForm != null && !string.IsNullOrWhiteSpace(RegistrationForm.Email))
        {
            IPContact contactToUpdate = SinglePContactByEmail.FirstOrDefault()!;

            string textToAdd = "Updated To:";

            if (contactToUpdate != null)
            {
                var result = _contactRepository.UpdateContactToListByEmail(contactToUpdate, updatedContact);
                if (result)
                {
                    StatusUpdateText.Add(textToAdd);
                    UpdatedContactByEmail = new ObservableCollection<IPContact>(_contactRepository.GetContactFromListByEmail(updatedContact).Select(contact => contact).ToList()) ?? [];
                    UpdateContactList();
                    RegistrationForm = new();
                }
            }
        }
    }

    /// <summary>
    /// Cancels updating contact and returns to (ContactListPage)
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task CancelAndNavigateToListContact()
    {
        ClearDataOnScreen();
        //await Shell.Current.GoToAsync(nameof(ContactListPage)); - The blue underline in tab navigation doesnt move when using this command.
        await Shell.Current.GoToAsync("//ContactListPage");
    }


    /// <summary>
    /// Displays error messages when (GetContactByEmailButton) has wrong or missing input
    /// </summary>
    /// <param name="errorCode">Enums representing error message in a clear way.</param>
    private async void ErrorOnUpDateAlert(ErrorCodes errorCode)
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
            PContactList = new ObservableCollection<IPContact>(_contactRepository.GetAllContactsFromList().Select(contact => contact).ToList());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    /// <summary>
    /// Takes params via Edit button from list on (ContactListPage) and passes them to (GetContactByEmailButton). Also Prepoulates (RegistrationForm) on (ContactUpdatePage) with (contactToUpdate)
    /// </summary>
    /// <param name="query">(PContact) data</param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var contactToUpdate = (query["PContact"] as PContact)!;
        GetContactByEmailButton(contactToUpdate);
        RegistrationForm = contactToUpdate;
    }

    /// <summary>
    /// Clears form data and textmessages displayed on (ContactUpdatePage)
    /// </summary>
    private void ClearDataOnScreen()
    {
        SinglePContactByEmail = [];
        UpdatedContactByEmail = [];
        RegistrationForm = new();
        if (StatusUpdateText.Any())
        {
            StatusUpdateText.RemoveAt(0);
        }
    }
}
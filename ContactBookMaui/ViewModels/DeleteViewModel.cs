using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;

public partial class DeleteViewModel : ObservableObject//, IQueryAttributable
{
    private readonly IContactRepository _contactRepository;

    public DeleteViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        //_contactRepository.PContactListUpdated += (sender, e) =>
        //{
        //    PContactList = new ObservableCollection<IPContact>(_contactRepository.GetAllContactsFromList().Select(contact => contact).ToList());
        //};
        UpdateContactList();
    }

    [ObservableProperty]
    private PContact _registrationForm = new();

    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

    [ObservableProperty]
    private ObservableCollection<IPContact> _singlePContactByEmail = [];

    [ObservableProperty]
    private ObservableCollection<string> _statusUpdateText = new ObservableCollection<string>();

    [RelayCommand]
    public void GetContactByEmailButton(IPContact contactToUpdate)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(contactToUpdate.Email))
            {

                SinglePContactByEmail = new ObservableCollection<IPContact>(_contactRepository.GetContactFromListByEmail(contactToUpdate).Select(contact => contact).ToList()) ?? [];
                if (StatusUpdateText.Any())
                {
                    StatusUpdateText.RemoveAt(0);
                }
                else if (SinglePContactByEmail.Count == 0)
                {
                    ErrorOnUpDateAlert("2");
                    ClearDataOnScreen();
                }
            }
            else
            {
                ErrorOnUpDateAlert("1");
                ClearDataOnScreen();

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    public void RemoveContactByEmail()
    {
        if (RegistrationForm != null && !string.IsNullOrWhiteSpace(RegistrationForm.Email))
        {
            IPContact contactToDelete = SinglePContactByEmail.FirstOrDefault()!;
            string displayText = "Has been deleted.";

            if (contactToDelete != null)
            {
                var result = _contactRepository.DeleteContactByEmail(contactToDelete);
                if (result)
                {
                    StatusUpdateText.Add(displayText);
                    UpdateContactList();
                }
            }
        }
    }

    [RelayCommand]
    private async Task NavigateToListContact()
    {
        await Shell.Current.GoToAsync("//ContactListPage");
    }

    private async void ErrorOnUpDateAlert(string num)
    {
        switch (num)
        {
            case "1":
                await Shell.Current.DisplayAlert("400 Bad Request - No Email provided", "Please Enter An Email", "Continue")!;
                break;
            case "2":
                await Shell.Current.DisplayAlert("404 Not Found - No Contact with that Email", "Please Enter A Valid Email", "Continue")!;
                break;

        }
    }

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

    //public void ApplyQueryAttributes(IDictionary<string, object> query)
    //{
    //    RegistrationForm = (query["PContact"] as PContact)!;
    //}

    private void ClearDataOnScreen()
    {
        SinglePContactByEmail = [];
        if (StatusUpdateText.Any())
        {
            StatusUpdateText.RemoveAt(0);
        }
    }

}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;

public partial class UpdateViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    public UpdateViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        UpdateContactList();
    }

    [ObservableProperty]
    private PContact _registrationForm = new();

    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

    [ObservableProperty]
    private ObservableCollection<IPContact> _singlePContactByEmail = [];

    [ObservableProperty]
    private ObservableCollection<IPContact> _updatedContactByEmail = [];

    [ObservableProperty]
    private ObservableCollection<string> _statusUpdateText = new ObservableCollection<string>();

    [RelayCommand]
    public void GetContactByEmailButton(IPContact contactToUpdate)
    {
        try
        {
            if (contactToUpdate.Email != null!)
            {
                SinglePContactByEmail = new ObservableCollection<IPContact>(_contactRepository.GetContactFromListByEmail(contactToUpdate).Select(contact => contact).ToList()) ?? [];
                UpdatedContactByEmail = [];
                if (SinglePContactByEmail.Count == 0)//if (!SinglePContactByEmail.Any())
                {
                    SinglePContactByEmail = [];
                    UpdatedContactByEmail = [];
                    StatusUpdateText.RemoveAt(0);
                    ErrorOnUpDateAlert("2");

                }
            }
            else if (contactToUpdate.Email == null)
            {
                SinglePContactByEmail = [];
                UpdatedContactByEmail = [];
                StatusUpdateText.RemoveAt(0);
                ErrorOnUpDateAlert("1");

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    public void UpdateContactButton(PContact updatedContact)
    {
        if (RegistrationForm != null && !string.IsNullOrWhiteSpace(RegistrationForm.Email))
        {
            IPContact contactToDelete = SinglePContactByEmail.FirstOrDefault()!;
            string textToAdd = "Will be updated to:";

            if (contactToDelete != null)
            {
                var result = _contactRepository.UpdateContactToListByEmail((IPContact)contactToDelete, updatedContact);
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
}
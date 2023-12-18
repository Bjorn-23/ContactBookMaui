﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;

public partial class UpdateViewModel : ObservableObject, IQueryAttributable
{
    private readonly IContactRepository _contactRepository;

    public UpdateViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        _contactRepository.PContactListUpdated += (sender, e) =>
        {
            PContactList = new ObservableCollection<IPContact>(_contactRepository.GetAllContactsFromList().Select(contact => contact).ToList());
        };
        UpdateContactList();
    }

    [ObservableProperty]
    private PContact _registrationForm = new();

    [ObservableProperty]
    private PContact _emailOfContactToUpdateOrDelete = new();

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

    [RelayCommand]
    private async Task CancelAndNavigateToListContact()
    {
        ClearDataOnScreen();
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
    /// <summary>
    /// Sets 
    /// </summary>
    /// <param name="query"></param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var contactToUpdate = (query["PContact"] as PContact)!;
        GetContactByEmailButton(contactToUpdate);
        RegistrationForm = contactToUpdate;
    }

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
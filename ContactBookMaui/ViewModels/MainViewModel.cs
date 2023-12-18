﻿using CommunityToolkit.Mvvm.ComponentModel;
using ContactBook_Shared.Models;
using ContactBook_Shared.Interfaces;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    public MainViewModel(IContactRepository contactRepository)
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
    private ObservableCollection<IPContact> _pContactList = [];

    [ObservableProperty]
    private ObservableCollection<IPContact> _singlePContactByEmail = [];

    [ObservableProperty]
    private ObservableCollection<IPContact> _updatedContactByEmail = [];

    [ObservableProperty]
    private ObservableCollection<string> _statusUpdateText = new ObservableCollection<string>();

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
            }
        }
    }

    [RelayCommand]
    public void RemoveContactButton(IPContact contactToDelete)
    {
        if (contactToDelete != null)
        {
            var result = _contactRepository.DeleteContactByEmail(contactToDelete);
            if (result)
            {
                UpdateContactList();
            }
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
    private async Task NavigateToAddContact()
    {
        await Shell.Current.GoToAsync("//ContactAddPage");
    }

    [RelayCommand]
    private async Task NavigateToListContact()
    {
        await Shell.Current.GoToAsync("//ContactListPage");
    }

    [RelayCommand]
    private async Task NavigateToUpdateContact()
    {
        await Shell.Current.GoToAsync("//ContactUpdatePage");
    }

    [RelayCommand]
    private async Task NavigateToDeleteContact()
    {
        await Shell.Current.GoToAsync("//ContactDeletePage");
    }

    [RelayCommand]
    private async Task NavigateBack()
    {
        await Shell.Current.GoToAsync(".."); // Changed from ContactListPage to .. as nav wouldnt work otherwise.
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
}
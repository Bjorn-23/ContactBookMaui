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

    [ObservableProperty]
    private PContact _registrationForm = new();

    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

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

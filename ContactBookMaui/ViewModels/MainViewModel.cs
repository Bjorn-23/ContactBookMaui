using CommunityToolkit.Mvvm.ComponentModel;
using ContactBook_Shared.Models;
using ContactBook_Shared.Interfaces;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;


namespace ContactBookMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    public MainViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
        UpdateCustomerList();
    }    

    /// <summary>
    /// new PContact Entry
    /// </summary>
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
                UpdateCustomerList();
                RegistrationForm = new();
            }
        }
    }

    [RelayCommand]
    public void RemoveCustomerButton(IPContact contactToDelete)
    {
        if (contactToDelete != null)
        {
            var result = _contactRepository.DeleteContactByEmail(contactToDelete);
            if (result)
            {
                UpdateCustomerList();
            }
        }
    }

    [RelayCommand]
    private async Task NavigateToAddContact()
    {
        await Shell.Current.GoToAsync("ContactAddPage");
    }
    
    [RelayCommand]
    private async Task NavigateToListAllContact()
    {
        await Shell.Current.GoToAsync("ContactListAllPage");
    }

    [RelayCommand]
    private async Task NavigateToUpdateContact()
    {
        await Shell.Current.GoToAsync("ContactUpdatePage");
    }

    [RelayCommand]
    private async Task NavigateToDeleteContact()
    {
        await Shell.Current.GoToAsync("ContactDeletePage");
    }

    [RelayCommand]
    private async Task NavigateBack()
    {
        await Shell.Current.GoToAsync(".."); // Changed from ContactListPage to .. as nav wouldnt work otherwise.
    }

    public void UpdateCustomerList()
    {
        PContactList = new ObservableCollection<IPContact>(_contactRepository.GetAllContactsFromList().Select(contact => contact).ToList());
          //  Customer.Select(customer => customer).ToList());
    }
}

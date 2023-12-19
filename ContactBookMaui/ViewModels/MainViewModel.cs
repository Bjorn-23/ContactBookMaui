using CommunityToolkit.Mvvm.ComponentModel;
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
    private ObservableCollection<IPContact> _pContactList = [];

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
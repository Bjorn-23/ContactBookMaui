using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ContactBookMaui.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly IContactRepository _contactRepository;

    public ListViewModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;

        _contactRepository

        //UpdateContactList();

    }

    

    [ObservableProperty]
    private ObservableCollection<IPContact> _pContactList = [];

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
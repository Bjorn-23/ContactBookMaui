using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContactBook_Shared.Interfaces;

public interface IPContactServices
{
    public ObservableCollection<IPContact> GetAllContactsFromList();
    public bool AddContactToList(PContact contact);
    public ObservableCollection<IPContact> GetContactFromListByEmail(IPContact contact);
    public bool UpdateContactToListByEmail(IPContact contactToUpdate, PContact updatedContactDetails);
    public bool DeleteContactByEmail(IPContact contactToDelete);

    public event EventHandler? PContactListUpdated;

}
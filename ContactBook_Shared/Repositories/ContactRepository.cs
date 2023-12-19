using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Diagnostics;


namespace ContactBook_Shared.Repositories;

public class ContactRepository : IContactRepository
{
    public ContactRepository()
    {
    }

    private List<IPContact> _pContactList = [];

    private readonly IFileServices? _fileService;

    private readonly string _filePath = (@"d:\projectFiles\Contacts.json");

    public event EventHandler? PContactListUpdated;

    public ContactRepository(IFileServices fileServices)
    {
        _fileService = fileServices;
        _pContactList = (List<IPContact>)_fileService.GetFile(_filePath);
    }

    public IEnumerable<IPContact> GetAllContactsFromList()
    {
        if (_fileService != null)
        {
            _fileService.GetFile(_filePath);
            return _pContactList;
        }
        return null!;
    }

    public bool AddContactToList(PContact contact)
    {
        try
        {
            if (_fileService != null && contact.Email != "" && contact.FirstName != "")
            {
                if (!_pContactList.Any(x => x.Email == contact.Email))
                {
                    _pContactList.Add(contact);
                    _fileService.WriteToFile(_pContactList, _filePath);
                    PContactListUpdated?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
        
    public List<IPContact> GetContactFromListByEmail(IPContact contact)
    {
        try
        {
            if (contact.Email != null)
            {
                foreach (var existingContact in _pContactList)
                {
                    if (existingContact.Email.Equals(contact.Email, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new List<IPContact> { existingContact };
                    }
                }
            }
            else
                //return new List<IPContact>();
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        //return null!;
        return [];
    }

    public bool UpdateContactToListByEmail(IPContact contactToUpdate, PContact updatedContactDetails)
    {
        try
        {
            if (_fileService != null && updatedContactDetails.Email != "" && updatedContactDetails.FirstName != "")
            {
                contactToUpdate = updatedContactDetails;
                bool result = _fileService.WriteToFile(_pContactList, _filePath);
                PContactListUpdated?.Invoke(this, EventArgs.Empty);

                if (result)
                {
                    return true;
                }

                //var res1 = DeleteContactByEmail(contactToUpdate);
                //var res2 = AddContactToList(updatedContactDetails);
                //PContactListUpdated?.Invoke(this, EventArgs.Empty);

                //if (res1 && res2)
                //{
                //    return true;
                //}
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public bool DeleteContactByEmail(IPContact contactToDelete)
    {
        try
        {
            if (_fileService != null && _pContactList.Any(x => x.Email == contactToDelete.Email))
            {
                _pContactList.Remove(contactToDelete);
                var result = _fileService.WriteToFile(_pContactList, _filePath);
                PContactListUpdated?.Invoke(this, EventArgs.Empty);
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
}

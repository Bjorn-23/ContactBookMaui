using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using System.Diagnostics;


namespace ContactBook_Shared.Repositories;

public class ContactRepository : IContactRepository
{
    public ContactRepository()
    {
    }

    private List<IPContact> _contactList = [];

    private readonly IFileServices? _fileService;

    private readonly string _filePath = (@"d:\projectFiles\Contacts.json");
    public ContactRepository(IFileServices fileServices)
    {
        _fileService = fileServices;
        _contactList = (List<IPContact>)_fileService.GetFile(_filePath);
    }

    public IEnumerable<IPContact> GetAllContactsFromList()
    {
        if (_fileService != null)
        {
            _fileService.GetFile(_filePath);
            return _contactList;
        }
        return null!;
    }

    public bool AddContactToList(PContact contact)
    {
        try
        {
            if (_fileService != null && contact.Email != "" && contact.FirstName != "")
            {
                if (!_contactList.Any(x => x.Email == contact.Email))
                {
                    _contactList.Add(contact);
                    _fileService.WriteToFile(_contactList, _filePath);
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
                foreach (var existingContact in _contactList)
                {
                    if (existingContact.Email.Equals(contact.Email, StringComparison.CurrentCultureIgnoreCase))   // if (existingContact.Email.ToLower() == contact.Email.ToLower())
                    {
                        return new List<IPContact> { existingContact };
                    }
                }
            }
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        //return null!;
        return [];
    }

    public bool UpdateContactToListByEmail(IPContact contactToDelete, PContact updatedContact)
    {
        try
        {
            if (updatedContact.Email != "" && updatedContact.FirstName != "")
            {
                var res1 = DeleteContactByEmail(contactToDelete);
                var res2 = AddContactToList(updatedContact);

                if (res1 && res2)
                {
                    return true;
                }
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
            if (_fileService != null && _contactList.Any(x => x.Email == contactToDelete.Email))
            {
                _contactList.Remove(contactToDelete);
                var result = _fileService.WriteToFile(_contactList, _filePath);
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

using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ContactBook_Shared.Services;

public class PContactServices : IPContactServices
{
    public PContactServices()
    {
    }

    private ObservableCollection<IPContact> _pContactList = [];

    private ObservableCollection<IPContact> _newList = [];

    private readonly IFileRepository? _fileRepository;

    //File directories where you might find the list on disk:
    //C:\Users\bjorn\AppData\Local\Contacts - ConsoleApp
    //C:\Users\bjorn\AppData\Local\Packages\com.companyname.contactbookmaui_9zz4h110yvjzm\LocalCache\Local\Contacts.json
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contacts.json");

    public event EventHandler? PContactListUpdated;

    public PContactServices(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;

        _pContactList = GetAllContactsFromList();
    }

    public ObservableCollection<IPContact> GetAllContactsFromList()
    {
        try
        {
            if (_fileRepository != null)
            {
                var data = _fileRepository.GetFile(_filePath);
                var _pContactList = DeserializeObject(data);
                return _pContactList;
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public bool AddContactToList(PContact contact)
    {
        try
        {            
            if (!string.IsNullOrEmpty(contact.Email.ToString()) && !string.IsNullOrEmpty(contact.FirstName.ToString()))
            {
                if (!_pContactList.Any(x => x.Email == contact.Email))
                {
                    _pContactList.Add(contact);
                    var pContactListUpdated = SerializeObject(_pContactList);
                    var result =  _fileRepository!.WriteToFile(pContactListUpdated, _filePath);
                    
                    if (result)
                    {
                        PContactListUpdated?.Invoke(this, EventArgs.Empty);
                        return true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public ObservableCollection<IPContact> GetContactFromListByEmail(IPContact contact)
    {
        try
        {
            if (!string.IsNullOrEmpty(contact.Email.ToString()))
            {
                foreach (var existingContact in _pContactList)
                {
                    if (existingContact.Email.Equals(contact.Email, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new ObservableCollection<IPContact> { existingContact };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public bool UpdateContactToListByEmail(IPContact contactToUpdate, PContact updatedContactDetails)
    {
        try
        {
            if (!string.IsNullOrEmpty(updatedContactDetails.Email.ToString()) && !string.IsNullOrEmpty(updatedContactDetails.FirstName.ToString()))
            {
                contactToUpdate = updatedContactDetails;
                int index = _pContactList.IndexOf(_pContactList.FirstOrDefault(c => c.Email == contactToUpdate.Email)!);

                if (index >= 0)
                {
                    _pContactList[index] = updatedContactDetails;
                    var pContactListUpdated = SerializeObject(_pContactList);
                    bool result = _fileRepository!.WriteToFile(pContactListUpdated, _filePath);

                    if (result)
                    {
                        PContactListUpdated?.Invoke(this, EventArgs.Empty);
                        return true;
                    }
                }
            }
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
            _pContactList.Remove(contactToDelete);
            var pContactListUpdated = SerializeObject(_pContactList);
            var result = _fileRepository!.WriteToFile(pContactListUpdated, _filePath);

            if (result)
            {
                PContactListUpdated?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public string SerializeObject(ObservableCollection<IPContact> _pContactList)
    {
        try
        {
            var data = JsonConvert.SerializeObject(_pContactList, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            });
            return data;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public ObservableCollection<IPContact> DeserializeObject(string data)
    {        
        try
        {
            var _pContactList = JsonConvert.DeserializeObject<ObservableCollection<IPContact>>(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            });

            if (_pContactList != null)
            {
                return _pContactList;
            }
            else
            {
                return _newList;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return _newList;
    }
    
}

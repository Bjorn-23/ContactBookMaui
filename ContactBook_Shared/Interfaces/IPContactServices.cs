using ContactBook_Shared.Models;
using System.Collections.ObjectModel;

namespace ContactBook_Shared.Interfaces;

public interface IPContactServices
{
    /// <summary>
    /// Populates _pContactList from file on disk
    /// </summary>
    /// <returns>ObservableCollection<IPContact></returns>
    public ObservableCollection<IPContact> GetAllContactsFromList();

    /// <summary>
    /// Adds new PContact to _pContactList
    /// </summary>
    /// <param name="contact">New PContact</param>
    /// <returns>True if PContact was added, false if not</returns>
    public bool AddContactToList(PContact contact);

    /// <summary>
    /// Retrieves specific contact from _pContactList from email of user input
    /// </summary>
    /// <param name="contact">Email from user input</param>
    /// <returns>ObservableCollection<IPContact></returns>
    public ObservableCollection<IPContact> GetContactFromListByEmail(IPContact contact);

    /// <summary>
    /// Updates an already present contact in _pContactList
    /// </summary>
    /// <param name="contactToUpdate">Old PContact details</param>
    /// <param name="updatedContactDetails">New PContact details</param>
    /// <returns>True if update was succesful, false if not</returns>
    public bool UpdateContactToListByEmail(IPContact contactToUpdate, PContact updatedContactDetails);

    /// <summary>
    /// Deletes a PContact from _pContactList
    /// </summary>
    /// <param name="contactToDelete">The PContact to remove</param>
    /// <returns>True if PContact was deleted, false if not</returns>
    public bool DeleteContactByEmail(IPContact contactToDelete);

    /// <summary>
    /// Serializes an ObservableCollection of _pContactList to a string of IPContact
    /// </summary>
    /// <param name="_pContactList">Observable Collection of _pContactList</param>
    /// <returns>JSON string of _pContactlist called "data"</returns>
    public string SerializeObject(ObservableCollection<IPContact> _pContactList);

    /// <summary>
    /// Deserializes a string of IPContact to an ObservableCollection of _pContactList
    /// </summary>
    /// <param name="data">The serialized form of a PContact in JSON string</param>
    /// <returns>ObservableCollection of _pContactList</returns>
    public ObservableCollection<IPContact> DeserializeObject(string data);

    /// <summary>
    /// Eventhandler for ContactServices, updates ObservableCollection(s) when _pContalist changes.
    /// </summary>
    public event EventHandler? PContactListUpdated;
}
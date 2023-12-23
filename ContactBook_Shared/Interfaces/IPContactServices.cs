using ContactBook_Shared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
    /// Eventhandler for ContactServices, updates ObservableCollection(s) when _pContalist changes.
    /// </summary>
    public event EventHandler? PContactListUpdated;
}
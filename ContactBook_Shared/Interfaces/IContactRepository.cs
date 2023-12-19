using ContactBook_Shared.Models;

namespace ContactBook_Shared.Interfaces;

public interface IContactRepository
{
    /// <summary>
    /// Gets all Contacts from _pContactList.
    /// </summary>
    /// <returns>IEnumerable of IContact</returns>
    IEnumerable<IPContact> GetAllContactsFromList();

    /// <summary>
    /// Adds new contact to list. _fileService needs to exist, email or user can not be empty string.
    /// </summary>
    /// <param name="contact">The new contact</param>
    /// <returns>True if contact was added, otherwise false.</returns>
    bool AddContactToList(PContact contact);

    /// <summary>
    /// Takes in contact.Email from user input and loops through _pContactList to find a match.
    /// </summary>
    /// <param name="contact">The Email input by user</param>
    /// <returns>If found the contact associated with the email is returned, else null.</returns>
    List<IPContact> GetContactFromListByEmail(IPContact contact);

    /// <summary>
    /// Updates a contact with new details. FirstName and Email can't be empty strings. (contactToUpdate) is replaced with (updatedContactDetails).
    /// </summary>
    /// <param name="contactToUpdate">Old contact details</param>
    /// <param name="updatedContactDetails">New contact details</param>
    /// <returns>True if _contactList can be written to _fileService. Else false.</returns>
    bool UpdateContactToListByEmail(IPContact contactToUpdate, PContact updatedContactDetails);

    /// <summary>
    /// Removes a contact from the _pContactList and writes the new _pContactList to _fileService List
    /// </summary>
    /// <param name="contactToDelete"></param>
    /// <returns>True or False depending on if the contact could be deleted or not and also false if the try doesnt work.</returns>
    bool DeleteContactByEmail(IPContact contactToDelete);

    /// <summary>
    /// Eventhandler for methods in ContactRepository to update PContact on changes to the list.
    /// </summary>
    public event EventHandler? PContactListUpdated;
}
using ContactBook_Shared.Models;

namespace ContactBook_Shared.Interfaces;

public interface IContactRepository
{
    /// <summary>
    /// Gets all Contacts from _contactList.
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
    /// Takes in contact.Email from user input and loops through _contactList to find a match.
    /// </summary>
    /// <param name="contact">The Email input by user</param>
    /// <returns>If found the contact associated with the email is returned, else null.</returns>
    List<IPContact> GetContactFromListByEmail(IPContact contact);

    /// <summary>
    /// Updates a contact with new details. FirstName and Email can't be empty strings. "contactToDelete" is deleted first, then "updatedContact" is added so the same email can be used.
    /// </summary>
    /// <param name="contactToDelete">New contact details</param>
    /// <param name="updatedContact">Old contact details</param>
    /// <returns>True if delete and add contact were succesful. Else false.</returns>
    bool UpdateContactToListByEmail(IPContact contactToDelete, PContact updatedContact);

    /// <summary>
    /// Removes a contact from the _contactList and wirtes the new _contactList to _fileService List
    /// </summary>
    /// <param name="contactToDelete"></param>
    /// <returns>True or False depending on if the contact could be deleted or not and also false if the try doesnt work.</returns>
    bool DeleteContactByEmail(IPContact contact);

    /// <summary>
    /// USed by MainViewModel To remove a contact with Button on list.
    /// </summary>
    /// <param name="contactToDelete"></param>
    /// <returns></returns>
    //bool RemoveCustomerFromList(IPContact contactToDelete);

    //
    public event EventHandler? PContactListUpdated;
}
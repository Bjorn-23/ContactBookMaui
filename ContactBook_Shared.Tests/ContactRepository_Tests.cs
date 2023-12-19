
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using ContactBook_Shared.Repositories;
using ContactBook_Shared.Services;
using Moq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;

namespace AddressBookOne.Tests;

public class ContactRepository_Tests
{
    [Fact]
    public void GetAllContactsFromList_ShouldCallOnGetFile_ThenReturnsIEnumerable()
    {
        //Arrange

        var fileServicesMock = new Mock<IFileServices>();

        var expectedContacts = new List<IPContact>
        {
            new Mock<IPContact>().Object,
            new Mock<IPContact>().Object,
            // Add more mock contacts as needed
        };

        fileServicesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(expectedContacts);

        var contactRepository = new ContactRepository(fileServicesMock.Object);

        //Act
        var result = contactRepository.GetAllContactsFromList();

        //Assert
        Assert.NotNull(((IEnumerable<IPContact>)result));
        Assert.Equal(expectedContacts, result);
    }

    [Fact]
    public void AddToContactToList_AddsOneCustomerToList_ThenReturnsTrue()
    {
        //Arrange
        PContact contact = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = RandomNumberGenerator.Create().ToString()!,
            PhoneNumber = "0798654321",
            Address = "Storgatan 1, 263 33, Storstan"
        };

        var fileServicesMock = new Mock<IFileServices>();

        fileServicesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(new List<IPContact>());

        var contactRepository = new ContactRepository(fileServicesMock.Object);

        //Act
        bool result = contactRepository.AddContactToList(contact);

        //Assert
        Assert.True(result);
        Assert.NotNull(contact);
    }


    [Fact]
    public void GetContactFromListByEmail_ShouldGetOneContact_AndReturnNewList()
    {
        //Arrange

        PContact contact = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = RandomNumberGenerator.Create().ToString()!,
            PhoneNumber = "0798654321",
            Address = "Storgatan 1, 263 33, Storstan"
        };

        List<IPContact> pContactList = new List<IPContact>
        {
            new PContact { Email = "test1@example.com" },
            contact,
            new PContact { Email = "test2@example.com" },
            // Add more contacts as needed
        };

        // Create the FileServices with Mock
        var fileServicesMock = new Mock<IFileServices>();

        // Set up the mock file service to return the expected contacts
        fileServicesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(pContactList);

        // Create the ContactRepository with the mock file service
        var contactRepository = new ContactRepository(fileServicesMock.Object);

        //Act
        var result = contactRepository.GetContactFromListByEmail(contact);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(contact.Email, result.FirstOrDefault()?.Email);
    }


}

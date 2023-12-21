
using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using ContactBook_Shared.Repositories;
using ContactBook_Shared.Services;
using Microsoft.Maui.ApplicationModel.Communication;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;

namespace AddressBookOne.Tests;

public class ContactRepository_Tests
{
    [Fact]
    public void GetAllContactsFromList_ShouldCallOnGetFile_ThenReturnsIEnumerable()
    {
        //Arrange

        var fileRepositoriesMock = new Mock<IFileRepository>();

        fileRepositoriesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(It.IsAny<string>);

        ObservableCollection<IPContact> expectedResult = new();

        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        //Act
        var result = pContactServices.GetAllContactsFromList();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
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

        var fileRepositoriesMock = new Mock<IFileRepository>();

        fileRepositoriesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(It.IsAny<string>());

        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        //Act
        bool result = pContactServices.AddContactToList(contact);

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

        // Create a pContactList to use with mock
        List<IPContact> pContactList = new List<IPContact>
        {
            new PContact { Email = "test1@example.com" },
            contact,
            new PContact { Email = "test2@example.com" },
        };

        // Create the FileServices with mock
        var fileRepositoriesMock = new Mock<IFileRepository>();

        // Set up the mock file service to give access to PContactList
        fileRepositoriesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(It.IsAny<string>);

        // Create the ContactRepository with the mock file service
        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        //Act
        var result = pContactServices.GetContactFromListByEmail(contact);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(contact.Email, result.FirstOrDefault()?.Email);
    }

    [Fact]
    public void UpdateContactToListByEmail_ShouldReplaceContactToUpdateWithUpdatedContact_AndThenReturnTrue()
    {
        //Arrange

        // Create old contact to be updated
        IPContact contactToUpdate = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = RandomNumberGenerator.Create().ToString()!,
            PhoneNumber = "0798654321",
            Address = "Storgatan 1, 263 33, Storstan"
        };

        // Create a contact to update old contact with
        PContact updatedContactDetails = new()
        {
            FirstName = "Aimee",
            LastName = "Andersson",
            Email = RandomNumberGenerator.Create().ToString()!,
            PhoneNumber = "0798321654",
            Address = "Storgatan 1, 263 33, Storstan"
        };

        // Create the FileServices with mock
        var fileRepositoriesMock = new Mock<IFileRepository>();

        // Sets writeToFile to use pContactList and return true
        fileRepositoriesMock.Setup(fs => fs.WriteToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        // Create the ContactRepository with the mock file service
        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        // Act
        var result = pContactServices.UpdateContactToListByEmail(contactToUpdate, updatedContactDetails);

        //Assert
        Assert.True(result); //_fileService.WriteToFile keeps returning false or res1 will return false, also when writing list to file. CURRENTLY RETURNS FALSE
        Assert.Equal(contactToUpdate.Email, updatedContactDetails.Email);
    }

    [Fact]
    public void DeleteContactByEmail_ShouldRemoveContactFromList_ThenWriteListToFile_AndReturnTrue()
    {
        //Arrange
        var contactToDelete = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = RandomNumberGenerator.Create().ToString()!,
            PhoneNumber = "0798654321",
            Address = "Storgatan 1, 263 33, Storstan"
        };

        var fileRepositoriesMock = new Mock<IFileRepository>();

        fileRepositoriesMock.Setup(fs => fs.WriteToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        //Act
        var result = pContactServices.DeleteContactByEmail(contactToDelete);

        //Assert
        Assert.True(result);
    }
}

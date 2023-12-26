
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AddressBookOne.Tests;

public class ContactRepository_Tests
{

    //Tests should be setup in order of 1. FileRepository 2. ContactServices to be able to work.

    [Fact]
    public void GetAllContactsFromList_ShouldCallOnGetFile_ThenReturnsIEnumerable()
    {
        //Arrange

        // Create the FileRepository with mock
        var fileRepositoriesMock = new Mock<IFileRepository>();

        // Setup Getfile to return string of data
        fileRepositoriesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(It.IsAny<string>);

        // Creates an empty observable collection to compare to the return type of GetAllContactsFromList()
        ObservableCollection<IPContact> expectedResult = new();

        //Create the ContactRepository with the mock file service
        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        //Act
        var result = pContactServices.GetAllContactsFromList();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void AddContactToList_AddsOneCustomerToList_ThenReturnsTrue()
    {
        //Arrange
        PContact contact = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = "bjorn@mail.com",
            PhoneNumber = "0798654321",
            Address = "Plöjargränd 143"
        };

        // Create the FileRepository with mock
        var fileRepositoriesMock = new Mock<IFileRepository>();
        
        //Create the ContactRepository with the mock file service
        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        // Create a string of data to return from GetFile that matches contact
        string data = "[{ \"$type\":\"ContactBook_Shared.Models.PContact, ContactBook_Shared\",\"FirstName\":\"Björn\",\"LastName\":\"Andersson\",\"Email\":\"bjorn@mail.com\",\"PhoneNumber\":\"0798654321\",\"Address\":\"Plöjargränd 143\",\"FullName\":\"Björn Andersson\"}]"; //",\"FullName\":\"Björn Andersson\

        // Setup Getfile to return string of data for comparison with contact.
        fileRepositoriesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(data);

        // Setup WriteToFile to return true
        fileRepositoriesMock.Setup(fs => fs.WriteToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        //Act
        bool result = pContactServices.AddContactToList(contact);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void GetContactFromListByEmail_ShouldGetOneContact_AndReturnNewList()
    {
        //Arrange

        // Create a contact to get from list
        PContact contact = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = "bjorn@mail.com",
            PhoneNumber = "0798654321",
            Address = "Plöjargränd 143"
        };

        // Create the FileRepository with mock
        var fileRepositoriesMock = new Mock<IFileRepository>();

        // Create a string of data to return from GetFile that matches contact
        string data = "[{ \"$type\":\"ContactBook_Shared.Models.PContact, ContactBook_Shared\",\"FirstName\":\"Björn\",\"LastName\":\"Andersson\",\"Email\":\"bjorn@mail.com\",\"PhoneNumber\":\"0798654321\",\"Address\":\"Plöjargränd 143\"}]"; //",\"FullName\":\"Björn Andersson\

        // Setup Getfile to return string of data for comparison with contact.
        fileRepositoriesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(data);

        //Create the ContactRepository with the mock file service
        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        // Act
        var result = pContactServices.GetContactFromListByEmail(contact);

        // Assert
        Assert.NotNull(result);
        Assert.StrictEqual(1, result.Count);
        Assert.Equal(contact.Email, result.FirstOrDefault()?.Email);
    }

    [Fact]
    public void UpdateContactToListByEmail_ShouldReplaceContactToUpdateWithUpdatedContact_AndThenReturnTrue()
    {
        //Arrange
        // Create contact to be updated
        PContact contactToUpdate = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = "bjorn@mail.com",
            PhoneNumber = "0798654321",
            Address = "Plöjargränd 143"
        };

        // Create new contact details
        PContact updatedContactDetails = new()
        {
            FirstName = "Aimee",
            LastName = "Andersson",
            Email = "aimee@mail.com",
            PhoneNumber = "0798321654",
            Address = "Plöjargränd 143"
        };

        // Create the FileRepository with mock
        var fileRepositoriesMock = new Mock<IFileRepository>();

        // Create a string of data to return from GetFile that matches contact (serialized array of objects in JSON format?)
        string data = "[{ \"$type\":\"ContactBook_Shared.Models.PContact, ContactBook_Shared\",\"FirstName\":\"Björn\",\"LastName\":\"Andersson\",\"Email\":\"bjorn@mail.com\",\"PhoneNumber\":\"0798654321\",\"Address\":\"Plöjargränd 143\"}]";

        // Create a string of data to return from GetFile that matches contact
        fileRepositoriesMock.Setup(fs => fs.GetFile(It.IsAny<string>())).Returns(data);

        // Setup WriteToFile to return true
        fileRepositoriesMock.Setup(fs => fs.WriteToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        // Create the ContactRepository with the mock file service
        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        // Act
        var result = pContactServices.UpdateContactToListByEmail(contactToUpdate, updatedContactDetails);

        //Assert        
        Assert.True(result);
    }

    [Fact]
    public void DeleteContactByEmail_ShouldRemoveContactFromList_ThenWriteListToFile_AndReturnTrue()
    {
        //Arrange
        // Create contact to be deleted
        var contactToDelete = new PContact
        {
            FirstName = "Björn",
            LastName = "Andersson",
            Email = RandomNumberGenerator.Create().ToString()!,
            PhoneNumber = "0798654321",
            Address = "Storgatan 1, 263 33, Storstan"
        };

        // Create the FileRepository with mock
        var fileRepositoriesMock = new Mock<IFileRepository>();

        // Create the ContactRepository with the mock file service
        var pContactServices = new PContactServices(fileRepositoriesMock.Object);

        // Setup WriteToFile to return true
        fileRepositoriesMock.Setup(fs => fs.WriteToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        //Act
        var result = pContactServices.DeleteContactByEmail(contactToDelete);

        //Assert
        Assert.True(result);
    }
}

using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactService = new ContactService(_fileServiceMock.Object);
    }
    
    [Fact]
    public void AddContact_ShouldReturnTrue_WhenContactSuccessfullyCreated()
    {
        // Arrange
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);
        var contact = new Contact();
        
        // Act
        var result = _contactService.AddContact(contact);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void AddContact_ShouldReturnFalse_WhenContactCreationFails()
    {
        // Arrange
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(false);
        var contact = new Contact();
        
        // Act
        var result = _contactService.AddContact(contact);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void GetAllContacts_ShouldReturnContacts_WhenContentIsRetrievedFromFileSuccessfully()
    {
        // Arrange
        // This string(The mockedJsonFileContent value) simulates the contents of a file that the ContactService would read using its PopulateContentFromFile method.
        
        var mockedJsonFileContent =
            "[{\"FirstName\":\"Sara\", \"LastName\":\"Andersson\",\"Email\":\"sara@gmail.com\",\"Phone\":\"0729876543\"," +
            "\"StreetAddress\":\"Gäddan 4\",\"PostalCode\":\"212 14\",\"City\":\"Stockholm\"}, " +
            "{\"FirstName\":\"Nina\", \"LastName\":\"Larsson\", \"Email\":\"nina@gmail.com\",\"Phone\":\"0765439876\"," +
            "\"StreetAddress\":\"Fregattgatan 2\",\"PostalCode\":\"211 13\",\"City\":\"Malmö\"}]";

        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(mockedJsonFileContent);

        // Each Contact object inside the list is just an instance of the Contact class.
        // This list is used in the test to represent the expected data structure.
        // The expectedContacts list is compared to the result of GetAllContacts,
        // which also returns a List<Contact> after deserializing the mocked JSON file content.
        
        var expectedContacts = new List<Contact>
        {
            new Contact
            {
                FirstName = "Sara", LastName = "Andersson", Email = "sara@gmail.com", Phone = "0729876543",
                StreetAddress = "Gäddan 4", PostalCode = "212 14", City = "Stockholm"
            },
            new Contact
            {
                FirstName = "Nina", LastName = "Larsson", Email = "nina@gmail.com", Phone = "0765439876",
                StreetAddress = "Fregattgatan 2", PostalCode = "211 13", City = "Malmö"
            },
        };
        
        // Act
        var result = _contactService.GetAllContacts();
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedContacts.Count, result.Count());
        
        Assert.Equal(expectedContacts.First().FirstName, result.First().FirstName);
        Assert.Equal(expectedContacts.First().LastName, result.First().LastName);
        Assert.Equal(expectedContacts.First().Email, result.First().Email);
        Assert.Equal(expectedContacts.First().Phone, result.First().Phone);
        Assert.Equal(expectedContacts.First().StreetAddress, result.First().StreetAddress);
        Assert.Equal(expectedContacts.First().PostalCode, result.First().PostalCode);
        Assert.Equal(expectedContacts.First().City, result.First().City);
        
        Assert.Equal(expectedContacts.Last().FirstName, result.Last().FirstName);
        Assert.Equal(expectedContacts.Last().LastName, result.Last().LastName);
        Assert.Equal(expectedContacts.Last().Email, result.Last().Email);
        Assert.Equal(expectedContacts.Last().Phone, result.Last().Phone);
        Assert.Equal(expectedContacts.Last().StreetAddress, result.Last().StreetAddress);
        Assert.Equal(expectedContacts.Last().PostalCode, result.Last().PostalCode);
        Assert.Equal(expectedContacts.Last().City, result.Last().City);
    }

    [Fact]
    public void GetAllContacts_ShouldReturnEmptyList_WhenContentIsEmpty()
    {
        // Arrange
        // This string(The mockedJsonFileContent value) simulates the contents of a file that the ContactService would read using its PopulateContentFromFile method.
        
        var mockedJsonFileContent = "";

        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(mockedJsonFileContent);
        
        // Act
        var result = _contactService.GetAllContacts();
        
        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetAllContacts_ShouldReturnEmptyList_WhenContentIsNull()
    {
        // Arrange

        _fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns((string) null);
        
        // Act
        var result = _contactService.GetAllContacts();
        
        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void UpdateContacts_ShouldReturnTrue_WhenContactExists()
    {
        // Arrange

        var contact = new Contact()
        {
            Id = "1",
            FirstName = "Sara",
        };
        
        _contactService.AddContact(contact);
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);
        
        // Act
        var updatedContact = new Contact()
        {
            Id = "1",
            FirstName = "Mona",
        };
        
        var result = _contactService.UpdateContact(updatedContact);
        
        // Assert
        Assert.True(result);
        Assert.Equal("Mona", _contactService.GetAllContacts().First().FirstName);
        
        // This should be two because we also call the method when saved the contact initially
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Exactly(2));
    }
    
    [Fact]
    public void UpdateContacts_ShouldReturnFalse_WhenContactDoesNotExists()
    {
        // Arrange

        // Act
        var updatedContact = new Contact()
        {
            Id = "1",
            FirstName = "Mona",
        };
        
        var result = _contactService.UpdateContact(updatedContact);
        
        // Assert
        Assert.False(result);
        
        // Assert it never calls the file service
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Never);
    }
    
    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenContactExists()
    {
        // Arrange

        var contact = new Contact()
        {
            Id = "1",
            FirstName = "Sara",
        };
        
        _contactService.AddContact(contact);
        _fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>())).Returns(true);
        
        // Act
        
        var result = _contactService.DeleteContact(contact.Id);
        
        // Assert
        Assert.True(result);
        Assert.Empty(_contactService.GetAllContacts());
        
        // This should be two because we also call the method when saved the contact initially
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Exactly(2));
    }
    
    [Fact]
    public void DeleteContact_ShouldReturnFalse_WhenContactDoesNotExist()
    {
        // Arrange
        
        // Act
        
        var result = _contactService.DeleteContact("1");
        
        // Assert
        Assert.False(result); ;
        
        // This should be two because we also call the method when saved the contact initially
        _fileServiceMock.Verify(fs => fs.SaveContentToFile(It.IsAny<string>()), Times.Never);
    }
}
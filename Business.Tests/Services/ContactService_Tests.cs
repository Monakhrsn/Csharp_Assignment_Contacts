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
}
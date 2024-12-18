using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    
    public void Create_ShouldReturnContact()
    {
        // Act
        var result = ContactFactory.Create();
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
    }
}
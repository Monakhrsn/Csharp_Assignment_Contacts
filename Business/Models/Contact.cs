namespace Business.Models;

public class Contact
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string StreetAddress { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string DisplayName => $"{FirstName} {LastName}";
    public string DisplayEmail => $"{Email}";
    public string DisplayPhone => $"{Phone}";
    public string DisplayAddress => $"{StreetAddress}, {PostalCode}, {City}";




}
using System.Text.Json;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IFileService fileService) : IContactService
{
    private readonly IFileService _fileService = fileService;
    private List<Contact> _contacts = [];
    
    public bool AddContact(Contact contact)
    {
        _contacts.Add(contact);
        return SaveContactsToList();
    }
    
    public IEnumerable<Contact> GetAllContacts()
    {
        PopulateContentFromFile();
        return _contacts;
    }

    public bool UpdateContact(Contact updatedContact)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == updatedContact.Id);

        
        if (contact == null)
            return false;
        
        contact.FirstName = updatedContact.FirstName;
        contact.LastName = updatedContact.LastName;
        contact.Email = updatedContact.Email;
        contact.Phone = updatedContact.Phone;
        contact.StreetAddress = updatedContact.StreetAddress;
        contact.PostalCode = updatedContact.PostalCode;
        contact.City = updatedContact.City;
        
        return SaveContactsToList();
    }

    public bool DeleteContact(string contactId)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == contactId);

        if (contact == null)
            return false;
        
        _contacts.Remove(contact);
        
        return SaveContactsToList();
    }

    private bool SaveContactsToList()
    {
        var jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(_contacts, jsonSerializerOptions);
        return _fileService.SaveContentToFile(json);
    }

    private void PopulateContentFromFile()
    {
        var json = _fileService.GetContentFromFile();
        if (!string.IsNullOrEmpty(json))
        {
            _contacts = JsonSerializer.Deserialize<List<Contact>>(json)!;
        }
    }
    
}



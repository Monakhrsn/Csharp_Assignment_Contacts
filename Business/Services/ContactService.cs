using System.Text.Json;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IFileService fileService) : IContactService
{
    private readonly IFileService _fileService = fileService;
    private List<Contact> _contacts;
    
    public bool AddContact(Contact contact)
    {
        _contacts.Add(contact);
        SaveContactsToList();
        
        return true;
    }
    
    public IEnumerable<Contact> GetAllContacts()
    {
        PopulateContentFromFile();
        return _contacts;
    }

    public void SaveContactsToList()
    {
        var jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(_contacts, jsonSerializerOptions);
        _fileService.SaveContentToFile(json);
    }

    public void PopulateContentFromFile()
    {
        var json = _fileService.GetContentFromFile();
        if (!string.IsNullOrEmpty(json))
        {
            _contacts = JsonSerializer.Deserialize<List<Contact>>(json)!;
        }
    }
}



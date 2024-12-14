using System.Text.Json;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IFileService fileService) : IContactService
{
    private readonly IFileService _fileService = fileService;
    private List<Contact> _contacts = [];
    
    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
        SaveContactsToList();
    }
    
    public IEnumerable<Contact> GetAllContacts()
    {
        return _contacts;
    }

    public void SaveContactsToList()
    {
        var json = JsonSerializer.Serialize(_contacts);
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



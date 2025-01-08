using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool AddContact(Contact contact);
    IEnumerable<Contact> GetAllContacts();
    
    void UpdateContact(Contact updatedContact);
    
}
using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool AddContact(Contact contact);
    IEnumerable<Contact> GetAllContacts();
    bool UpdateContact(Contact? updatedContact);
    bool DeleteContact(string contactId);
}
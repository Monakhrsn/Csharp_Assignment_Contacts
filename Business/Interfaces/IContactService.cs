using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    void AddContact(Contact contact);
    IEnumerable<Contact> GetAllContacts();
}
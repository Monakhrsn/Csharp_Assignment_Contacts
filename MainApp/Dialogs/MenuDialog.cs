using Business.Factories;
using Business.Interfaces;

namespace MainApp.Dialogs;

public class MenuDialog(IContactService contactService)
{
    private readonly IContactService _contactService = contactService;

    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-------- MAIN MENU --------");
            Console.WriteLine("1) Add New Contact");
            Console.WriteLine("2) View All Contacts");
            Console.WriteLine("Select Option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddNewContact();
                    break;
                case "2":
                    ViewAllContacts();
                    break;
            }
        }
    }

    public void AddNewContact()
    {
        var contact = ContactFactory.Create();
        
        Console.Clear();
        Console.WriteLine("-------- ADD NEW CONTACT --------");
        
        Console.WriteLine("Enter First name: ");
        contact.FirstName = Console.ReadLine()!;
        
        Console.WriteLine("Enter Last name: ");
        contact.LastName = Console.ReadLine()!;
        
        Console.WriteLine("Enter Email: ");
        contact.Email = Console.ReadLine()!;
        
        Console.WriteLine("Enter Phone number: ");
        contact.Phone = Console.ReadLine()!;
        
        Console.WriteLine("Enter Street address: ");
        contact.StreetAddress = Console.ReadLine()!;
        
        Console.WriteLine("Enter Postal code: ");
        contact.PostalCode = Console.ReadLine()!;
        
        Console.WriteLine("Enter City: ");
        contact.City = Console.ReadLine()!;
        
        _contactService.AddContact(contact);
    }

    public void ViewAllContacts()
    {
        Console.WriteLine("-------- VIEW ALL CONTACTS --------");
        Console.WriteLine("1) Add New Contact");

        foreach (var contact in _contactService.GetAllContacts())
        {
            Console.WriteLine($"Contact Id: {contact.Id}");
            Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Phone number: {contact.Phone}");
            Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostalCode}, {contact.City}");
            Console.WriteLine("----------------");
        }
        Console.ReadKey();
    }
}

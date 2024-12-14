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
        
        Console.WriteLine("Enter Contact's First name: ");
        contact.FirstName = Console.ReadLine()!;
        
        Console.WriteLine("Enter Contact's Last name: ");
        contact.LastName = Console.ReadLine()!;
        
        Console.WriteLine("Enter Contact's Email: ");
        contact.Email = Console.ReadLine()!;
        
        Console.WriteLine("Enter Contact's Phone: ");
        contact.Phone = Console.ReadLine()!;
        
        Console.WriteLine("Enter Contact's Address: ");
        contact.Address = Console.ReadLine()!;
        
        _contactService.AddContact(contact);
    }

    public void ViewAllContacts()
    {
        Console.Clear();
        Console.WriteLine("-------- VIEW ALL CONTACT --------");
        Console.WriteLine("1) Add New Contact");

        foreach (var contact in _contactService.GetAllContacts())
        {
            Console.WriteLine($"Contact Id: {contact.Id}");
            Console.WriteLine($"Contact's Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"Contact's Email: {contact.Email}");
            Console.WriteLine($"Contact's Phone: {contact.Phone}");
            Console.WriteLine($"Contact's Address: {contact.Address}");
            Console.WriteLine("");
            
            Console.ReadKey();
        }
    }
}

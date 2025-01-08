using Business.Factories;
using Business.Interfaces;

namespace MainApp.Dialogs;

public class MenuDialog(IContactService contactService)
{
    private bool _initializing = true;
    private readonly IContactService _contactService = contactService;

    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            
            if (_initializing)
            {
                ViewAllContacts();
                _initializing = false;
            }

            
            Console.WriteLine("-------- MAIN MENU --------");
            Console.WriteLine("1) Add New Contact");
            Console.WriteLine("2) View All Contacts");
            Console.WriteLine("3) Edit Contact");
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
                case "3":
                    EditContact();
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
        
        Console.Clear();
        Console.WriteLine("-------- Contact Added Successfully --------");
        Console.ReadLine();
    }

    public void ViewAllContacts()
    {
        var contacts = _contactService.GetAllContacts();
        
        Console.Clear();

        if (contacts.Any())
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("-------- CONTACTS --------");
        }
        else
        {
            Console.WriteLine("-------- No Contacts To View --------");
        }
            

        foreach (var contact in contacts)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine($"Contact Id: {contact.Id}");
            Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Phone number: {contact.Phone}");
            Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostalCode}, {contact.City}");
        }

        if (!_initializing)
            Console.ReadKey();
    }

    public void EditContact()
    { 
        ViewAllContacts();

        var contact = ContactFactory.Edit();
        Console.WriteLine("-------- Edit CONTACT --------");
        
    }
}

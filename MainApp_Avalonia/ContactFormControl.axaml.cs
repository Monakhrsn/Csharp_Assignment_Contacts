using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Business.Interfaces;
using Business.Models;
using MainApp_Avalonia.Services;

namespace MainApp_Avalonia;

public partial class ContactFormControl : UserControl
{
    private readonly IContactService _contactService;
    private readonly IMessageService _messageService;
    private readonly MainWindow _parent;
    private readonly Contact? _contact;
    
    public ContactFormControl(IContactService contactService, IMessageService messageService, MainWindow parent)
    {
        _parent = parent;
        _contactService = contactService;
        _messageService = messageService;
        _contact = null;
        
        InitializeComponent();
    }
    
    public ContactFormControl(IContactService contactService, IMessageService messageService, MainWindow parent, Contact contact)
    {
        _contactService = contactService;
        _messageService = messageService;
        _parent = parent;
        _contact = contact;
        
        InitializeComponent();

        // Pre-fill the form fields with contact data
        NameInput.Text = _contact?.FirstName;
        LastNameInput.Text = _contact?.LastName;
        EmailInput.Text = _contact?.Email;
        PhoneInput.Text = _contact?.Phone;
        StreetAddressInput.Text = _contact?.StreetAddress;
        PostalCodeInput.Text = _contact?.PostalCode;
        CityInput.Text = _contact?.City;
    }

    private void OnUpsertClick(object sender, RoutedEventArgs e)
    {
        
        if (_isFormInvalid())
        {
            _messageService.Show(_parent, "Please fill all fields before adding the contact ");
            return;
        };
        
        if (_contact == null)
            OnAddContact();
        else
            OnEditContact();
    }
    

    private void OnAddContact()
    {
        var contact = new Contact()
        {
            FirstName = NameInput.Text!.Trim(),
            LastName = LastNameInput.Text!.Trim(),
            Email = EmailInput.Text!.Trim(),
            Phone = PhoneInput.Text!.Trim(),
            StreetAddress = StreetAddressInput.Text!.Trim(),
            PostalCode = PostalCodeInput.Text!.Trim(),
            City = CityInput.Text!.Trim(),
        };
           
        // Add the contact to the service
        _contactService.AddContact(contact);
            
        // Clear the input fields
        // NameInput.Text = string.Empty;
        // LastNameInput.Text = string.Empty;
        // EmailInput.Text = string.Empty; 
        // PhoneInput.Text = string.Empty;
        // StreetAddressInput.Text = string.Empty;
        // PostalCodeInput.Text = string.Empty;
        // CityInput.Text = string.Empty;
            
        //Navigate back to the contact list
        _parent.ContentArea.Content = new ContactListControl(_contactService, _messageService, _parent);
        
        _messageService.Show(_parent, "Contact added successfully");
        
    }

    private bool _isFormInvalid()
    {
        if (string.IsNullOrWhiteSpace(NameInput.Text) || 
            string.IsNullOrWhiteSpace(LastNameInput.Text) || 
            string.IsNullOrWhiteSpace(EmailInput.Text) || 
            string.IsNullOrWhiteSpace(PhoneInput.Text)|| 
            string.IsNullOrWhiteSpace(StreetAddressInput.Text) || 
            string.IsNullOrWhiteSpace(PostalCodeInput.Text) || 
            string.IsNullOrWhiteSpace(CityInput.Text))
        {
            return true;
        }

        return false;
    }

    private void OnEditContact()
    {
        var updatedContact = new Contact()
        {
            Id = _contact!.Id,
            FirstName = NameInput.Text!.Trim(),
            LastName = LastNameInput.Text!.Trim(),
            Email = EmailInput.Text!.Trim(),
            Phone = PhoneInput.Text!.Trim(),
            StreetAddress = StreetAddressInput.Text!.Trim(),
            PostalCode = PostalCodeInput.Text!.Trim(),
            City = CityInput.Text!.Trim(),
        };
        
        //Update the service
        _contactService.UpdateContact(updatedContact);
        
        //Navigate back to the contact list
        _parent.ContentArea.Content = new ContactListControl(_contactService, _messageService, _parent);
        
        _messageService.Show(_parent, "Contact updated successfully");
    }
    
    
    private void OnCancelClick(object sender, RoutedEventArgs e)
    {
        _parent.ContentArea.Content = new ContactListControl(_contactService, _messageService, _parent);
    }

}
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

    public Contact? SelectedContact { get; set; }
    
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
    }

    private void OnUpsertClick(object sender, RoutedEventArgs e)
    {
        if (_contact == null)
            OnAddContact();
        else
            OnEditContact();
    }
    

    private void OnAddContact()
    {
        if (string.IsNullOrWhiteSpace(NameInput.Text) || 
            string.IsNullOrWhiteSpace(LastNameInput.Text) || 
            string.IsNullOrWhiteSpace(EmailInput.Text) || 
            string.IsNullOrWhiteSpace(PhoneInput.Text)|| 
            string.IsNullOrWhiteSpace(StreetAddressInput.Text) || 
            string.IsNullOrWhiteSpace(PostalCodeInput.Text) || 
            string.IsNullOrWhiteSpace(CityInput.Text))
        {
            // _messageService.Show(this, "Please fill all fields before adding the contact ");
            return;
        }
            
        var contact = new Contact()
        {
            FirstName = NameInput.Text.Trim(),
            LastName = LastNameInput.Text.Trim(),
            Email = EmailInput.Text.Trim(),
            Phone = PhoneInput.Text.Trim(),
            StreetAddress = StreetAddressInput.Text.Trim(),
            PostalCode = PostalCodeInput.Text.Trim(),
            City = CityInput.Text.Trim(),
        };
           
        // Add the contact to the service
        _contactService.AddContact(contact);
            
        // Clear the input fields
        NameInput.Text = string.Empty;
        LastNameInput.Text = string.Empty;
        EmailInput.Text = string.Empty; 
        PhoneInput.Text = string.Empty;
        StreetAddressInput.Text = string.Empty;
        PostalCodeInput.Text = string.Empty;
        CityInput.Text = string.Empty;
            
        // Hide the form and show the contact list view
        ContactForm.IsVisible = false;
    }
    
    private void OnEditContact()
    {
        if (SelectedContact == null)
        {
            // _messageService.Show(this, "Please select a contact to save");
            return;
    
        }
    
        //Update the selected contact with new details 
        SelectedContact.FirstName = NameInput.Text!.Trim();
        SelectedContact.LastName = LastNameInput.Text!.Trim();
        SelectedContact.Email = EmailInput.Text!.Trim();
        SelectedContact.Phone = PhoneInput.Text!.Trim();
        SelectedContact.StreetAddress = StreetAddressInput.Text!.Trim();
        SelectedContact.PostalCode = PostalCodeInput.Text!.Trim();
        SelectedContact.City = CityInput.Text!.Trim();
        
        //Update the service
        _contactService.UpdateContact(SelectedContact);
        
        //Refresh the UI
        // Items?.Clear();
        // LoadContacts();
        
        //Reset UI
        ContactForm.IsVisible = false;
    }
    
    private void OnCancelClick(object sender, RoutedEventArgs e)
    {
        _parent.ContentArea.Content = new ContactListControl(_contactService, _messageService, _parent);
    }

}
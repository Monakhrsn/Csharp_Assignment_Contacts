using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Authentication;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Business.Interfaces;
using Business.Models;
using MainApp_Avalonia.Services;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using static Avalonia.Controls.DataGridRowDetailsVisibilityMode;
using InvalidOperationException = System.InvalidOperationException;


namespace MainApp_Avalonia;

public partial class MainWindow : Window
{
    private readonly IContactService _contactService;
    private readonly IMessageService _messageService;
    
    // Property for binding
    public ObservableCollection<Contact> Items { get; set; }
    public Contact? SelectedContact { get; set; }
    
    // Chatgpt suggests: Parameterless constructor for Avalonia runtime loader. The parameterless constructor calls
    // the parameterized constructor (MainWindow(IContactService contactService)),
    // passing the required dependency (IContactService) retrieved from the dependency injection container.
    public MainWindow() : this(AppServices.Services?.GetService<IContactService>()
                               ?? throw new InvalidOperationException("Service provider is not initialized."))
    {
        // Delegates to the parameterized constructor
    }
    
        
    // Constructor with dependency injection
    public MainWindow(IContactService contactService)
    {
        _contactService = contactService;
        Items = new ObservableCollection<Contact>(_contactService.GetAllContacts());
        
        // ChatGpt suggestion: Safely retrieve the service with null checks
        var services = AppServices.Services
            ?? throw new InvalidOperationException("Service provider is not initialized.");

        _contactService = services.GetService<IContactService>()
                          ?? throw new InvalidOperationException("IContactService is not registered");
        
        _messageService = services.GetService<IMessageService>()
                          ?? throw new InvalidOperationException("IErrorMessage is not registered");
        
        // Initialize Items collection
        Items = new ObservableCollection<Contact>();
        
        InitializeComponent();
        
        // Set DataContext
        DataContext = this;
        
        // Load contacts
        LoadContacts();
    }

    private void LoadContacts()
    {
        Items.Clear();
        foreach (var contact in _contactService.GetAllContacts())
        {
            Items.Add(contact);
        }
    }
    
    private void OnContactSelected(object? sender, SelectionChangedEventArgs e)
    {
         if (Contacts.SelectedItem is Contact selectedContact)
         {
             // Set the SelectedContact directly without re-querying the entire list
             SelectedContact = selectedContact;
             Console.WriteLine($"Selected contact: {selectedContact}");
         }
    }
    
    private void OnShowList(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            ContactForm.IsVisible = false;
            Contacts.IsVisible = true;
        }
    }
    
    private void OnShowForm(object sender, RoutedEventArgs e)
    {
        if (ContactForm.IsVisible == false)
        {
            ContactForm.IsVisible = true;
            Contacts.IsVisible = false;
        }
    }
    
    private void OnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameInput.Text) || 
            string.IsNullOrWhiteSpace(LastNameInput.Text) || 
            string.IsNullOrWhiteSpace(EmailInput.Text) || 
            string.IsNullOrWhiteSpace(PhoneInput.Text)|| 
            string.IsNullOrWhiteSpace(StreetAddressInput.Text) || 
            string.IsNullOrWhiteSpace(PostalCodeInput.Text) || 
            string.IsNullOrWhiteSpace(CityInput.Text))
        {
            _messageService.Show(this, "Please fill all fields before adding the contact ");
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
        
        // Update the observable collection
        Items.Add(contact);
        
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
        Contacts.IsVisible = true;
    }
    
    private void OnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedContact == null)
        {
           _messageService.Show(this, "Please select a contact to edit"); 
           return;
        }
        
        // Populate the form fields with the selected contact's details
        NameInput.Text = SelectedContact.FirstName;
        LastNameInput.Text = SelectedContact.LastName;
        EmailInput.Text = SelectedContact.Email;
        PhoneInput.Text = SelectedContact.Phone;
        StreetAddressInput.Text = SelectedContact.StreetAddress;
        PostalCodeInput.Text = SelectedContact.PostalCode;
        CityInput.Text = SelectedContact.City;
        
        //Show the form
        ContactForm.IsVisible = true;
        Contacts.IsVisible = false;
    }

    private void OnSave_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedContact == null)
        {
            _messageService.Show(this, "Please select a contact to save");
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
        Items.Clear();
        LoadContacts();
        
        //Reset UI
        ContactForm.IsVisible = false;
        Contacts.IsVisible = true;
    }

    private void OnCancel_Click(object sender, RoutedEventArgs e)
    {
        ContactForm.IsVisible = false;
        Contacts.IsVisible = true;
    }
}
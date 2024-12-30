using System;
using System.Collections.ObjectModel;
using System.Security.Authentication;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Business.Interfaces;
using Business.Models;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using static Avalonia.Controls.DataGridRowDetailsVisibilityMode;



namespace MainApp_Avalonia;

public partial class MainWindow : Window
{
    private readonly IContactService _contactService;
    
    // Property for binding
    public ObservableCollection<string> Items { get; set; }

    public MainWindow()
    {
        // ChatGpt suggestion: Safely retrieve the service with null checks
        var services = AppServices.Services
            ?? throw new InvalidOperationException("Service provider is not initialized.");

        _contactService = services.GetService<IContactService>()
                          ?? throw new InvalidOperationException("IContactService is not registered");
        
        InitializeComponent();

        // Initialize Items collection
        Items = new ObservableCollection<string>();
        LoadContacts();
        
        // Set DataContext
        DataContext = this;
    }

    private void LoadContacts()
    {
        foreach (var contact in _contactService.GetAllContacts())
        {
            Items.Add(($"{contact.FirstName} {contact.LastName}"));
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
        
        // Update the observable collection
        Items.Add($"{contact.FirstName} {contact.LastName}," +
                  $" {contact.Email}, {contact.Phone}, " +
                  $"{contact.StreetAddress}, " +
                  $"{contact.PostalCode}, " +
                  $"{contact.City}");
        
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
}
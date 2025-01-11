using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Business.Interfaces;
using Business.Models;
using MainApp_Avalonia.Services;
using MainApp_Avalonia.ViewModels;

namespace MainApp_Avalonia;

public partial class ContactListControl : UserControl
{
    private readonly MainWindow _parent;
    private readonly IContactService _contactService;
    private readonly IMessageService _messageService;
    public ContactListControl(IContactService contactService, IMessageService messageService, MainWindow parent)
    {
        _parent = parent;
        _contactService = contactService;
        _messageService = messageService;
        
        InitializeComponent();
        
        DataContext = new ContactListViewModel(contactService.GetAllContacts());
    }
    
    private void OnEditClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var contact = button.CommandParameter as Contact;
        _parent.ContentArea.Content = new ContactFormControl(_contactService, _messageService, _parent, contact!);
    }
    
    private void OnDeleteClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var contact = button.CommandParameter as Contact;
        _contactService.DeleteContact(contact.Id);
        DataContext = new ContactListViewModel(_contactService.GetAllContacts());
        _messageService.Show(_parent, "Contact deleted successfully.");
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
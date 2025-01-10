
using Avalonia.Controls;
using Avalonia.Interactivity;
using Business.Interfaces;
using MainApp_Avalonia.Services;

namespace MainApp_Avalonia;

public partial class MainWindow : Window
{
    private readonly IContactService _contactService;
    private readonly IMessageService _messageService;
    
    public MainWindow(IContactService contactService, IMessageService messageService)
    {
        _contactService = contactService;
        _messageService = messageService;
        
        InitializeComponent();
        
        ContentArea.Content = new ContactListControl(_contactService, _messageService, this);
    }
    
    private void OnShowForm(object sender, RoutedEventArgs e)
    {
        ContentArea.Content = new ContactFormControl(_contactService, _messageService, this);
    }
}
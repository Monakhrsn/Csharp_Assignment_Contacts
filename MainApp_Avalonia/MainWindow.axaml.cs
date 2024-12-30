using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using static Avalonia.Controls.DataGridRowDetailsVisibilityMode;

namespace MainApp_Avalonia;

public partial class MainWindow : Window
{
    // Public property for binding
    public ObservableCollection<string> Items { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        // Initialize Items collection
        Items = new ObservableCollection<string>
        {
            "Item 1",
            "Item 22",
            "Item 3333"
        };
        
        // Set DataContext
        DataContext = this;
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
        var name = NameInput.Text;
        var lastName = LastNameInput.Text;
        
        var streetAddress = StreetAddressInput.Text;
        var postalCode = PostalCodeInput.Text;
        var city = CityInput.Text;

        if (!string.IsNullOrEmpty(name) && 
            !string.IsNullOrEmpty(lastName) && 
            !string.IsNullOrEmpty(EmailInput.Text) && 
            !string.IsNullOrEmpty(PhoneInput.Text) && 
            !string.IsNullOrEmpty(StreetAddressInput.Text) && 
            !string.IsNullOrEmpty(PostalCodeInput.Text) && 
            !string.IsNullOrEmpty(CityInput.Text))
        {
            Contacts.Items.Add($"{name} {lastName}");
            Contacts.Items.Add(EmailInput.Text);
            Contacts.Items.Add(PhoneInput.Text);
            Contacts.Items.Add($"{streetAddress}, {postalCode}, {city}");
        }
        
        ContactForm.IsVisible = false;
        Contacts.IsVisible = true;
    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace MainApp_Avalonia;

public partial class CustomMessageBox : Window
{
    // The CustomMessageBox() constructor ensures Avalonia can load this wind
    public CustomMessageBox()
    {
        InitializeComponent();
    }
    // This ensures the window is properly initialized before modifying MessageTextBlock.Text.
    public CustomMessageBox(string message): this()
    {
        MessageTextBlock.Text = message;
    }
    
    private void OnOkButtonClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MainApp_Avalonia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        button.Content = "You clicked me!";
    }
}
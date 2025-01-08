using Avalonia.Controls;

namespace MainApp_Avalonia.Services;

public class MessageService : IMessageService
{
    public void Show(Window owner, string message)
    {
        var messageWindow = new CustomMessageBox(message);
        messageWindow.ShowDialog(owner);
    }
}
using Avalonia.Controls;

namespace MainApp_Avalonia.Services;

public interface IMessageService
{
    public void Show(Window owner, string message);
}
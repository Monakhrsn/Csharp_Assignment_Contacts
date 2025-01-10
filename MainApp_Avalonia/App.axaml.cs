using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Business.Interfaces;
using MainApp_Avalonia.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp_Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            //Chatgpt Suggests
            // To retrieve AppServices.Services, which should be the DI container holding registered services.
            var serviceProvider = AppServices.Services
                                  ?? throw new InvalidOperationException("Service provider is not initialized");
            
            // Resolve IContactService from the service provider. To get an instance of IContactService.
            var contactService = serviceProvider.GetService<IContactService>()
                ?? throw new InvalidOperationException("IContactService is not registered");
            
      
            var messageService = serviceProvider.GetService<IMessageService>()
                              ?? throw new InvalidOperationException("IErrorMessage is not registered");
            
            // Pass the contactService to MainWindow
            desktop.MainWindow = new MainWindow(contactService, messageService);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
using System;
using System.Security.Authentication.ExtendedProtection;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp_Avalonia;

public static class AppServices
{
    // Store the Services provider
    public static IServiceProvider? Services { get; }
    
    // Constructor to initialize Services
    static AppServices()
    {
        Services = new ServiceCollection()
            .AddSingleton<IFileService>(new FileService("Data", "Contacts.json"))
            .AddSingleton<IContactService, ContactService>()
            .BuildServiceProvider();
    }
    public static IServiceProvider RegisterServices()
    {
        if (Services == null)
        {
            // make sure that Services is not null
            throw new InvalidOperationException("Services could not be initialized.");
        }
        return Services;
    }
}
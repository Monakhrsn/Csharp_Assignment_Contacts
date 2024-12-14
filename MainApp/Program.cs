
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using MainApp.Dialogs;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IFileService>(new FileService("Data", "contacts.json"))
    .AddSingleton<IContactService, ContactService>()
    .AddSingleton<MenuDialog>()
    .BuildServiceProvider();
    
    var menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
    menuDialog.MainMenu();
    
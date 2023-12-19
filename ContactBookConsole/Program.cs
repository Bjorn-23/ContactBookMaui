using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Repositories;
using ContactBook_Shared.Services;
using ContactBookConsole.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{

    services.AddSingleton<IContactRepository, ContactRepository>();
    services.AddSingleton<IFileServices, FileServices>();
    services.AddSingleton<IMenuServices, MenuServices>();

}).Build();

builder.Start();
Console.Clear();

//var contactRepository = builder.Services.GetRequiredService<IContactRepository>(); // REMOVE IF THINGS KEEP WORKING WITHOUT:

var menuService = builder.Services.GetRequiredService<IMenuServices>();


/** <summary>
 * Inializes the ContactList
 * </summary>*/
//contactRepository.GetAllContactsFromList(); // REMOVE IF THINGS KEEP WORKING WITHOUT:


/** <summary>
 * Initializes The MenuStart Method from MenuServices
 * </summary>*/
menuService.MenuStart();
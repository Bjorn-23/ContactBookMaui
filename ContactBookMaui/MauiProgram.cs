using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Models;
using ContactBook_Shared.Repositories;
using ContactBook_Shared.Services;
using ContactBookMaui.Pages;
using ContactBookMaui.ViewModels;
using Microsoft.Extensions.Logging;

namespace ContactBookMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IFileServices, FileServices>();
            builder.Services.AddSingleton<IContactRepository, ContactRepository>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<ListViewModel>();
            builder.Services.AddSingleton<AddViewModel>();
            builder.Services.AddSingleton<UpdateViewModel>();
            builder.Services.AddSingleton<DeleteViewModel>();

            builder.Services.AddSingleton<ContactMainPage>();
            builder.Services.AddSingleton<ContactListPage>();
            builder.Services.AddSingleton<ContactAddPage>();
            builder.Services.AddSingleton<ContactUpdatePage>();
            builder.Services.AddSingleton<ContactDeletePage>();

            // Do I have any need for changing these from AddSingleton? Makes no different for testing...
            //builder.Services.AddScoped<ContactMainPage>();
            //builder.Services.AddScoped<ContactListPage>();
            //builder.Services.AddTransient<ContactAddPage>();
            //builder.Services.AddTransient<ContactUpdatePage>();
            //builder.Services.AddTransient<ContactDeletePage>();

            return builder.Build();
        }
    }
}

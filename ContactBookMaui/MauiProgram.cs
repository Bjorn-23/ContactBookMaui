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
                    fonts.AddFont("fa-regular-400.ttf", "FARegular");
                    fonts.AddFont("fa-brands-400.ttf", "FABrands");
                    fonts.AddFont("fa-solid-900.ttf", "FASolid");
                });

            builder.Services.AddSingleton<IFileServices, FileServices>();
            builder.Services.AddSingleton<IContactRepository, ContactRepository>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<ContactMainPage>();

            builder.Services.AddSingleton<ListViewModel>();
            builder.Services.AddSingleton<ContactListPage>();

            builder.Services.AddTransient<AddViewModel>();
            builder.Services.AddTransient<ContactAddPage>();

            builder.Services.AddTransient<UpdateViewModel>();
            builder.Services.AddTransient<ContactUpdatePage>();

            builder.Services.AddTransient<DeleteViewModel>();
            builder.Services.AddTransient<ContactDeletePage>();








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

using ContactBook_Shared.Interfaces;
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
            //builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ContactMainPage>();
            builder.Services.AddSingleton<ContactAddPage>();
            builder.Services.AddSingleton<ContactListPage>();
            builder.Services.AddSingleton<ContactUpdatePage>();
            builder.Services.AddSingleton<ContactDeletePage>();


            return builder.Build();
        }
    }
}

﻿using ContactBook_Shared.Interfaces;
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
            builder.Services.AddScoped<MainPage>(); //Do I keep mainpage? is it needed....?
            builder.Services.AddScoped<ContactMainPage>();
            builder.Services.AddScoped<ContactAddPage>();
            builder.Services.AddScoped<ContactListPage>();
            builder.Services.AddScoped<ContactUpdatePage>();
            builder.Services.AddScoped<ContactDeletePage>();

            return builder.Build();
        }
    }
}

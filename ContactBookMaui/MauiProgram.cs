﻿using ContactBook_Shared.Interfaces;
using ContactBook_Shared.Repositories;
using ContactBook_Shared.Services;
using ContactBookMaui.Pages;
using ContactBookMaui.ViewModels;


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

            builder.Services.AddSingleton<IFileRepository, FileRepository>();
            builder.Services.AddSingleton<IPContactServices, PContactServices>();

            builder.Services.AddSingleton<PContactListViewModel>();
            builder.Services.AddSingleton<ContactListPage>();

            builder.Services.AddTransient<PContactDetailsViewModel>();
            builder.Services.AddTransient<ContactDetailsPage>();

            builder.Services.AddTransient<PContactAddViewModel>();
            builder.Services.AddTransient<ContactAddPage>();

            builder.Services.AddTransient<PContactUpdateViewModel>();
            builder.Services.AddTransient<ContactUpdatePage>();

            builder.Services.AddTransient<PContactDeleteViewModel>();
            builder.Services.AddTransient<ContactDeletePage>();

            return builder.Build();
        }
    }
}

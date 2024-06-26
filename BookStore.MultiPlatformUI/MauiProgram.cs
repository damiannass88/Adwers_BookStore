﻿using BookStore.MultiPlatformUI.Services;
using BookStore.MultiPlatformUI.ViewModels;
using BookStore.MultiPlatformUI.Views;
using Microsoft.Extensions.Logging;

namespace BookStore.MultiPlatformUI
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

			builder.Services.AddSingleton(sp => new HttpClient(new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			})
			{
				BaseAddress = new Uri("https://localhost:7127")
			});

			builder.Services.AddSingleton<BookService>();
			builder.Services.AddSingleton<BooksViewModel>();
			builder.Services.AddTransient<BooksPage>();
#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}

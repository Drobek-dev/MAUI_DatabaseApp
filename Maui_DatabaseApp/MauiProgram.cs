using Maui_DatabaseApp.Services.Database;
using Maui_DatabaseApp.ViewModel;
using Maui_DatabaseApp.View;
using Microsoft.Extensions.Logging;

namespace Maui_DatabaseApp;

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

		builder.Services.AddSingleton<FestivalsMainPageView>(); 
		builder.Services.AddSingleton<FestivalsMainPageVM>();

		builder.Services.AddSingleton<AddFestivalPageView>();
		builder.Services.AddSingleton<AddFestivalPageVM>();

		builder.Services.AddSingleton<AddExternalWorkerPageView>();
		builder.Services.AddTransient<AddExternalWorkerPageVM>();

		builder.Services.AddSingleton<AddEquipmentPageView>();
		builder.Services.AddSingleton<AddEquipmentPageVM>();

		builder.Services.AddTransient<FestivalDetailPageView>();
		builder.Services.AddTransient<FestivalDetailPageVM>();

		builder.Services.AddTransient<ExternalWorkerPageView>();
		builder.Services.AddTransient<ExternalWorkerPageVM>();

        builder.Services.AddTransient<EquipmentPageView>();
        builder.Services.AddTransient<EquipmentPageVM>();

        builder.Services.AddTransient<TransferPageView>();
        builder.Services.AddTransient<TransferPageVM>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

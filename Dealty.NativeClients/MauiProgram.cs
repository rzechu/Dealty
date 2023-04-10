using Microsoft.AspNetCore.Components.WebView.Maui;
using Dealty.NativeClients.Data;
using Dealty.Shared.Services;
using Dealty.Shared.Data;

namespace Dealty.NativeClients;

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
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		
		builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<IDataService, DataService>();

        builder.Services.AddSingleton<PromotionStateContainer>();

        return builder.Build();
	}
}

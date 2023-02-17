namespace Maui_DatabaseApp.ViewModel;

public class FestivalsMainPageVM : ContentPage
{
	public FestivalsMainPageVM()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}
namespace Maui_DatabaseApp.ViewModel;

public class FestivalDetailPageVMcs : ContentPage
{
	public FestivalDetailPageVMcs()
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
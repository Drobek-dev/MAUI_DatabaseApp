namespace Maui_DatabaseApp.ViewModel;

public class EquipmentPageVM : ContentPage
{
	public EquipmentPageVM()
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
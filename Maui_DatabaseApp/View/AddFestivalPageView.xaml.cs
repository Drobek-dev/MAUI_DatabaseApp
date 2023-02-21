namespace Maui_DatabaseApp.View;

public partial class AddFestivalPageView : ContentPage
{
	public AddFestivalPageView(AddFestivalPageVM addFestivalPageVM)
	{
		InitializeComponent();
		BindingContext = addFestivalPageVM;
	}


}
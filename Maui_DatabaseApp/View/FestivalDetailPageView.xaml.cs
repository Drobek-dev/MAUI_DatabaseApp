namespace Maui_DatabaseApp.View;

public partial class FestivalDetailPageView : ContentPage
{
	public FestivalDetailPageView(FestivalDetailPageVM festivalDetailPageVM)
	{
		InitializeComponent();
		BindingContext= festivalDetailPageVM;
	}
}
namespace Maui_DatabaseApp.View;

public partial class FestivalDetailPageView : ContentPage
{
	FestivalDetailPageVM vm;
	public FestivalDetailPageView(FestivalDetailPageVM festivalDetailPageVM)
	{
		InitializeComponent();
		BindingContext= festivalDetailPageVM;
		vm = festivalDetailPageVM;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Title = vm.Festival?.Name;
    }
}
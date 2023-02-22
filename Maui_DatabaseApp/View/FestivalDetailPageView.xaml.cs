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
    async protected override void OnAppearing()
    {
        base.OnAppearing();
		await vm.Refresh();
		Title = vm.Festival?.Name;
    }
}
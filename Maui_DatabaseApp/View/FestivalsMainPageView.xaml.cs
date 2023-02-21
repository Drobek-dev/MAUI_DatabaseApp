namespace Maui_DatabaseApp.View;

public partial class FestivalsMainPageView : ContentPage
{
    FestivalsMainPageVM vm;
    public FestivalsMainPageView(FestivalsMainPageVM festivalMainPageVM)
	{
        InitializeComponent();
		
        BindingContext= festivalMainPageVM;
        vm = festivalMainPageVM;

    }

    async protected override void OnAppearing()
    {
        base.OnAppearing();
        await vm.Refresh();
    }





}
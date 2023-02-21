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

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (vm is null)
            return;
        vm.SelectedFestivals = new();
        foreach (var eqp in e.CurrentSelection)
        {
            vm.SelectedFestivals.Add((Festival)eqp);
        }
    }



}
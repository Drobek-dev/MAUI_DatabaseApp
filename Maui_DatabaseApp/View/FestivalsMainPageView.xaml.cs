namespace Maui_DatabaseApp.View;

public partial class FestivalsMainPageView : ContentPage
{
	public FestivalsMainPageView(FestivalsMainPageVM festivalMainPageVM)
	{
        InitializeComponent();
		
        BindingContext= festivalMainPageVM;

    }
  




}
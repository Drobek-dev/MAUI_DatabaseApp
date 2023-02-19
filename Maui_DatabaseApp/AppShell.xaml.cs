namespace Maui_DatabaseApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(FestivalDetailPageView), typeof(FestivalDetailPageView));
	}
}

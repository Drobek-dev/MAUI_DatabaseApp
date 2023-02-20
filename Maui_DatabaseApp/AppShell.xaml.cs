namespace Maui_DatabaseApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(FestivalDetailPageView), typeof(FestivalDetailPageView));
		
		Routing.RegisterRoute(nameof(TransferPageView), typeof(TransferPageView));
		Routing.RegisterRoute(nameof(ExternalWorkerPageView), typeof(ExternalWorkerPageView));
		Routing.RegisterRoute(nameof(EquipmentPageView), typeof(EquipmentPageView));

		Routing.RegisterRoute(nameof(AddEquipmentPageView), typeof(AddEquipmentPageView));
		Routing.RegisterRoute(nameof(AddExternalWorkerPageView), typeof(AddExternalWorkerPageView));
		Routing.RegisterRoute(nameof(AddFestivalPageView), typeof(AddFestivalPageView));


	}
}

namespace Maui_DatabaseApp.View;

public partial class ExternalWorkerPageView : ContentPage
{
	public ExternalWorkerPageView(ExternalWorkerPageVM externalWorkerPageVM)
	{
		InitializeComponent();
		BindingContext= externalWorkerPageVM;
	}


}
namespace Maui_DatabaseApp.View;

public partial class AddExternalWorkerPageView : ContentPage
{
    AddExternalWorkerPageVM vm; 
	public AddExternalWorkerPageView(AddExternalWorkerPageVM addExternalWorkerPageVM)
	{
		InitializeComponent();
		BindingContext = addExternalWorkerPageVM;
        vm = addExternalWorkerPageVM;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}
namespace Maui_DatabaseApp.View;

public partial class AddExternalWorkerPageView : ContentPage
{
	public AddExternalWorkerPageView(AddEquipmentPageVM addEquipmentPageVM)
	{
		InitializeComponent();
		BindingContext = addEquipmentPageVM;
	}
}
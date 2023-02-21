namespace Maui_DatabaseApp.View;

public partial class AddEquipmentPageView : ContentPage
{
	public AddEquipmentPageView(AddEquipmentPageVM addEquipmentPageVM)
	{
		InitializeComponent();
		BindingContext= addEquipmentPageVM;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
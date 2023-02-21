

namespace Maui_DatabaseApp.View;

public partial class EquipmentPageView : ContentPage
{
    EquipmentPageVM vm;
    public EquipmentPageView(EquipmentPageVM equipmentPageVM)
	{
		InitializeComponent();
		BindingContext= equipmentPageVM;
        vm = equipmentPageVM;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AddButton.IsVisible = !vm.NavigatedToBin;
        BinButton.IsVisible= !vm.NavigatedToBin;
    }
    


    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (vm is null)
            return;
        vm.EquipmentToTransfer = new();
        foreach (var eqp in e.CurrentSelection)
        {
            vm.EquipmentToTransfer.Add((Equipment)eqp);
        }
    }
}
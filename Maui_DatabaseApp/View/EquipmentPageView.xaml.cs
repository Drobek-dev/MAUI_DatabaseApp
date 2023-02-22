

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

    async protected override void OnAppearing()
    {
        base.OnAppearing();
        await vm.Refresh();
        AddButton.IsVisible = !vm.NavigatedToBin;
        BinButton.IsVisible= !vm.NavigatedToBin;
        EmptyTheBinButton.IsVisible= vm.NavigatedToBin;
    }
    


    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (vm is null)
            return;
        vm.SelectedEquipment = new();
        foreach (var eqp in e.CurrentSelection)
        {
            vm.SelectedEquipment.Add((Equipment)eqp);
        }
    }
}
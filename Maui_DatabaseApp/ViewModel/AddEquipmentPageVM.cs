using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(Festival), nameof(Festival))]
public partial class AddEquipmentPageVM: BaseVM
{
    public Festival Festival { get; init; }
    [ObservableProperty]
    Equipment newEquipment;

    AddEquipmentPageVM()
    {
        newEquipment = new Equipment
        {
            Festival = Festival,
            FestivalID = Festival.FestivalID,
            Quantity = 1,
            Name = "",
            IsInBin = false
        };
    }

    [RelayCommand]
    async Task AddEquipment()
    {
        IsBusy = true;

        if (await DatabaseAccessor.TryAddEquipment(NewEquipment))
        {
            await NavigateTo(Shell.Current.GoToAsync(".."));
            await NotificationDisplayer.DisplayNotificationOperationSuccessful($"[Add equipment {NewEquipment.Name}]");
        }
        else
            await NotificationDisplayer.DisplayNotificationOperationFailed($"[Add equipment {NewEquipment.Name}]");


        IsBusy = false;
    }
}

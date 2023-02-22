using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(FestivalID), nameof(FestivalID))]
public partial class AddEquipmentPageVM: BaseVM
{
    public Guid FestivalID { get; init; }
    [ObservableProperty]
    Equipment newEquipment;

    public AddEquipmentPageVM()
    {
        newEquipment = new Equipment
        { 
            Quantity = 1,
            Name = "",
            IsInBin = false
        };
    }

    [RelayCommand]
    async Task AddEquipment()
    {
        IsBusy = true;
        NewEquipment.FestivalID = FestivalID;

        if (!IsEquipmentInputValid(NewEquipment))
            await NotificationDisplayer.DisplayNotification("All inputs must be valid to proceed...");
           
        else if (await DatabaseAccessor.TryAddEquipment(NewEquipment))
        {
            await NavigateTo(Shell.Current.GoToAsync(".."));
            await NotificationDisplayer.DisplayNotificationOperationSuccessful($"[Add equipment {NewEquipment.Name}]");
        }
        else
            await NotificationDisplayer.DisplayNotificationOperationFailed($"[Add equipment {NewEquipment.Name}]");


        IsBusy = false;
    }
}

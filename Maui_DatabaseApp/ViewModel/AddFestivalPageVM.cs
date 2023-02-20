    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.ViewModel;

public partial class AddFestivalPageVM : BaseVM
{
    [ObservableProperty]
    Festival newFestival;

    AddFestivalPageVM() 
    {
        newFestival = new Festival
        {
            Name = "",
            Location = "",
            StartDate = Globals.GetTodaysDayOnly(),
            EndDate = Globals.GetTodaysDayOnly()
        };
        
    }
    [RelayCommand]
    async Task AddFestival()
    {
        IsBusy = true;
        if (await DatabaseAccessor.TryAddFestival(NewFestival))
        {
            await NavigateTo(Shell.Current.GoToAsync(".."));
            await NotificationDisplayer.DisplayNotificationOperationSuccessful($"[Add festival {NewFestival.Name}]");
        }
        else
            await NotificationDisplayer.DisplayNotificationOperationFailed($"[Add festival {NewFestival.Name}]");
            
        IsBusy= false;
    }
}

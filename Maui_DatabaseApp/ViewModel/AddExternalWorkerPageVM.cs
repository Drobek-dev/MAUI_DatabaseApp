
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(Festival), nameof(Festival))]
public partial class AddExternalWorkerPageVM: BaseVM
{
    public Festival Festival { get; init; }
    [ObservableProperty]
    ExternalWorker newExternalWorker;

    AddExternalWorkerPageVM()
    {
        newExternalWorker = new ExternalWorker
        {
            Festival = Festival,
            FestivalID = Festival.FestivalID,
            Function = "",
            Name = "",
            PhoneNumber = ""
        };
    }

    [RelayCommand]
    async Task AddExternalWorker()
    {
        IsBusy = true;

        if(await DatabaseAccessor.TryAddExternalWorker(NewExternalWorker))
        {
            await NavigateTo(Shell.Current.GoToAsync(".."));
            await NotificationDisplayer.DisplayNotificationOperationSuccessful($"[Add external worker {NewExternalWorker.Name}]");
        }
        else
            await NotificationDisplayer.DisplayNotificationOperationFailed($"[Add external worker {NewExternalWorker.Name}]");


        IsBusy = false;
    }
}

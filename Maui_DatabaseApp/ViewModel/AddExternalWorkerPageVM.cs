
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(FestivalID), nameof(FestivalID))]
public partial class AddExternalWorkerPageVM: BaseVM
{
    public Guid FestivalID { get; init; }
    [ObservableProperty]
    ExternalWorker newExternalWorker;

    public AddExternalWorkerPageVM()
    {
        newExternalWorker = new ExternalWorker
        {
            Function = "",
            Name = "",
            PhoneNumber = ""
        };
    }

    [RelayCommand]
    async Task AddExternalWorker()
    {
        IsBusy = true;       
        NewExternalWorker.FestivalID = FestivalID;

        if (await DatabaseAccessor.TryAddExternalWorker(NewExternalWorker))
        {
            await NavigateTo(Shell.Current.GoToAsync(".."));
            await NotificationDisplayer.DisplayNotificationOperationSuccessful($"[Add external worker {NewExternalWorker.Name}]");
        }
        else
            await NotificationDisplayer.DisplayNotificationOperationFailed($"[Add external worker {NewExternalWorker.Name}]");


        IsBusy = false;
    }
}

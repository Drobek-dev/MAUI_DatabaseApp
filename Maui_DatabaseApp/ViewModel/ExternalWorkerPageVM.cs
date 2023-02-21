using System.Collections.ObjectModel;
using System.Data.SqlTypes;


namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(FestivalID), nameof(FestivalID))]
public partial class ExternalWorkerPageVM : BaseVM
{
	[ObservableProperty]
	int takeAmount = 10;

	[ObservableProperty]
	ObservableCollection<ExternalWorker> externalWorkers;

	Guid lastID = new Guid();

	public Guid FestivalID { get; init; }

	[RelayCommand]
	async Task Refresh(bool calledFromNextPage = false)
	{
		IsBusy = true;
		ExternalWorkers = await DatabaseAccessor.GetExternalWorkersFromFestival(FestivalID) ?? new();
		lastID = ExternalWorkers.Count > 0 ? ExternalWorkers.Last().ExternalWorkerID : new();
		IsBusy = calledFromNextPage;
	}
	[RelayCommand]
	async Task LoadNextPage()
	{
		IsBusy = true;
        ExternalWorkers = await DatabaseAccessor.GetExternalWorkersFromFestival(FestivalID, lastID, TakeAmount) ?? new();
		
		if (ExternalWorkers.Count == 0)
			await Refresh(true);
		else
			lastID= ExternalWorkers.Last().ExternalWorkerID;
		
        IsBusy = false;
	}

	[RelayCommand]
    async Task Update(ExternalWorker externalWorker)
	{
		IsBusy = true;
		if ( await DatabaseAccessor.TryUpdateExternalWorker(externalWorker))
		{
			await NotificationDisplayer.DisplayNotification($"External worker {externalWorker.Name} succesfully updated.");
		}
		else
		{
			await NotificationDisplayer.DisplayNotification($"Error: external worker update failed!{Environment.NewLine}" +
				$"Try reloading external workers page.");
		}
		IsBusy = false;
	}

	[RelayCommand]
	async Task NavToAddExternalWorker()
	{
		IsBusy = true;
		await NavigateTo(Shell.Current.GoToAsync(nameof(AddExternalWorkerPageView),new Dictionary<string, object>
		{
			["FestivalID"] = FestivalID
		}));
        IsBusy = false;
    }

	[RelayCommand]
	async Task DeleteExternalWorker(ExternalWorker ew)
	{
		IsBusy = true;
		if(await DatabaseAccessor.TryDeleteExternalWorker(ew))
		{
			await NotificationDisplayer.DisplayNotification($"External worker{ew.Name} succesfully deleted.");
			ExternalWorkers.Remove(ew);
		}
		else
		{
            await NotificationDisplayer.DisplayNotification($"Error: external worker operation failed!{Environment.NewLine}" +
                $"Try reloading external workers page.");
        }
		IsBusy = false;
	}

	[RelayCommand]
	async Task PerformSearch(string substring)
	{
		IsBusy = true;	
        if (string.IsNullOrWhiteSpace(substring))
        {
            ExternalWorkers = new();
		}
		else
		{
            ExternalWorkers = await DatabaseAccessor.GetExternalWorkersByNameSubstring(substring) ?? new();
        }
		IsBusy= false;
    }
}
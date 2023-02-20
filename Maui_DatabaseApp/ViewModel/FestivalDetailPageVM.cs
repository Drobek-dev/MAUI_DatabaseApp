namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(Festival), nameof(Festival))]
public partial class FestivalDetailPageVM : BaseVM
{

	[ObservableProperty]
	Festival festival;

	[RelayCommand]
	async Task Refresh()
	{
		if (Festival is null)
		{
			await NotificationDisplayer.DisplayNotification($"Festival is Null.{Environment.NewLine}" +
				$"Try reloading this page.");
			return;
		}
		IsBusy = true;
		Festival = await DatabaseAccessor.GetFestivalByID(Festival.FestivalID);

		if(Festival is null)
		{
			await NavigateTo(Shell.Current.GoToAsync(nameof(FestivalsMainPageView)));
			await NotificationDisplayer.DisplayNotification($"Festival {Festival.Name} was not found.{Environment.NewLine}" +
				$"Redirecting to Festivals main page.");
		}
		IsBusy = false;
	}

	[RelayCommand]
	async Task Update()
	{
		IsBusy = true;
		if (await DatabaseAccessor.TryUpdateFestival(Festival))
			await NotificationDisplayer.DisplayNotification($"Festival {Festival.Name} suiccessfully updated.");
		else
            await NotificationDisplayer.DisplayNotification($"Error: Festival update failed!{Environment.NewLine}" +
                $"Try reloading this page.");
        IsBusy = false;
	}

	[RelayCommand]
	async Task NavToEquipment()
	{
		IsBusy = true;
		await NavigateTo(Shell.Current.GoToAsync(nameof(EquipmentPageView), new Dictionary<string, object>
		{
			["FestivalID"] = Festival.FestivalID
		}));

		IsBusy = false;
	}

	[RelayCommand]
	async Task NavToExternalWorkers()
	{
		IsBusy = true;
        await NavigateTo(Shell.Current.GoToAsync(nameof(ExternalWorkerPageView), new Dictionary<string, object>
        {
            ["FestivalID"] = Festival.FestivalID
        }));

        IsBusy = false;

	}

	[RelayCommand]
	async Task DeleteFestival()
	{
		IsBusy = true;
		if (await DatabaseAccessor.TryDeleteFestival(Festival))
			await NotificationDisplayer.DisplayNotification($"Festival {Festival.Name} successfully deleted.");
		else
            await NotificationDisplayer.DisplayNotification($"Error: Festival delete operation failed!{Environment.NewLine}" +
                $"Try reloading this page.");
        IsBusy = false;
	}

	
}
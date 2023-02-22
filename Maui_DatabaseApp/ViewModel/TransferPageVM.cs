using System.Collections.ObjectModel;

namespace Maui_DatabaseApp.ViewModel;


[QueryProperty(nameof(EquipmentToTransfer), nameof(EquipmentToTransfer))]
[QueryProperty(nameof(OriginalLocationFestivalID), nameof(OriginalLocationFestivalID))]
public partial class TransferPageVM : BaseVM
{
	public Guid OriginalLocationFestivalID { get; init; }

	[ObservableProperty]
	ObservableCollection<Equipment> equipmentToTransfer;

	[ObservableProperty]
	int takeAmount = Globals.TAKE_AMOUNT;

	Guid lastID = new();

	[ObservableProperty]
	ObservableCollection<TargetType> targets;

	void FestivalsToTargets(ObservableCollection<Festival> festivals)
	{
		Targets = new();
		foreach (var f in festivals)
		{
			Targets.Add(new TargetType
			{
				TargetID = f.FestivalID,
				TargetName = f.Name
			});
		}
        lastID = festivals.Count > 0 ? festivals.Last().FestivalID : new Guid();
    }
	[RelayCommand]
	async Task Refresh(bool calledFromNextPage = false)
	{
		IsBusy = true;
		ObservableCollection<Festival> festivals = await DatabaseAccessor.GetFestivalsSuitableForTransfer(OriginalLocationFestivalID, lastID, TakeAmount) ?? new();

        FestivalsToTargets(festivals);
		IsBusy = calledFromNextPage;
	}

	[RelayCommand]
	async Task LoadNextPage()
	{
		IsBusy = true;
        ObservableCollection<Festival> festivals = await DatabaseAccessor.GetFestivalsSuitableForTransfer(OriginalLocationFestivalID,lastID,TakeAmount) ?? new();
		
		if (festivals.Count == 0)
			await Refresh(true);
		else
		{
            FestivalsToTargets(festivals);
		}

        IsBusy = false;
	}

	[RelayCommand]
	async Task Transfer(Guid festivalID)
	{
		IsBusy = true;
		if (await DatabaseAccessor.TryChangeEquipmentLocation(EquipmentToTransfer, festivalID))
		{
			await NotificationDisplayer.DisplayNotificationOperationSuccessful("[Equipment transfer]");

			await NavigateTo(Shell.Current.GoToAsync(nameof(EquipmentPageView), new Dictionary<string, object>
			{
				["FestivalID"] = festivalID
			}));
		}
		else
			await NotificationDisplayer.DisplayNotificationOperationFailed("[Equipment transfer]");
		IsBusy = false;

	}

	[RelayCommand]
	async Task PerformSearch(string substring)
	{
        IsBusy = true;
        if (string.IsNullOrWhiteSpace(substring))
        {
            Targets = new();
        }
        else
        {
            var festivals = await DatabaseAccessor.GetFestivalsByNameSubstringForTransfer(substring, OriginalLocationFestivalID) ?? new();
			FestivalsToTargets(festivals);
        }
        IsBusy = false;
    }
}

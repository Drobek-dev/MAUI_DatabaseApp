using System.Collections.ObjectModel;

namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(FestivalID),nameof(FestivalID))]
public partial class EquipmentPageVM : BaseVM
{
	[ObservableProperty]
	int takeAmount = 10;

	int navigationLocationGlobID = -1;
	Guid lastID = new();

	[ObservableProperty]
	ObservableCollection<Equipment> equipmentToDisplay = new();

	[ObservableProperty]
	ObservableCollection<Equipment> selectedEquipment = new();

	[ObservableProperty]
	ObservableCollection<Equipment> equipmentToTransfer = new();

	List<Guid> eqpToTransferIDs = new();


	public Guid FestivalID;

	bool navigatedToBin;
	public bool NavigatedToBin
	{
		get => FestivalID.Equals(new Guid());
		init
		{
			if (value == navigatedToBin)
				return;

			navigatedToBin = value;
			OnPropertyChanged(nameof(NavigatedToBin));
		}
	}

	[RelayCommand]
	async Task Refresh(bool navigatedFromLoadNext = false)
	{
		IsBusy = true;

		if(NavigatedToBin)
			EquipmentToDisplay = await DatabaseAccessor.GetBinContent(takeAmount: TakeAmount);
		else
			EquipmentToDisplay= await DatabaseAccessor.GetEquipmentFromFestival(FestivalID, takeAmount: TakeAmount);

        lastID = EquipmentToDisplay.Count == 0 ? new() : EquipmentToDisplay.First().FestivalID;
        IsBusy = navigatedFromLoadNext;
	}

	[RelayCommand]
	async Task LoadNextEquipment()
	{
		IsBusy = true;
		if(NavigatedToBin)
            EquipmentToDisplay = await DatabaseAccessor.GetBinContent(lastID, TakeAmount);
		else
            EquipmentToDisplay = await DatabaseAccessor.GetEquipmentFromFestival(FestivalID, lastID, TakeAmount);

		if (EquipmentToDisplay.Count == 0)
			await Refresh(true);
		else
			lastID = EquipmentToDisplay.First().FestivalID;

        IsBusy = false;
	}

	[RelayCommand]
	void MoveSelectedToTransferSection()
	{
		IsBusy = true;
		foreach(var e in SelectedEquipment)
		{
			if (!eqpToTransferIDs.Contains(e.EquipmentID))
			{
				eqpToTransferIDs.Add(e.EquipmentID);
				EquipmentToTransfer.Add(e);
			}
		}
		IsBusy = false;
	}

	[RelayCommand]
	async Task NavToTransferPage()
	{
		if (navigationLocationGlobID == Globals.InvalidValue)
			return;
		else if(navigationLocationGlobID == Globals.Bin)
		{
			if (await DatabaseAccessor.TryMoveEquipmentToBin(EquipmentToTransfer))
				await NotificationDisplayer.DisplayNotificationOperationSuccessful("[Bin Tranfer]");
			else
				await NotificationDisplayer.DisplayNotificationOperationFailed("[Bin Transfer]");
		}
		else if(navigationLocationGlobID == Globals.Festival)
		{
			await NavigateTo(Shell.Current.GoToAsync(nameof(TransferPageView), new Dictionary<string, object>
			{
				[nameof(EquipmentToTransfer)] = EquipmentToTransfer
			}));

		}

    }

	[RelayCommand]
	void ClenEquipmentToTransfer()
	{
		EquipmentToTransfer = new();
		eqpToTransferIDs = new();
	}
}
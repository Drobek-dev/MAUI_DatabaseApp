using System.Collections.ObjectModel;

namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(FestivalID),nameof(FestivalID))]
public partial class EquipmentPageVM : BaseVM
{
	[ObservableProperty]
	int takeAmount = 10;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsDisplayingEquipmentToTransfer))]
	bool isNotDisplayingEquipmentToTransfer = true;

	public bool IsDisplayingEquipmentToTransfer => !IsNotDisplayingEquipmentToTransfer; 

	Guid lastID = new();

	[ObservableProperty]
	ObservableCollection<Equipment> equipmentToDisplay = new();

	[ObservableProperty]
	ObservableCollection<Equipment> selectedEquipment = new();

	[ObservableProperty]
	ObservableCollection<Equipment> equipmentToTransfer = new();

	List<Guid> eqpToTransferIDs = new();


	public Guid FestivalID { get; init; }

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
			OnPropertyChanged(nameof(IsThisFestivalEquipment));
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
	async Task LoadNextPage()
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
	async Task PerformSearch(string substring)
	{
        if (string.IsNullOrWhiteSpace(substring))
        {
            EquipmentToDisplay = new();
        }
        else
        {
            EquipmentToDisplay = await DatabaseAccessor.GetEquipmentByNameSubstring(substring) ?? new();
        }
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
	void MoveDoubleClickedToTransferSection(Equipment e)
	{
		IsBusy = true;
		if (!eqpToTransferIDs.Contains(e.EquipmentID))
		{
            eqpToTransferIDs.Add(e.EquipmentID);
            EquipmentToTransfer.Add(e);
        }
		IsBusy = false;

	}

	[RelayCommand]
	async Task NavToTransferPage(bool transferToBin)
	{
		
		if(transferToBin)
		{
			if (await DatabaseAccessor.TryMoveEquipmentToBin(EquipmentToTransfer))
				await NotificationDisplayer.DisplayNotificationOperationSuccessful("[Bin Tranfer]");
			else
				await NotificationDisplayer.DisplayNotificationOperationFailed("[Bin Transfer]");
		}
		else
		{
			await NavigateTo(Shell.Current.GoToAsync(nameof(TransferPageView), new Dictionary<string, object>
			{
				[nameof(EquipmentToTransfer)] = EquipmentToTransfer
			}));

		}

    }

	[RelayCommand]
	async Task NavToAddEquipmentPage()
	{
		await NavigateTo(Shell.Current.GoToAsync(nameof(AddEquipmentPageView), new Dictionary<string, object>
		{
			["FestivalID"] = FestivalID
		}));
	}

	[RelayCommand]
	void ChangeDisplayMode()
	{
		IsNotDisplayingEquipmentToTransfer = !IsNotDisplayingEquipmentToTransfer;
	}

	[RelayCommand]
	void ClenEquipmentToTransfer()
	{
		EquipmentToTransfer = new();
		eqpToTransferIDs = new();
	}
}
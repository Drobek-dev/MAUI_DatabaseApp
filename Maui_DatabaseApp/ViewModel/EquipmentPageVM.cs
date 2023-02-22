using System.Collections.ObjectModel;

namespace Maui_DatabaseApp.ViewModel;

[QueryProperty(nameof(FestivalID),nameof(FestivalID))]
public partial class EquipmentPageVM : BaseVM
{
	[ObservableProperty]
	int takeAmount = Globals.TAKE_AMOUNT + 2;

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

        lastID = EquipmentToDisplay.Count == 0 ? new() : EquipmentToDisplay.Last().EquipmentID;
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
			lastID = EquipmentToDisplay.Last().EquipmentID;

        IsBusy = false;
	}

	[RelayCommand]
	async Task PerformSearch(string substring)
	{
		IsBusy= true;
        if (string.IsNullOrWhiteSpace(substring))
        {
            EquipmentToDisplay = new();
        }
        else
        {
            EquipmentToDisplay = await DatabaseAccessor.GetEquipmentByNameSubstring(substring) ?? new();
        }
		IsBusy= false;
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
		IsBusy= true;
		if(transferToBin)
		{
			if (await DatabaseAccessor.TryMoveEquipmentToBin(EquipmentToTransfer))
			{
				await NotificationDisplayer.DisplayNotificationOperationSuccessful("[Bin Transfer]");
				foreach(var e in EquipmentToTransfer) 
				{
					EquipmentToDisplay.Remove(e);
												
				}
				EquipmentToTransfer = new();

			}
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
		IsBusy= false;
    }

	[RelayCommand]
	async Task DeleteEquipment()
	{
		IsBusy= true;
		int delNum = EquipmentToTransfer.Count;
		if(await DatabaseAccessor.TryDeleteMultipleEquipment(EquipmentToTransfer))
		{
			foreach(var e in EquipmentToTransfer) 
			{
				EquipmentToDisplay.Remove(e);
			}
			EquipmentToTransfer = new();
			await NotificationDisplayer.DisplayNotificationOperationSuccessful($"[Delete {delNum} equipment]");
		}
		else
            await NotificationDisplayer.DisplayNotificationOperationSuccessful($"[Delete {delNum} equipment]");
        IsBusy = false;
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
	void ClearEquipmentToTransfer()
	{
		IsBusy= true;
		EquipmentToTransfer = new();
		eqpToTransferIDs = new();
		IsBusy= false;
	}
}
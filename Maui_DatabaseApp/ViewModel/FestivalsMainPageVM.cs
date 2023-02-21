
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Maui_DatabaseApp.ViewModel;

public partial class FestivalsMainPageVM : BaseVM
{
	

    [ObservableProperty]
    int takeAmount =3;

    [ObservableProperty]
    ObservableCollection<Festival> festivals;

	[ObservableProperty]
	ObservableCollection<Festival> selectedFestivals = new();

    Guid lastID = new();

    [RelayCommand]
    internal async Task Refresh(bool calledFromNextPage = false)
    {
        IsBusy = true;
        Festivals = await DatabaseAccessor.GetFestivals(takeAmount: TakeAmount);
        lastID = Festivals is null || Festivals.Count == 0 ? new() : Festivals.Last().FestivalID;
        IsBusy = calledFromNextPage;
    }
	
	[RelayCommand]
	async Task LoadNextPage()
	{
		IsBusy= true;
		Festivals = await DatabaseAccessor.GetFestivals(lastID,TakeAmount) ?? new();
		if(Festivals.Count == 0)
		{
            await Refresh(true);	
        }
		else
		{			
			lastID = Festivals.Last().FestivalID;
		}
        IsBusy = false;
    }

    [RelayCommand]
	async Task NavToFestivalDetail(Festival festival)
	{
		IsBusy = true;
		await NavigateTo(Shell.Current.GoToAsync(nameof(FestivalDetailPageView), new Dictionary<string, object>
		{
			["Festival"] = festival
		})
		);
		IsBusy =false;
	}

	[RelayCommand]
	async Task NavToAddNewFestival()
	{
		IsBusy = true;
		await NavigateTo(Shell.Current.GoToAsync(nameof(AddFestivalPageView)));
		IsBusy = false;
	}

	[RelayCommand]
	async Task PerformSearch(string searchedName)
	{
		IsBusy= true;
        if (string.IsNullOrWhiteSpace(searchedName))
        {
			Festivals = new();
        }
        else
        {
            Festivals = await DatabaseAccessor.GetFestivalByNameSubstring(searchedName) ?? new();
        }
		IsBusy= false;
    }

	[RelayCommand]
	async Task DeleteFestivals()
	{
		IsBusy = true;
		if(await DatabaseAccessor.TryDeleteFestivals(SelectedFestivals))
		{
			foreach(var f in SelectedFestivals)
			{
				Festivals.Remove(f);
			}
			await NotificationDisplayer.DisplayNotificationOperationSuccessful("[Multiple festival delete operation]");
		}
		else
		{
			await NotificationDisplayer.DisplayNotificationOperationFailed("[Multiple festival delete operation]");
        }
		IsBusy = true;
	}


}
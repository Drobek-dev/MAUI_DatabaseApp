
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
		await NavigateTo(Shell.Current.GoToAsync(nameof(FestivalDetailPageView), new Dictionary<string, object>
		{
			["Festival"] = festival
		})
		);
	}

	[RelayCommand]
	async Task NavToAddNewFestival()
	{
		await NavigateTo(Shell.Current.GoToAsync(nameof(AddFestivalPageView)));
	}

	[RelayCommand]
	async Task PerformSearch(string searchedName)
	{
        if (string.IsNullOrWhiteSpace(searchedName))
        {
			Festivals = new();
        }
        else
        {
            Festivals = await DatabaseAccessor.GetFestivalByNameSubstring(searchedName) ?? new();
        }
    }


}
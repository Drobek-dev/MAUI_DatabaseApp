using System.Collections.ObjectModel;

namespace Maui_DatabaseApp.ViewModel;

public partial class FestivalsMainPageVM : ObservableObject
{
	[ObservableProperty]
	int takeAmount;

	[ObservableProperty]
	public ObservableCollection<Festival> festivals;

	public FestivalsMainPageVM()
	{

	}

	[RelayCommand]
	async Task LoadFestivals()
	{
		Festivals = await DatabaseAccessor.GetFestivals();
	}

 
}
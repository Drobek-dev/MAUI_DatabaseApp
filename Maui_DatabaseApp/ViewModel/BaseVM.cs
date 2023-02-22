using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.CompilerServices;

namespace Maui_DatabaseApp.ViewModel;

public partial class BaseVM: ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool _isBusy;

    public bool IsNotBusy => !IsBusy;

    public bool FalseValue { get; } = false;
    public bool TrueValue { get; } = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsInternetNotAvailable))]
    bool isInternetAvailable;

    public bool IsInternetNotAvailable => !IsInternetAvailable;

    public BaseVM()
    {
        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            IsInternetAvailable = true;
        }
        else
        {
            IsInternetAvailable = false;
        }

    }
    ~BaseVM() =>
     Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

    protected virtual void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.Internet)
        {
            IsInternetAvailable = true;
        }
        else
        {
            IsInternetAvailable = false;
        }

    }

    protected async Task NavigateTo(Task navTask)
    {
        IsBusy = true;
        await navTask;
        IsBusy = false;
    }

    protected static bool IsFestivalInputValid(Festival f)
    {
        return
            IsStringNameEntryValid(f.Name) &&
            IsStringNameEntryValid(f.Location);
    }

    protected static bool IsExternalWorkerValid(ExternalWorker ew)
    {
        return IsStringNameEntryValid(ew.Name) &&
            IsStringNameEntryValid(ew.Function) &&
            IsStringPhoneNumberValid(ew.PhoneNumber);
    }

    protected static bool IsEquipmentInputValid(Equipment e)
    {
        return
            IsStringNameEntryValid(e.Name);

    }

    static private bool IsStringNameEntryValid(string input) => input.Length >= 2;

    static private bool IsStringPhoneNumberValid(string input) => Globals.MyPhoneRegex().IsMatch(input);
           
   
}

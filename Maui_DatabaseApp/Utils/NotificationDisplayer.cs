namespace Maui_DatabaseApp.Utils;

internal static class NotificationDisplayer
{
    internal static async Task<bool> DisplayYesNoAlert(string question)
    {
        return await Shell.Current.DisplayAlert("Question?", question, "Yes", "No");
    }

    internal static async Task DisplayNotification(string message)
    {
        await Shell.Current.DisplayAlert("Alert", message, "Ok");
    }
    internal static async Task DisplayNotification(Exception e)
    {
        string message = "";
        while (e is not null)
        {
            message += $" Exception: {e.Message} {Environment.NewLine}";
            e = e.InnerException;
        }
        await Shell.Current.DisplayAlert("Alert", message, "Ok");
    }

    internal static async Task DisplayNotificationOperationSuccessful(string operationName)
    {
        await Shell.Current.DisplayAlert("Confirmation", $"Operation {operationName} successful!", "Ok");
    }

    internal static async Task DisplayNotificationOperationFailed(string operationName)
    {
        await Shell.Current.DisplayAlert("Confirmation", $"Operation {operationName} failed!{Environment.NewLine}" +
            $"Try reloading this page.", "Ok");
    }
}

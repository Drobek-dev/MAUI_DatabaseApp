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
}

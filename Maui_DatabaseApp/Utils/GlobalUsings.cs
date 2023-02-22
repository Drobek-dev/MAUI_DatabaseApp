global using Maui_DatabaseApp.View;
global using Maui_DatabaseApp.ViewModel;
global using Maui_DatabaseApp.Services.Database;
global using Maui_DatabaseApp.Utils;
global using Maui_DatabaseApp.Model.Entities;

global using Microsoft.EntityFrameworkCore;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace Maui_DatabaseApp.Utils
{ 

    public static partial class Globals
    {
        public const int TAKE_AMOUNT = 10;

        public static DateOnly GetTodaysDayOnly()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

        [GeneratedRegex("^\\+?[1-9][0-9]{7,14}$", RegexOptions.Compiled)]
        public static partial Regex MyPhoneRegex();

    }

    public class TargetType
    {
        public Guid TargetID { get; set; }
        public string TargetName { get; set; }
    }
}


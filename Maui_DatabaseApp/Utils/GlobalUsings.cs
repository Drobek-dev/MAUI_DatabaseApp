global using Maui_DatabaseApp.View;
global using Maui_DatabaseApp.ViewModel;
global using Maui_DatabaseApp.Services.Database;
global using Maui_DatabaseApp.Utils;
global using Maui_DatabaseApp.Model.Entities;

global using Microsoft.EntityFrameworkCore;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;

namespace Maui_DatabaseApp.Utils
{ 

    public static class Globals
    {

        public static DateOnly GetTodaysDayOnly()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

    }

    public class TargetType
    {
        public Guid TargetID { get; set; }
        public string TargetName { get; set; }
    }
}


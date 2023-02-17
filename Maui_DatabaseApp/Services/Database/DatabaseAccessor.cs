using Maui_DatabaseApp.Model.Entities;
using Maui_DatabaseApp.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.Services.Database;

internal class DatabaseAccessor 
{

    public static async Task<ObservableCollection<Festival>> GetFestivals()
    {
        try
        {
            DatabaseConnector connector = new();
            var festivals = connector.Festivals.OrderByDescending(f => f.Name).ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Festival> ret = new(festivals);
            return ret; 
            

        }
        catch(Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<Equipment>> GetEquipment()
    {
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.Equipment.OrderByDescending(f => f.Name).ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Equipment> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<ExternalWorker>> GetExternalWorkers()
    {
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.ExternaWorkers.OrderByDescending(f => f.Name).ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<ExternalWorker> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<Bin>> GetBin()
    {
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.Bin.Include(b => b.Equipment).OrderBy(b=> b.Equipment.Name).ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Bin> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }


}

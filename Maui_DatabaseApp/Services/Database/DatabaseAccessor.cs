using System.Collections.ObjectModel;

namespace Maui_DatabaseApp.Services.Database;

internal class DatabaseAccessor 
{
    // Get Type Paging
    public static async Task<ObservableCollection<Festival>> GetFestivals(Guid start = new Guid(),int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        
        try
        {
            DatabaseConnector connector = new();
            var festivals = connector.Festivals
                .Where(f=> f.FestivalID > start)
                .OrderBy(f => f.FestivalID)
                .Take(takeAmount)
                .ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Festival> ret = new(festivals);
            return ret; 
            

        }
        catch(Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<Equipment>> GetEquipment(Guid start = new Guid(), int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.Equipment
                .Where(e => e.EquipmentID > start)
                .OrderBy(e => e.EquipmentID)
                .Take(takeAmount)
                .AsNoTracking()
                .ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Equipment> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }
    public static async Task<ObservableCollection<Equipment>> GetEquipmentFromFestival( Festival festival, Guid start = new Guid(), int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.Equipment
                .Where(e => !e.IsInBin && e.EquipmentID > start && e.FestivalID == festival.FestivalID)
                .OrderBy(e => e.EquipmentID)
                .Take(takeAmount)
                .AsNoTracking()
                .ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Equipment> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }
    public static async Task<ObservableCollection<ExternalWorker>> GetExternalWorkers(Guid start = new Guid(), int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.ExternaWorkers
                .Where(ew=> ew.ExternalWorkerID > start)
                .OrderBy(ew=>ew.ExternalWorkerID)
                .AsNoTracking()
                .ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<ExternalWorker> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<ExternalWorker>> GetExternalWorkersFromFestival(Festival festival, Guid start = new Guid(), int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.ExternaWorkers
                .Where(ew => ew.ExternalWorkerID > start && ew.FestivalID == festival.FestivalID)
                .OrderBy(ew => ew.ExternalWorkerID)
                .AsNoTracking()
                .ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<ExternalWorker> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<Equipment>> GetBinContent(Guid start = new Guid(), int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        try
        {
            DatabaseConnector connector = new();
            var eqp = connector.Equipment
                .Where(e => e.EquipmentID > start && e.IsInBin)
                .OrderBy(e=> e.EquipmentID)
                .AsNoTracking()
                .ToList(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Equipment> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    // Get by Name
     public static async Task<Festival> GetFestivalByName(string name)
    {  
        try
        {
            DatabaseConnector connector = new();
            var f = await connector.Festivals
                .FindAsync(name);
            return f;

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<Equipment> GetEquipmentByName(string name)
    {
        try
        {
            DatabaseConnector connector = new();
            var e = await connector.Equipment
                .FindAsync(name);
            return e;

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }

    }
    public static async Task<ExternalWorker> GetExternalWorkerByName(string name)
    {
        try
        {
            DatabaseConnector connector = new();
            var ew = await connector.ExternaWorkers
                .FindAsync(name);
            return ew;

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }

    }

    // Equipment manipulation
    public static async Task<bool> TryChangeEquipmentLocation(List<Equipment> equipment, Festival festivalToBeAssignedToEquipment)
    {
        Festival f = festivalToBeAssignedToEquipment;
        try
        {
            DatabaseConnector connector = new();
            connector.AttachRange(equipment);
            foreach (var e in equipment) 
            {
                e.Festival = f;
                e.FestivalID = f.FestivalID;
                if (e.IsInBin)
                {
                    throw new ArgumentException($"Equipment in the bin can not have its location changed!" +
                        $"Equipment {e.Name} with ID: {e.EquipmentID} is located in the bin!" +
                        $"Restore the equipment first to change its location.");
                }
            }
            await connector.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    public static async Task<bool> TryMoveEquipmentToBin(List<Equipment> equipment)
    {
        try
        {
            DatabaseConnector connector = new();
            connector.AttachRange(equipment);
            foreach (var e in equipment)
            {
                e.IsInBin = true;
            }
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }
    public static async Task<bool> TryRestoreEquipment(List<Equipment> equipment, Festival festivalToBeAssignedToEquipment = null)
    {
        if (equipment is null || equipment.Count == 0)
            throw new ArgumentException($"Equipment must not be null or an empty list!");

        Guid festivalID = festivalToBeAssignedToEquipment is null ? new Guid() : festivalToBeAssignedToEquipment.FestivalID;
        Festival festival = festivalToBeAssignedToEquipment is null ? equipment.First().Festival : festivalToBeAssignedToEquipment;
        try
        {
            DatabaseConnector connector = new();
            foreach (var e in equipment)
            {
                e.IsInBin = false;
                e.FestivalID= festivalID;
                e.Festival = festival;
            }
            await connector.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }
    
    // Adding an entitie

    public static async Task<bool> TryAddFestival(Festival festival)
    {
        try
        {
            DatabaseConnector connector= new();
            await connector.Festivals.AddAsync(festival);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex); 
            return false;
        }
    }

    public static async Task<bool> TryAddEquipment(Equipment equipment)
    {

        try
        {
            DatabaseConnector connector = new();
            await connector.Equipment.AddAsync(equipment);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    public static async Task<bool> TryAddExternalWorker(ExternalWorker externalWorker)
    {

        try
        {
            DatabaseConnector connector = new();
            await connector.ExternaWorkers.AddAsync(externalWorker);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    // Update of an entitie
    public static async Task<bool> TryUpdateFestival(Festival festival)
    {
        try
        {
            DatabaseConnector connector = new();
            connector.Festivals.Update(festival);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    public static async Task<bool> TryUpdateEquipment(Equipment equipment)
    {

        try
        {
            DatabaseConnector connector = new();
            connector.Equipment.Update(equipment);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    public static async Task<bool> TryUpdateExternalWorker(ExternalWorker externalWorker)
    {

        try
        {
            DatabaseConnector connector = new();
            connector.ExternaWorkers.Update(externalWorker);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    // Delete of an entity
    public static async Task<bool> TryDeleteFestival(Festival festival)
    {
        try
        {
            DatabaseConnector connector = new();
            connector.Festivals.Remove(festival);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    public static async Task<bool> TryDeleteEquipment(Equipment equipment)
    {

        try
        {
            DatabaseConnector connector = new();
            connector.Equipment.Remove(equipment);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

    public static async Task<bool> TryDeleteExternalWorker(ExternalWorker externalWorker)
    {

        try
        {
            DatabaseConnector connector = new();
            connector.ExternaWorkers.Remove(externalWorker);
            await connector.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return false;
        }
    }

}

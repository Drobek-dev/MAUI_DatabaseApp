using System.Collections.ObjectModel;


namespace Maui_DatabaseApp.Services.Database;

public class DatabaseAccessor 
{
    // Get Type Paging
    public static async Task<ObservableCollection<Festival>> GetFestivals(Guid start = new Guid(),int takeAmount = 10)
    {
        takeAmount = takeAmount <= 0 ? 10 : takeAmount;
        
        try
        {
            DatabaseConnector connector = new();
            var festivals = await connector.Festivals
                .Where(f=> f.FestivalID > start)
                .OrderBy(f => f.FestivalID)
                .Take(takeAmount)
                .ToListAsync(); // weirdly ToListAsync lasts forever...
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
            var eqp = await connector.Equipment
                .Where(e => e.EquipmentID > start)
                .OrderBy(e => e.EquipmentID)
                .Take(takeAmount)
                .AsNoTracking()
                .ToListAsync(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Equipment> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }
    public static async Task<ObservableCollection<Equipment>> GetEquipmentFromFestival( Guid festivalID, Guid start = new Guid(), int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        try
        {
            DatabaseConnector connector = new();
            var eqp = await connector.Equipment
                .Where(e => e.FestivalID == festivalID && !e.IsInBin && e.EquipmentID > start)
                .OrderBy(e => e.EquipmentID)
                .Take(takeAmount)
                .AsNoTracking()
                .ToListAsync(); // weirdly ToListAsync lasts forever...
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
            var eqp = await connector.ExternalWorkers
                .Where(ew=> ew.ExternalWorkerID > start)
                .OrderBy(ew=>ew.ExternalWorkerID)
                .AsNoTracking()
                .Take(takeAmount)
                .ToListAsync(); // weirdly ToListAsync lasts forever...
            ObservableCollection<ExternalWorker> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<ExternalWorker>> GetExternalWorkersFromFestival(Guid festivalID, Guid start = new Guid(), int takeAmount = 10)
    {
        takeAmount = takeAmount < 0 ? 10 : takeAmount;
        try
        {
            DatabaseConnector connector = new();
            var eqp = await connector.ExternalWorkers
                .Where(ew => ew.FestivalID == festivalID && ew.ExternalWorkerID > start)
                .OrderBy(ew => ew.ExternalWorkerID)
                .Take(takeAmount)
                .AsNoTracking()
                .ToListAsync(); // weirdly ToListAsync lasts forever...
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
            var eqp = await connector.Equipment
                .Where(e => e.EquipmentID > start && e.IsInBin)
                .OrderBy(e=> e.EquipmentID)
                .Take(takeAmount)
                .AsNoTracking()
                .ToListAsync(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Equipment> ret = new(eqp);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    // Get Festivals For Transfer
    public static async Task<ObservableCollection<Festival>> GetFestivalsSuitableForTransfer(Guid originalLocationFestivalID, Guid start = new(),  int takeAmount = 10)
    {
        takeAmount = takeAmount <= 0 ? 10 : takeAmount;

        try
        {
            DatabaseConnector connector = new();
            var festivals = await connector.Festivals
                .Where(f => f.FestivalID > start && f.FestivalID != originalLocationFestivalID)
                .OrderBy(f => f.FestivalID)
                .Take(takeAmount)
                .AsNoTracking()
                .ToListAsync(); // weirdly ToListAsync lasts forever...
            ObservableCollection<Festival> ret = new(festivals);
            return ret;


        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }
    public static async Task<ObservableCollection<Festival>> GetFestivalsByNameSubstringForTransfer(string substring,Guid originalLocationFestivalID)
    {
        try
        {
            DatabaseConnector connector = new();
            var festivals = await connector.Festivals
                .Where(f => f.Name.Contains(substring) && f.FestivalID != originalLocationFestivalID)
                .ToListAsync();
            return new ObservableCollection<Festival>(festivals);

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }

    }
    // Get by ID
    public static async Task<Festival> GetFestivalByID(Guid ID)
    {
        try
        {
            DatabaseConnector connector = new();
            var f = await connector.Festivals
                .FindAsync(ID);
            return f;

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }


    // Get by Name
    public static async Task<ObservableCollection<Festival>> GetFestivalsByName(string name)
    {  
        try
        {
            DatabaseConnector connector = new();
            var f = await connector.Festivals
                .Where(f => f.Name.Equals(name)).AsNoTracking().ToListAsync();
            return new ObservableCollection<Festival>(f);

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }

    public static async Task<ObservableCollection<Festival>> GetFestivalByNameSubstring(string substring)
    {
        try
        {
            DatabaseConnector connector = new();
            var f = await connector.Festivals
                .Where(f => f.Name.Contains(substring)).AsNoTracking().ToListAsync();
            return new ObservableCollection<Festival>(f);

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }
    }
 
    public static async Task<ObservableCollection<Equipment>> GetEquipmentByName(string name)
    {
        try
        {
            DatabaseConnector connector = new();
            var e = await connector.Equipment
                .Where( e=> e.Name.Equals(name))
                .AsNoTracking()
                .ToListAsync();
            return new ObservableCollection<Equipment>(e);

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }

    }
    public static async Task<ObservableCollection<Equipment>> GetEquipmentByNameSubstring(string substring)
    {
        try
        {
            DatabaseConnector connector = new();
            var e = await connector.Equipment
                .Where(e => e.Name.Contains(substring))
                .AsNoTracking()
                .ToListAsync();
            return new ObservableCollection<Equipment>(e);

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }

    }
    public static async Task<ObservableCollection<ExternalWorker>> GetExternalWorkerByName(string name)
    {
        try
        {
            DatabaseConnector connector = new();
            var ew = await connector.ExternalWorkers
                .Where(ew => ew.Name.Equals(name))
                .ToListAsync();
            return new ObservableCollection<ExternalWorker>(ew);

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }

    }
    public static async Task<ObservableCollection<ExternalWorker>> GetExternalWorkersByNameSubstring(string substring)
    {
        try
        {
            DatabaseConnector connector = new();
            var ew = await connector.ExternalWorkers
                .Where( ew => ew.Name.Contains(substring))
                .AsNoTracking()
                .ToListAsync();
            return new ObservableCollection<ExternalWorker>(ew);

        }
        catch (Exception ex)
        {
            await NotificationDisplayer.DisplayNotification(ex);
            return null;
        }

    }

    // Equipment manipulation
    public static async Task<bool> TryChangeEquipmentLocation(ObservableCollection<Equipment> equipment, Guid festivalIDToBeAssignedToEquipment)
    {
        
        try
        {
            DatabaseConnector connector = new();
            Festival f = await GetFestivalByID(festivalIDToBeAssignedToEquipment) 
                ?? throw new Exception($"Festival with ID {festivalIDToBeAssignedToEquipment} not found." +
                $"{Environment.NewLine}Try reloading this page.");

            connector.Equipment.AttachRange(equipment);
            foreach (var e in equipment) 
            {
                e.Festival = f;
                e.FestivalID = f.FestivalID;
                e.IsInBin = false;
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

    public static async Task<bool> TryMoveEquipmentToBin(ObservableCollection<Equipment> equipment)
    {
        try
        {
            DatabaseConnector connector = new();
            connector.Equipment.AttachRange(equipment);
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
    public static async Task<bool> TryRestoreEquipment(ObservableCollection<Equipment> equipment, Festival festivalToBeAssignedToEquipment = null)
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
            connector.Equipment.AttachRange(equipment);
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
            await connector.ExternalWorkers.AddAsync(externalWorker);
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
            connector.ExternalWorkers.Update(externalWorker);
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

    public static async Task<bool> TryDeleteFestivals(ObservableCollection<Festival> festivals)
    {
        try
        {
            DatabaseConnector connector = new();
            connector.Festivals.RemoveRange(festivals);
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

    public static async Task<bool> TryDeleteMultipleEquipment(ObservableCollection<Equipment> equipment)
    {

        try
        {
            DatabaseConnector connector = new();
            connector.Equipment.RemoveRange(equipment);
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
            connector.ExternalWorkers.Remove(externalWorker);
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

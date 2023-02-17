using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Maui_DatabaseApp.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maui_DatabaseApp.Services.Database;

internal class DatabaseConnector : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=rogue.db.elephantsql.com;" +
            "Database=vryvboaw;" +
            "Username=vryvboaw;" +
            "Password=3_bCGbBlw1-E9VqAv60ZYAvadvD-BXIM");
        optionsBuilder.LogTo((string m) => System.Diagnostics.Debug.WriteLine(m), Microsoft.Extensions.Logging.LogLevel.Information);
    }
   
    public DbSet<Festival> Festivals { get; init; }  
    public DbSet<Bin> Bin { get; init; }
    public DbSet<Equipment> Equipment { get; init; }
    public DbSet<ExternalWorker> ExternaWorkers { get; init; }

}

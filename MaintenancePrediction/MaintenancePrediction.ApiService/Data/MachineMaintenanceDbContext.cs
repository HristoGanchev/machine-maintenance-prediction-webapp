using MaintenancePrediction.ApiService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MaintenancePrediction.ApiService.Data
{
    // EF Core DbContext for database interactions.
    public class MachineMaintenanceDbContext : DbContext
    {
        public DbSet<ApiService.Models.MachineData> Machines
        {
            get; set;
        }
        public DbSet<MachineUsage> MachineUsages
        {
            get; set;
        }
        public DbSet<MachineEvent> MachineEvents
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MachineUsage>()
                .HasKey(mu => mu.MachineId);

            modelBuilder.Entity<MachineEvent>()
                .HasKey(me => me.EventId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

using MaintenancePrediction.ApiService.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace MaintenancePrediction.ApiService.Data
{
    // EF Core DbContext for database interactions.
    public class MachineMaintenanceDbContext : DbContext
    {
        public MachineMaintenanceDbContext(DbContextOptions<MachineMaintenanceDbContext> options) : base(options) { }

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

        public DbSet<MachineMaintenanceCheckResult> MachineMaintenanceChecks
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiService.Models.MachineData>()
                .HasMany<MachineEvent>()
                .WithOne(e => e.Machine)
                .HasForeignKey(e => e.MachineId);

            modelBuilder.Entity<MachineUsage>()
                .HasNoKey();

            modelBuilder.Entity<MachineEvent>()
                .HasKey(me => me.EventId);

            modelBuilder.Entity<MachineMaintenanceCheckResult>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApiService.Models.MachineData>().HasData(
                new ApiService.Models.MachineData { MachineId = 1, Name = "Hydraulic Press A1", Location = "Factory Floor 2", RuntimeThreshold = 500.0, CycleTimeThreshold = 2000, CycleCountThreshold = 50000 },
                new ApiService.Models.MachineData { MachineId = 2, Name = "CNC Lathe B1", Location = "Workshop 1", RuntimeThreshold = 400.0, CycleTimeThreshold = 1500, CycleCountThreshold = 50000 }
            );

            modelBuilder.Entity<MachineEvent>().HasData(
                new MachineEvent { EventId = 1, MachineId = 1, EventCode = "E100", Description = "Routine maintenance performed" },
                new MachineEvent { EventId = 2, MachineId = 2, EventCode = "E101", Description = "Overheating detected" }
            );
        }
    }
}

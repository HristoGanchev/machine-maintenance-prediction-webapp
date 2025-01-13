using MaintenancePrediction.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace MaintenancePrediction.ApiService.Data
{
    // EF Core DbContext for database interactions.
    public class MachineMaintenanceDbContext : DbContext
    {
        public MachineMaintenanceDbContext(DbContextOptions<MachineMaintenanceDbContext> options) : base(options) { }

        public DbSet<MachineData> Machines
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
            modelBuilder.Entity<MachineData>()
                .HasMany<MachineEvent>()
                .WithOne(e => e.Machine)
                .HasForeignKey(e => e.MachineId);

            modelBuilder.Entity<MachineUsage>()
                .Property(mu => mu.LastUpdated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<MachineEvent>()
                .Property(me => me.Timestamp)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<MachineMaintenanceCheckResult>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MachineData>().HasData(
                new MachineData { MachineId = 1, Name = "Hydraulic Press A1", Location = "Factory Floor 2", RuntimeThreshold = 500.0, CycleTimeThreshold = 2000, CycleCountThreshold = 50000 },
                new MachineData { MachineId = 2, Name = "CNC Lathe B1", Location = "Workshop 1", RuntimeThreshold = 400.0, CycleTimeThreshold = 1500, CycleCountThreshold = 50000 }
            );

            modelBuilder.Entity<MachineEvent>().HasData(
                new MachineEvent { EventId = 1, MachineId = 1, EventCode = "E100", Description = "Routine maintenance performed" },
                new MachineEvent { EventId = 2, MachineId = 2, EventCode = "E101", Description = "Overheating detected" }
            );

            modelBuilder.Entity<MachineUsage>().HasData(
                new MachineUsage { Id = 1, MachineId = 1, RuntimeHours = 2500, CycleCount = 10000, CycleTime = 8 },
                new MachineUsage { Id = 2, MachineId = 2, RuntimeHours = 5500, CycleCount = 20000, CycleTime = 10 }
            );

            modelBuilder.Entity<MachineMaintenanceCheckResult>().HasData(
                new MachineMaintenanceCheckResult { Id = 1, MachineId = 1, RuntimeHours = 2500, RuntimeThreshold = 500.0, CycleCount = 10000, CycleThreshold = 50000, RequiresMaintenance = false, Reason = "No maintenance required" },
                new MachineMaintenanceCheckResult { Id = 2, MachineId = 2, RuntimeHours = 5500, RuntimeThreshold = 400.0, CycleCount = 20000, CycleThreshold = 50000, RequiresMaintenance = true, Reason = "Overheating detected" }
            );
        }
    }
}

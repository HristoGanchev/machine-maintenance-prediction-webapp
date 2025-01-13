﻿// <auto-generated />
using System;
using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MaintenancePrediction.ApiService.Migrations
{
    [DbContext(typeof(MachineMaintenanceDbContext))]
    [Migration("20250113092654_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MaintenancePrediction.ApiService.Data.Models.MachineEvent", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId");

                    b.HasIndex("MachineId");

                    b.ToTable("MachineEvents");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            Description = "Routine maintenance performed",
                            EventCode = "E100",
                            MachineId = 1,
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EventId = 2,
                            Description = "Overheating detected",
                            EventCode = "E101",
                            MachineId = 2,
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MaintenancePrediction.ApiService.Data.Models.MachineMaintenanceCheckResult", b =>
                {
                    b.Property<int>("CycleCount")
                        .HasColumnType("int");

                    b.Property<int>("CycleThreshold")
                        .HasColumnType("int");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RequiresMaintenance")
                        .HasColumnType("bit");

                    b.Property<double>("RuntimeHours")
                        .HasColumnType("float");

                    b.Property<double>("RuntimeThreshold")
                        .HasColumnType("float");

                    b.ToTable("MachineMaintenanceChecks");
                });

            modelBuilder.Entity("MaintenancePrediction.ApiService.Data.Models.MachineUsage", b =>
                {
                    b.Property<int>("CycleCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<float>("RuntimeHours")
                        .HasColumnType("real");

                    b.HasIndex("MachineId");

                    b.ToTable("MachineUsages");
                });

            modelBuilder.Entity("MaintenancePrediction.ApiService.Models.MachineData", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MachineId"));

                    b.Property<int>("CycleCountThreshold")
                        .HasColumnType("int");

                    b.Property<double>("CycleTimeThreshold")
                        .HasColumnType("float");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RuntimeThreshold")
                        .HasColumnType("float");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");

                    b.HasData(
                        new
                        {
                            MachineId = 1,
                            CycleCountThreshold = 50000,
                            CycleTimeThreshold = 2000.0,
                            Location = "Factory Floor 2",
                            Name = "Hydraulic Press A1",
                            RuntimeThreshold = 500.0
                        },
                        new
                        {
                            MachineId = 2,
                            CycleCountThreshold = 50000,
                            CycleTimeThreshold = 1500.0,
                            Location = "Workshop 1",
                            Name = "CNC Lathe B1",
                            RuntimeThreshold = 400.0
                        });
                });

            modelBuilder.Entity("MaintenancePrediction.ApiService.Data.Models.MachineEvent", b =>
                {
                    b.HasOne("MaintenancePrediction.ApiService.Models.MachineData", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("MaintenancePrediction.ApiService.Data.Models.MachineUsage", b =>
                {
                    b.HasOne("MaintenancePrediction.ApiService.Models.MachineData", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using DrivingLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DrivingLog.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210503135536_timeSpanAlteration")]
    partial class timeSpanAlteration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DrivingLog.Models.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("QuantityCharged")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("TotalTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("LogEntries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinishDateTime = new DateTime(2020, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            QuantityCharged = 0m,
                            StartDateTime = new DateTime(2020, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalTime = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinishDateTime = new DateTime(2020, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            QuantityCharged = 0m,
                            StartDateTime = new DateTime(2020, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalTime = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinishDateTime = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            QuantityCharged = 0m,
                            StartDateTime = new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalTime = new TimeSpan(0, 0, 0, 0, 0)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

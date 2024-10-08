﻿// <auto-generated />
using System;
using IdealTimeTracker.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IdealTImeTracker.API.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20240723145314_empid")]
    partial class empid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IdealTImeTracker.API.Models.ApplicationConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("Value")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("ApplicationConfigurations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IDEAL TIME",
                            Value = new TimeSpan(0, 0, 5, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            Name = "WORKING TIME",
                            Value = new TimeSpan(0, 0, 5, 0, 0)
                        },
                        new
                        {
                            Id = 3,
                            Name = "SYNC TIME ONE",
                            Value = new TimeSpan(0, 4, 0, 0, 0)
                        },
                        new
                        {
                            Id = 4,
                            Name = "SYNC TIME TWO",
                            Value = new TimeSpan(0, 13, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("IdealTimeTracker.API.Models.User", b =>
                {
                    b.Property<string>("EmpId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportingTo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmpId");

                    b.HasIndex("ReportingTo");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            EmpId = "ADMIN",
                            Email = "admin@example.com",
                            IsActive = true,
                            Name = "Admin User",
                            PassWord = "ADMIN",
                            Role = "admin"
                        });
                });

            modelBuilder.Entity("IdealTimeTracker.API.Models.UserActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Activity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountPerDay")
                        .HasColumnType("int");

                    b.Property<int>("DurationInMins")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("UserActivities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activity = "none",
                            DurationInMins = 0,
                            IsActive = true
                        },
                        new
                        {
                            Id = 2,
                            Activity = "login",
                            DurationInMins = 0,
                            IsActive = true
                        },
                        new
                        {
                            Id = 3,
                            Activity = "logout",
                            DurationInMins = 0,
                            IsActive = true
                        },
                        new
                        {
                            Id = 4,
                            Activity = "Others",
                            DurationInMins = 0,
                            IsActive = true
                        },
                        new
                        {
                            Id = 5,
                            Activity = "tea break",
                            CountPerDay = 2,
                            DurationInMins = 15,
                            IsActive = true
                        },
                        new
                        {
                            Id = 6,
                            Activity = "lunch break",
                            CountPerDay = 2,
                            DurationInMins = 30,
                            IsActive = true
                        });
                });

            modelBuilder.Entity("IdealTimeTracker.API.Models.UserLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ActivityAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ActivityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("EmpId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("EmpId");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("IdealTimeTracker.API.Models.User", b =>
                {
                    b.HasOne("IdealTimeTracker.API.Models.User", "ReportingUser")
                        .WithMany()
                        .HasForeignKey("ReportingTo");

                    b.Navigation("ReportingUser");
                });

            modelBuilder.Entity("IdealTimeTracker.API.Models.UserLog", b =>
                {
                    b.HasOne("IdealTimeTracker.API.Models.UserActivity", "UserActivity")
                        .WithMany()
                        .HasForeignKey("ActivityId");

                    b.HasOne("IdealTimeTracker.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserActivity");
                });
#pragma warning restore 612, 618
        }
    }
}

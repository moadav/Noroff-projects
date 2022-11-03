﻿// <auto-generated />
using System;
using HvZ_API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HvZ_API.Migrations
{
    [DbContext(typeof(HvZDbEfContext))]
    partial class HvZDbEfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HvZ_API.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("ChatTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsHumanGlobal")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsZombieGlobal")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("SquadId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SquadId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("HvZ_API.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameConfigId")
                        .HasColumnType("int");

                    b.Property<bool>("Is_Started")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("nw_Lat")
                        .HasColumnType("float");

                    b.Property<double>("nw_Lng")
                        .HasColumnType("float");

                    b.Property<double>("se_Lat")
                        .HasColumnType("float");

                    b.Property<double>("se_Lng")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GameConfigId");

                    b.ToTable("Game");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "First Desc",
                            GameConfigId = 1,
                            Is_Started = false,
                            Name = "First Game",
                            StartTime = new DateTime(2022, 10, 21, 14, 5, 54, 574, DateTimeKind.Local).AddTicks(4338),
                            nw_Lat = 20.0,
                            nw_Lng = 20.0,
                            se_Lat = 20.0,
                            se_Lng = 20.0
                        },
                        new
                        {
                            Id = 2,
                            Description = "Second Desc",
                            GameConfigId = 2,
                            Is_Started = false,
                            Name = "Second Game",
                            StartTime = new DateTime(2022, 10, 21, 14, 5, 54, 574, DateTimeKind.Local).AddTicks(4388),
                            nw_Lat = 0.0,
                            nw_Lng = 20.0,
                            se_Lat = 20.0,
                            se_Lng = 20.0
                        });
                });

            modelBuilder.Entity("HvZ_API.Models.GameConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("ChatCooldown")
                        .HasColumnType("float");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<double>("HungerDuration")
                        .HasColumnType("float");

                    b.Property<int>("InitZombies")
                        .HasColumnType("int");

                    b.Property<int>("PlayerCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GameConfig");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChatCooldown = 20.0,
                            Duration = 12.0,
                            HungerDuration = 1.0,
                            InitZombies = 2,
                            PlayerCount = 64
                        },
                        new
                        {
                            Id = 2,
                            ChatCooldown = 0.0,
                            Duration = 24.0,
                            HungerDuration = 2.0,
                            InitZombies = 2,
                            PlayerCount = 64
                        });
                });

            modelBuilder.Entity("HvZ_API.Models.Gravestone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int?>("KillerId")
                        .HasColumnType("int");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<DateTime>("TimeOfDeath")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VictimId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("KillerId");

                    b.HasIndex("VictimId")
                        .IsUnique()
                        .HasFilter("[VictimId] IS NOT NULL");

                    b.ToTable("Gravestone");
                });

            modelBuilder.Entity("HvZ_API.Models.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<bool>("Is_Human_Visible")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Zombie_Visible")
                        .HasColumnType("bit");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Mission");
                });

            modelBuilder.Entity("HvZ_API.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BiteCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("CheckinLat")
                        .HasColumnType("float");

                    b.Property<double?>("CheckinLon")
                        .HasColumnType("float");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<double>("HungerTime")
                        .HasColumnType("float");

                    b.Property<bool>("IsHuman")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMuted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPatientZero")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SquadId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VictimId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("SquadId");

                    b.ToTable("Player");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BiteCode = "1",
                            FirstName = "adea",
                            GameId = 1,
                            HungerTime = 10.0,
                            IsHuman = true,
                            IsMuted = false,
                            IsPatientZero = true,
                            LastName = "123",
                            UserId = "test"
                        },
                        new
                        {
                            Id = 2,
                            BiteCode = "2",
                            FirstName = "yesbbb",
                            GameId = 1,
                            HungerTime = 10.0,
                            IsHuman = true,
                            IsMuted = false,
                            IsPatientZero = false,
                            LastName = "aaaff",
                            UserId = "test"
                        },
                        new
                        {
                            Id = 3,
                            BiteCode = "3",
                            FirstName = "arnold",
                            GameId = 1,
                            HungerTime = 10.0,
                            IsHuman = true,
                            IsMuted = false,
                            IsPatientZero = true,
                            LastName = "tesi",
                            UserId = "test"
                        });
                });

            modelBuilder.Entity("HvZ_API.Models.Squad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("MemberCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Squad");
                });

            modelBuilder.Entity("HvZ_API.Models.Chat", b =>
                {
                    b.HasOne("HvZ_API.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("HvZ_API.Models.Player", "Player")
                        .WithMany("Chats")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HvZ_API.Models.Squad", "Squad")
                        .WithMany()
                        .HasForeignKey("SquadId");

                    b.Navigation("Game");

                    b.Navigation("Player");

                    b.Navigation("Squad");
                });

            modelBuilder.Entity("HvZ_API.Models.Game", b =>
                {
                    b.HasOne("HvZ_API.Models.GameConfig", "GameConfig")
                        .WithMany()
                        .HasForeignKey("GameConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameConfig");
                });

            modelBuilder.Entity("HvZ_API.Models.Gravestone", b =>
                {
                    b.HasOne("HvZ_API.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HvZ_API.Models.Player", "Killer")
                        .WithMany("KillerStones")
                        .HasForeignKey("KillerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HvZ_API.Models.Player", "Victim")
                        .WithOne("Victim")
                        .HasForeignKey("HvZ_API.Models.Gravestone", "VictimId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Game");

                    b.Navigation("Killer");

                    b.Navigation("Victim");
                });

            modelBuilder.Entity("HvZ_API.Models.Mission", b =>
                {
                    b.HasOne("HvZ_API.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("HvZ_API.Models.Player", b =>
                {
                    b.HasOne("HvZ_API.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HvZ_API.Models.Squad", "Squad")
                        .WithMany()
                        .HasForeignKey("SquadId");

                    b.Navigation("Game");

                    b.Navigation("Squad");
                });

            modelBuilder.Entity("HvZ_API.Models.Squad", b =>
                {
                    b.HasOne("HvZ_API.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("HvZ_API.Models.Player", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("KillerStones");

                    b.Navigation("Victim");
                });
#pragma warning restore 612, 618
        }
    }
}
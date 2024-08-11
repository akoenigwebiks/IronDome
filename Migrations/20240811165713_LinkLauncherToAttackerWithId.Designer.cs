﻿// <auto-generated />
using System;
using IronDome.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IronDome.Migrations
{
    [DbContext(typeof(IronDomeContextV3))]
    [Migration("20240811165713_LinkLauncherToAttackerWithId")]
    partial class LinkLauncherToAttackerWithId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IronDome.Models.Ammo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDestroyed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLaunched")
                        .HasColumnType("bit");

                    b.Property<int>("LauncherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LauncherId");

                    b.ToTable("Ammo");
                });

            modelBuilder.Entity("IronDome.Models.Attacker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Attacker");
                });

            modelBuilder.Entity("IronDome.Models.Launcher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttackerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Range")
                        .HasColumnType("int");

                    b.Property<int>("Velocity")
                        .HasColumnType("int");

                    b.Property<int?>("VolleyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttackerId");

                    b.HasIndex("VolleyId");

                    b.ToTable("Launcher");
                });

            modelBuilder.Entity("IronDome.Models.Volley", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AttackerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LaunchDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AttackerId");

                    b.ToTable("Volley");
                });

            modelBuilder.Entity("IronDome.Models.Ammo", b =>
                {
                    b.HasOne("IronDome.Models.Launcher", "Launcher")
                        .WithMany("Ammo")
                        .HasForeignKey("LauncherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Launcher");
                });

            modelBuilder.Entity("IronDome.Models.Launcher", b =>
                {
                    b.HasOne("IronDome.Models.Attacker", "Attacker")
                        .WithMany()
                        .HasForeignKey("AttackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IronDome.Models.Volley", null)
                        .WithMany("Launchers")
                        .HasForeignKey("VolleyId");

                    b.Navigation("Attacker");
                });

            modelBuilder.Entity("IronDome.Models.Volley", b =>
                {
                    b.HasOne("IronDome.Models.Attacker", null)
                        .WithMany("Volleys")
                        .HasForeignKey("AttackerId");
                });

            modelBuilder.Entity("IronDome.Models.Attacker", b =>
                {
                    b.Navigation("Volleys");
                });

            modelBuilder.Entity("IronDome.Models.Launcher", b =>
                {
                    b.Navigation("Ammo");
                });

            modelBuilder.Entity("IronDome.Models.Volley", b =>
                {
                    b.Navigation("Launchers");
                });
#pragma warning restore 612, 618
        }
    }
}

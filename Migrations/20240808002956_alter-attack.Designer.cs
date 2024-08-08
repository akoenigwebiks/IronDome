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
    [DbContext(typeof(IronDomeContext))]
    [Migration("20240808002956_alter-attack")]
    partial class alterattack
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IronDome.Models.Attack", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ActiveID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInterceptedOrExploded")
                        .HasColumnType("bit");

                    b.Property<int>("ThreatSource")
                        .HasColumnType("int");

                    b.Property<int>("ThreatType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Attack");
                });

            modelBuilder.Entity("IronDome.Models.Defense", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Ammunition")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Defense");
                });
#pragma warning restore 612, 618
        }
    }
}

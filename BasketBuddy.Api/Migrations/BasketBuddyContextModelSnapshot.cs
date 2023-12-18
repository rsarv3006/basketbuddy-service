﻿// <auto-generated />
using System;
using System.Text.Json;
using BasketBuddy.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BasketBuddy.Api.Migrations
{
    [DbContext(typeof(BasketBuddyContext))]
    partial class BasketBuddyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BasketBuddy.Core.Models.Share", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<JsonDocument>("Data")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ShareCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.HasKey("Id");

                    b.HasIndex("ShareCode")
                        .IsUnique();

                    b.ToTable("Shares");
                });
#pragma warning restore 612, 618
        }
    }
}

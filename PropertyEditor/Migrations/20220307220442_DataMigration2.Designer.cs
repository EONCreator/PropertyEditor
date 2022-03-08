﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PropertyEditor.Models;

namespace PropertyEditor.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220307220442_DataMigration2")]
    partial class DataMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PropertyEditor.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("PropertyEditor.Models.IntegerProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("IntegerProperties");
                });

            modelBuilder.Entity("PropertyEditor.Models.IntegerValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ObjectId");

                    b.Property<int>("PropertyId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId");

                    b.HasIndex("PropertyId");

                    b.ToTable("IntegerValues");
                });

            modelBuilder.Entity("PropertyEditor.Models.Object", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Objects");
                });

            modelBuilder.Entity("PropertyEditor.Models.StringProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("StringProperties");
                });

            modelBuilder.Entity("PropertyEditor.Models.StringValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ObjectId");

                    b.Property<int>("PropertyId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId");

                    b.HasIndex("PropertyId");

                    b.ToTable("StringValues");
                });

            modelBuilder.Entity("PropertyEditor.Models.IntegerProperty", b =>
                {
                    b.HasOne("PropertyEditor.Models.Category")
                        .WithMany("IntegerProperties")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("PropertyEditor.Models.IntegerValue", b =>
                {
                    b.HasOne("PropertyEditor.Models.Object")
                        .WithMany("IntegerValues")
                        .HasForeignKey("ObjectId");

                    b.HasOne("PropertyEditor.Models.IntegerProperty", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PropertyEditor.Models.Object", b =>
                {
                    b.HasOne("PropertyEditor.Models.Category", "Category")
                        .WithMany("Objects")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PropertyEditor.Models.StringProperty", b =>
                {
                    b.HasOne("PropertyEditor.Models.Category")
                        .WithMany("StringProperties")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("PropertyEditor.Models.StringValue", b =>
                {
                    b.HasOne("PropertyEditor.Models.Object")
                        .WithMany("StringValues")
                        .HasForeignKey("ObjectId");

                    b.HasOne("PropertyEditor.Models.StringProperty", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

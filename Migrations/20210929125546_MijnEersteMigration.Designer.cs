﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using wpfwopdracht5;

namespace wpfwopdracht5.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210929125546_MijnEersteMigration")]
    partial class MijnEersteMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("wpfwopdracht5.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Naam")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Studenten");
                });
#pragma warning restore 612, 618
        }
    }
}
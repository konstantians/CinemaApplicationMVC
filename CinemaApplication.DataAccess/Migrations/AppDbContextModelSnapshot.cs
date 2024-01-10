﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApplication.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaApplication.SharedModels.CinemaRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Supports3D")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CinemaRooms");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.MovieProjection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AirDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CinemaRoomId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("SeatsLeft")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinemaRoomId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieProjections");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieProjectionId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("ReservationRefoundPrice")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieProjectionId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.MovieProjection", b =>
                {
                    b.HasOne("CinemaApplication.SharedModels.CinemaRoom", "CinemaRoom")
                        .WithMany("Projections")
                        .HasForeignKey("CinemaRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaApplication.SharedModels.Movie", "Movie")
                        .WithMany("Projections")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinemaRoom");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.Reservation", b =>
                {
                    b.HasOne("CinemaApplication.SharedModels.MovieProjection", "MovieProjection")
                        .WithMany("Reservations")
                        .HasForeignKey("MovieProjectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieProjection");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.CinemaRoom", b =>
                {
                    b.Navigation("Projections");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.Movie", b =>
                {
                    b.Navigation("Projections");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.MovieProjection", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}

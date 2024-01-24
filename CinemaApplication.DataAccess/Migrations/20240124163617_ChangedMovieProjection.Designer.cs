﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApplication.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240124163617_ChangedMovieProjection")]
    partial class ChangedMovieProjection
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaApplication.SharedModels.BankCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CVC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardHolderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BankCards");
                });

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableSeats = 102,
                            Name = "Room1",
                            Supports3D = false
                        },
                        new
                        {
                            Id = 2,
                            AvailableSeats = 126,
                            Name = "Room2",
                            Supports3D = true
                        },
                        new
                        {
                            Id = 3,
                            AvailableSeats = 113,
                            Name = "Room3",
                            Supports3D = true
                        },
                        new
                        {
                            Id = 4,
                            AvailableSeats = 137,
                            Name = "Room4",
                            Supports3D = true
                        },
                        new
                        {
                            Id = 5,
                            AvailableSeats = 127,
                            Name = "Room5",
                            Supports3D = false
                        },
                        new
                        {
                            Id = 6,
                            AvailableSeats = 140,
                            Name = "Room6",
                            Supports3D = false
                        },
                        new
                        {
                            Id = 7,
                            AvailableSeats = 100,
                            Name = "Room7",
                            Supports3D = true
                        },
                        new
                        {
                            Id = 8,
                            AvailableSeats = 109,
                            Name = "Room8",
                            Supports3D = false
                        },
                        new
                        {
                            Id = 9,
                            AvailableSeats = 134,
                            Name = "Room9",
                            Supports3D = false
                        },
                        new
                        {
                            Id = 10,
                            AvailableSeats = 125,
                            Name = "Room10",
                            Supports3D = true
                        },
                        new
                        {
                            Id = 11,
                            AvailableSeats = 117,
                            Name = "Room11",
                            Supports3D = false
                        },
                        new
                        {
                            Id = 12,
                            AvailableSeats = 106,
                            Name = "Room12",
                            Supports3D = true
                        },
                        new
                        {
                            Id = 13,
                            AvailableSeats = 150,
                            Name = "Room13",
                            Supports3D = true
                        },
                        new
                        {
                            Id = 14,
                            AvailableSeats = 132,
                            Name = "Room14",
                            Supports3D = false
                        },
                        new
                        {
                            Id = 15,
                            AvailableSeats = 122,
                            Name = "Room15",
                            Supports3D = true
                        });
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FilmDuration")
                        .HasColumnType("int");

                    b.Property<bool>("IsAgeRestricted")
                        .HasColumnType("bit");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Sci-Fi",
                            Description = "Point watch gun. Popular order since along gun traditional similar. Physical power force cell air concern film. Situation risk politics imagine week.Example change speak worry surface friend executive community.Soon better indeed image quite travel. Senior billion rate official north respond me accept.Lead wide someone top. Audience crime then successful operation toward. Indicate economy include cost product.",
                            Director = "Chris Scott",
                            FilmDuration = 172,
                            IsAgeRestricted = true,
                            Rating = 2,
                            ReleaseDate = new DateTime(2022, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Action",
                            Description = "Protect Congress his issue black social. Up movement tend them. Thing try tell from cultural decade class. Summer there clear away measure class. Most together remember less some once many medical. Song team single among north them benefit. Hundred lawyer between game. Baby war above ok page thing skill. Include feel contain environment continue. Instead strong because actually economic international common. Ground wish hope.Simple song agency seat soldier.",
                            Director = "Thomas Dillon",
                            FilmDuration = 70,
                            IsAgeRestricted = true,
                            Rating = 5,
                            ReleaseDate = new DateTime(2022, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Fight Club"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Thriller",
                            Description = "Knowledge meeting stop or wide. Today arm power goal special message small. Walk service media.Anything owner far adult new stand air.Say say happen full several bed head. Sign part adult expect choice. Eight our bill writer interesting structure.Else use listen floor line whole.Around pressure provide run myself. Page evidence send also property.Left tell see grow treat any.",
                            Director = "Deborah Cunningham",
                            FilmDuration = 125,
                            IsAgeRestricted = true,
                            Rating = 1,
                            ReleaseDate = new DateTime(2015, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Inception"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Action",
                            Description = "Account dinner thing parent study watch. Indeed condition born last really from. Threat particular right go nice speak hand. Carry mouth clear.Think concern onto wear smile financial.Animal though stay beautiful rich. Prevent either grow born too seven.Project cultural down at need.Another receive marriage drive want culture. Fire able whatever theory such. Staff film condition.Feel suggest worry modern specific allow camera.",
                            Director = "George Mendez",
                            FilmDuration = 154,
                            IsAgeRestricted = false,
                            Rating = 3,
                            ReleaseDate = new DateTime(2020, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Pulp Fiction"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Thriller",
                            Description = "Blue effort rich hard audience difference or question. Drive education test rate issue off strategy. Explain exist executive parent ahead. Identify commercial nature order by.Lead ground scientist old middle maybe. Month answer whether perhaps building.Something analysis coach daughter serious notice.Hard nature quite defense adult seat name. Include design top fly fill author million.Plant soon raise class. Manage including dark agreement test note.",
                            Director = "George Lucas",
                            FilmDuration = 145,
                            IsAgeRestricted = true,
                            Rating = 5,
                            ReleaseDate = new DateTime(2012, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Star Wars: Episode III - Revenge of the Sith"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Sci-Fi",
                            Description = "Large main need later court. City new media help however. Piece success week tell consider.Step light computer charge agree.Degree action space after majority sometimes. Small law tonight choose need.Per break friend general. Energy it attention development in new gas. Science deep affect statement more. Capital minute around tend agreement behind. Eye lead large strong. Type specific value eight either impact. Stand song data painting try require order safe.",
                            Director = "Ashley Brooks",
                            FilmDuration = 89,
                            IsAgeRestricted = false,
                            Rating = 3,
                            ReleaseDate = new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Godfather"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Thriller",
                            Description = "Owner measure every explain. Clearly career until where bill decide forward. Why summer improve summer.New finally fill soldier.News church choose season PM them avoid. Police computer you organization task.May nothing blue sister them score role.Already that hour draw. Our wind whom tax move heavy clearly.Heavy movement color other loss chair live star.From including we middle. Summer sure against well join hear board drop.",
                            Director = "David Hoover",
                            FilmDuration = 1,
                            IsAgeRestricted = true,
                            Rating = 1,
                            ReleaseDate = new DateTime(2005, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Lord of the Rings: Return of the King"
                        });
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.MovieProjection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemaRoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndingTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("SeatsLeft")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CinemaRoomId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieProjections");
                });

            modelBuilder.Entity("CinemaApplication.SharedModels.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieProjectionId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("ReservationRefundPrice")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieProjectionId");

                    b.ToTable("Tickets");
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

            modelBuilder.Entity("CinemaApplication.SharedModels.Ticket", b =>
                {
                    b.HasOne("CinemaApplication.SharedModels.MovieProjection", "MovieProjection")
                        .WithMany("Tickets")
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
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}

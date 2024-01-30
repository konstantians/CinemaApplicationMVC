using CinemaApplication.SharedModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=CinemaAppDataDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
            options => options.EnableRetryOnFailure());
    }
    
    public DbSet<CinemaRoom> CinemaRooms { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieProjection> MovieProjections { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<BankCard> BankCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaRoom>().HasKey(cinemaRoom => cinemaRoom.Id);
        modelBuilder.Entity<Movie>().HasKey(movie => movie.Id);
        modelBuilder.Entity<MovieProjection>().HasKey(movieProjection => movieProjection.Id);
        modelBuilder.Entity<Ticket>().HasKey(reservation => reservation.Id);
        modelBuilder.Entity<BankCard>().HasKey(bankCard => bankCard.Id);

        modelBuilder.Entity<MovieProjection>()
            .HasOne(projection => projection.Movie)
            .WithMany(projection => projection.Projections)
            .HasForeignKey(projection => projection.MovieId);

        modelBuilder.Entity<MovieProjection>()
            .HasOne(projection => projection.CinemaRoom)
            .WithMany(projection => projection.Projections)
            .HasForeignKey(projection => projection.CinemaRoomId);

        modelBuilder.Entity<Movie>().HasData(
        new Movie() { 
            Id = 1,
            Title = "Avengers: Endgame",
            Description = "Point watch gun. Popular order since along gun traditional similar. Physical power force cell air concern film." +
            " Situation risk politics imagine week.Example change speak worry surface friend executive community.Soon better indeed image quite travel." +
            " Senior billion rate official north respond me accept.Lead wide someone top. Audience crime then successful operation toward. " +
            "Indicate economy include cost product.",
            Director = "Chris Scott",
            Category = "Sci-Fi",
            ReleaseDate = new DateTime(2022,8,27),
            Rating = 2,
            IsAgeRestricted = true,
            FilmDuration = 172
        },
        new Movie()
        {
            Id = 2,
            Title = "Fight Club",
            Description = "Protect Congress his issue black social. Up movement tend them. Thing try tell from cultural decade class. " +
            "Summer there clear away measure class. Most together remember less some once many medical. " +
            "Song team single among north them benefit. " +
            "Hundred lawyer between game. Baby war above ok page thing skill. " +
            "Include feel contain environment continue. Instead strong because actually economic international common. " +
            "Ground wish hope.Simple song agency seat soldier.",
            Director = "Thomas Dillon",
            Category = "Action",
            ReleaseDate = new DateTime(2022,7,27),
            Rating = 5,
            IsAgeRestricted = true,
            FilmDuration = 70
        },
        new Movie()
        {
            Id = 3,
            Title = "Inception",
            Description = "Knowledge meeting stop or wide. Today arm power goal special message small. " +
            "Walk service media.Anything owner far adult new stand air.Say say happen full several bed head. " +
            "Sign part adult expect choice. " +
            "Eight our bill writer interesting structure.Else use listen floor line whole.Around pressure provide run myself. " +
            "Page evidence send also property.Left tell see grow treat any.",
            Director = "Deborah Cunningham",
            Category = "Thriller",
            ReleaseDate = new DateTime(2015, 6, 6),
            Rating = 1,
            IsAgeRestricted = true,
            FilmDuration = 125
        },
        new Movie()
        {
            Id = 4,
            Title = "Pulp Fiction",
            Description = "Account dinner thing parent study watch. Indeed condition born last really from. Threat particular right go nice speak hand. " +
            "Carry mouth clear.Think concern onto wear smile financial.Animal though stay beautiful rich. " +
            "Prevent either grow born too seven.Project cultural down at need.Another receive marriage drive want culture. " +
            "Fire able whatever theory such. " +
            "Staff film condition.Feel suggest worry modern specific allow camera.",
            Director = "George Mendez",
            Category = "Action",
            ReleaseDate = new DateTime(2020, 7, 10),
            Rating = 3,
            IsAgeRestricted = false,
            FilmDuration = 154
        },
        new Movie()
        {
            Id = 5,
            Title = "Star Wars: Episode III - Revenge of the Sith",
            Description = "Blue effort rich hard audience difference or question. Drive education test rate issue off strategy. Explain exist executive parent ahead. " +
            "Identify commercial nature order by.Lead ground scientist old middle maybe. " +
            "Month answer whether perhaps building.Something analysis coach daughter serious notice.Hard nature quite defense adult seat name. " +
            "Include design top fly fill author million.Plant soon raise class. Manage including dark agreement test note.",
            Director = "George Lucas",
            Category = "Thriller",
            ReleaseDate = new DateTime(2012, 8, 4),
            Rating = 5,
            IsAgeRestricted = true,
            FilmDuration = 145
        },
        new Movie()
        {
            Id = 6,
            Title = "The Godfather",
            Description = "Large main need later court. City new media help however. " +
            "Piece success week tell consider.Step light computer charge agree.Degree action space after majority sometimes. " +
            "Small law tonight choose need.Per break friend general. Energy it attention development in new gas. " +
            "Science deep affect statement more. Capital minute around tend agreement behind. Eye lead large strong. " +
            "Type specific value eight either impact. Stand song data painting try require order safe.",
            Director = "Ashley Brooks",
            Category = "Sci-Fi",
            ReleaseDate = new DateTime(2023, 3, 21),
            Rating = 3,
            IsAgeRestricted = false,
            FilmDuration = 89
        },
        new Movie()
        {
            Id = 7,
            Title = "The Lord of the Rings: Return of the King",
            Description = "Owner measure every explain. Clearly career until where bill decide forward. " +
            "Why summer improve summer.New finally fill soldier.News church choose season PM them avoid. " +
            "Police computer you organization task.May nothing blue sister them score role.Already that hour draw. " +
            "Our wind whom tax move heavy clearly.Heavy movement color other loss chair live star.From including we middle. " +
            "Summer sure against well join hear board drop.",
            Director = "David Hoover",
            Category = "Thriller",
            ReleaseDate = new DateTime(2005, 1, 12),
            Rating = 1,
            IsAgeRestricted = true,
            FilmDuration = 1
        });

        modelBuilder.Entity<CinemaRoom>().HasData(
        new CinemaRoom()
        {
            Id = 1,
            Name = "Room1",
            AvailableSeats = 102,
            Supports3D = false
        },
        new CinemaRoom()
        {
            Id = 2,
            Name = "Room2",
            AvailableSeats = 126,
            Supports3D = true
        },
        new CinemaRoom()
        {
            Id = 3,
            Name = "Room3",
            AvailableSeats = 113,
            Supports3D = true
        },
        new CinemaRoom()
        {
            Id = 4,
            Name = "Room4",
            AvailableSeats = 137,
            Supports3D = true
        },
        new CinemaRoom()
        {
            Id = 5,
            Name = "Room5",
            AvailableSeats = 127,
            Supports3D = false
        },
        new CinemaRoom()
        {
            Id = 6,
            Name = "Room6",
            AvailableSeats = 140,
            Supports3D = false
        },
        new CinemaRoom()
        {
            Id = 7,
            Name = "Room7",
            AvailableSeats = 100,
            Supports3D = true
        },
        new CinemaRoom()
        {
            Id = 8,
            Name = "Room8",
            AvailableSeats = 109,
            Supports3D = false
        },
        new CinemaRoom()
        {
            Id = 9,
            Name = "Room9",
            AvailableSeats = 134,
            Supports3D = false
        },
        new CinemaRoom()
        {
            Id = 10,
            Name = "Room10",
            AvailableSeats = 125,
            Supports3D = true
        },
        new CinemaRoom()
        {
            Id = 11,
            Name = "Room11",
            AvailableSeats = 117,
            Supports3D = false
        },
        new CinemaRoom()
        {
            Id = 12,
            Name = "Room12",
            AvailableSeats = 106,
            Supports3D = true
        },
        new CinemaRoom()
        {
            Id = 13,
            Name = "Room13",
            AvailableSeats = 150,
            Supports3D = true
        },
        new CinemaRoom()
        {
            Id = 14,
            Name = "Room14",
            AvailableSeats = 132,
            Supports3D = false
        },
        new CinemaRoom()
        {
            Id = 15,
            Name = "Room15",
            AvailableSeats = 122,
            Supports3D = true
        });

        base.OnModelCreating(modelBuilder);
    }
}


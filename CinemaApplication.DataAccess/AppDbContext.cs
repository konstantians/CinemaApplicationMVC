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
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaRoom>().HasKey(cinemaRoom => cinemaRoom.Id);
        modelBuilder.Entity<Movie>().HasKey(movie => movie.Id);
        modelBuilder.Entity<MovieProjection>().HasKey(movieProjection => movieProjection.Id);
        modelBuilder.Entity<Reservation>().HasKey(reservation => reservation.Id);

        modelBuilder.Entity<MovieProjection>()
            .HasOne(projection => projection.Movie)
            .WithMany(projection => projection.Projections)
            .HasForeignKey(projection => projection.MovieId);

        modelBuilder.Entity<MovieProjection>()
            .HasOne(projection => projection.CinemaRoom)
            .WithMany(projection => projection.Projections)
            .HasForeignKey(projection => projection.CinemaRoomId);

        modelBuilder.Entity<Reservation>()
            .HasOne(reservation => reservation.MovieProjection)
            .WithMany(reservation => reservation.Reservations)
            .HasForeignKey(projection => projection.MovieProjectionId);

        base.OnModelCreating(modelBuilder);
    }
}


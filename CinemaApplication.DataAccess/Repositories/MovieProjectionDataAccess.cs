﻿using CinemaApplication.SharedModels;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.DataAccess.Repositories;

public class MovieProjectionDataAccess : IMovieProjectionDataAccess
{
    private readonly AppDbContext _context;
    private readonly ITicketDataAccess _reservationDataAccess;
    public MovieProjectionDataAccess(AppDbContext context, ITicketDataAccess reservationDataAccess)
    {
        _context = context;
        _reservationDataAccess = reservationDataAccess;
    }

    public async Task<IEnumerable<MovieProjection>> GetMovieProjectionsAsync()
    {
        try
        {
            return await _context.MovieProjections
                .Include(projection => projection.Tickets)
                .Include(projection => projection.CinemaRoom)
                .Include(projection => projection.Movie)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<MovieProjection> GetMovieProjectionAsync(int id)
    {
        try
        {
            return await _context.MovieProjections
                .Include(projection => projection.Tickets)
                .Include(projection => projection.CinemaRoom)
                .Include(projection => projection.Movie)
                .FirstOrDefaultAsync(projection => projection.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<int> CreateMovieProjectionAsync(MovieProjection movie)
    {
        try
        {
            var result = await _context.MovieProjections.AddAsync(movie);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task UpdateMovieProjectionAsync(int id, MovieProjection projection)
    {
        try
        {
            MovieProjection foundProjection = await GetMovieProjectionAsync(id);
            if (foundProjection is null)
                return;

            foundProjection.SeatsLeft = projection.SeatsLeft;
            foundProjection.StartingTime = projection.StartingTime;
            foundProjection.EndingTime = projection.EndingTime;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task DeleteMovieProjectionAsync(int id)
    {
        try
        {
            MovieProjection foundMovieProjection = await GetMovieProjectionAsync(id);
            foreach (Ticket reservation in foundMovieProjection.Tickets)
            {
                await _reservationDataAccess.DeleteReservationAsync(reservation.Id);
            }
            _context.MovieProjections.Remove(foundMovieProjection);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}

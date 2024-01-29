using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.DataAccess.Repositories;
using CinemaApplication.MVC.Models;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApplication.MVC.Controllers;

[Authorize(Roles = "ContentAdmin")]
public class ContentAdminController : Controller
{
    private readonly IMovieDataAccess _movieDataAccess;
    private readonly ICinemaRoomDataAccess _cinemaRoomDataAccess;
    private readonly IMovieProjectionDataAccess _movieProjectionDataAccess;

    public ContentAdminController(IMovieDataAccess movieDataAccess, ICinemaRoomDataAccess cinemaRoomDataAccess,
        IMovieProjectionDataAccess movieProjectionDataAccess, IAuthenticationProcedures authenticationProcedures)
    {
        _movieDataAccess = movieDataAccess;
        _cinemaRoomDataAccess = cinemaRoomDataAccess;
        _movieProjectionDataAccess = movieProjectionDataAccess;
    }

    public async Task<IActionResult> ManageMovies(bool movieDeletionSuccess, bool movieDeletionFailure, 
        bool movieCreationSuccess)
    {
        var result = await _movieDataAccess.GetMoviesAsync();
        ViewData["MovieDeletionSuccess"] = movieDeletionSuccess;
        ViewData["MovieDeletionFailure"] = movieDeletionFailure;

        ViewData["MovieCreationSuccess"] = movieCreationSuccess;

        return View(result.ToList());
    }

    [HttpGet]
    public async Task<IActionResult> CreateMovie(bool duplicateMovieTitleError, bool movieCreationFailure)
    {
        var foundCinemaRooms = await _cinemaRoomDataAccess.GetCinemaRoomsAsync();

        List<CinemaRoom> cinemaRooms = foundCinemaRooms.ToList();
        Movie movie = new Movie();
        CreateMovieViewModel movieAndCinemaRoomsModel = new()
        {
            CinemaRooms = cinemaRooms,
            Movie = movie
        };

        ViewData["DuplicateMovieTitleError"] = duplicateMovieTitleError;
        ViewData["MovieCreationFailure"] = movieCreationFailure;

        return View(movieAndCinemaRoomsModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(Movie movie, Dictionary<string, string> formData)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Movie foundMovie = await _movieDataAccess.GetMovieByTitleAsync(movie.Title!);
        if (foundMovie is not null)
            return RedirectToAction("CreateMovie", "ContentAdmin", new { duplicateMovieTitleError = true });

        //from all the names pick those that start with projection_
        var projectionEntries = formData
        .Where(kv => kv.Key.StartsWith("projection_"))
        .ToDictionary(kv => kv.Key, kv => kv.Value);

        foreach (string parameterName in projectionEntries.Keys)
        {
            MovieProjection projectionDataModel = new MovieProjection();

            // Split parameter based on "_"
            string[] parameterParts = parameterName.Split('_');

            // Separate the year, month, and day
            string[] parameterDate = parameterParts[1].Split('-');

            // Separate the hour and minutes
            string[] parameterStartingHour = parameterParts[2].Split(':');
            string[] parameterEndingHour = parameterParts[3].Split(':');

            DateTime date = new DateTime(
                int.Parse(parameterDate[0]),
                int.Parse(parameterDate[1]),
                int.Parse(parameterDate[2]));

            TimeSpan startingHour = new TimeSpan(
                int.Parse(parameterStartingHour[0]),
                int.Parse(parameterStartingHour[1]),
                0);

            TimeSpan endingHour = new TimeSpan(
                int.Parse(parameterEndingHour[0]),
                int.Parse(parameterEndingHour[1]),
                0);

            projectionDataModel.StartingTime = date.Add(startingHour);
            projectionDataModel.EndingTime = date.Add(endingHour);
            projectionDataModel.CinemaRoomId = int.Parse(parameterParts[4]);
            projectionDataModel.SeatsLeft = int.Parse(parameterParts[5]);

            movie.Projections.Add(projectionDataModel);
        }

        int movieId = await _movieDataAccess.CreateMovieAsync(movie);
        if (movieId == -1)
        {
            return RedirectToAction("CreateMovie", "ContentAdmin", new { movieCreationFailure = true });
        }

        return RedirectToAction("ManageMovies", "ContentAdmin", new { movieCreationSuccess = true});
    }

    [HttpGet]
    public async Task<IActionResult> EditMovie(int movieId, 
        bool movieProjectionDeletionSuccess, bool movieProjectionDeletionFailure,
        bool movieUpdateSuccess, bool movieUpdateFailure,
        bool movieProjectionCreationSuccess, bool movieProjectionCreationFailure)
    {
        Movie foundMovie = await _movieDataAccess.GetMovieAsync(movieId);
        var movieProjection = new MovieProjection();
        var result = await _cinemaRoomDataAccess.GetCinemaRoomsAsync();
        List<CinemaRoom> cinemaRooms = result.ToList();
        EditMovieViewModel movieProjectionAndCinemaRoomsModel = new()
        {
            Projection = movieProjection,
            Movie = foundMovie,
            CinemaRooms = cinemaRooms
        };

        ViewData["MovieProjectionDeletionSuccess"] = movieProjectionDeletionSuccess;
        ViewData["MovieProjectionDeletionFailure"] = movieProjectionDeletionFailure;
        ViewData["MovieUpdateSuccess"] = movieUpdateSuccess;
        ViewData["MovieUpdateFailure"] = movieUpdateFailure;
        ViewData["MovieProjectionCreationSuccess"] = movieProjectionCreationSuccess;
        ViewData["MovieProjectionCreationFailure"] = movieProjectionCreationFailure;

        return View(movieProjectionAndCinemaRoomsModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditMovie(Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return View(movie.Id);
        }

        bool result = await _movieDataAccess.UpdateMovieAsync(movie.Id, movie);
        if (!result)
            return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = movie.Id, movieUpdateFailure = true });

        return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = movie.Id, movieUpdateSuccess = true });
    }

    public async Task<IActionResult> DeleteMovie(int movieId)
    {
        Movie movie = await _movieDataAccess.GetMovieAsync(movieId);
        if (movie is null)
            return RedirectToAction("ManageMovies", "ContentAdmin", new { movieDeletionFailure = true });

        bool result = await _movieDataAccess.DeleteMovieAsync(movieId);
        if (!result)
            return RedirectToAction("ManageMovies", "ContentAdmin", new { movieDeletionFailure = true });

        return RedirectToAction("ManageMovies", "ContentAdmin", new { movieDeletionSuccess = true });
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjection(MovieProjection projection)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = projection.MovieId, movieProjectionCreationFailure = true });

        CinemaRoom cinemaRoom = await _cinemaRoomDataAccess.GetCinemaRoomAsync(projection.CinemaRoomId);
        if(cinemaRoom is null)
            return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = projection.MovieId, movieProjectionCreationFailure = true });

        projection.SeatsLeft = cinemaRoom.AvailableSeats;

        int projectionId = await _movieProjectionDataAccess.CreateMovieProjectionAsync(projection);
        if(projectionId == -1)
            return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = projection.MovieId, movieProjectionCreationFailure = true });

        return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = projection.MovieId, movieProjectionCreationSuccess = true });
    }

    public async Task<IActionResult> DeleteProjection(int projectionId, int movieId)
    {
        MovieProjection projection = await _movieProjectionDataAccess.GetMovieProjectionAsync(projectionId);
        if (projection is null)
            return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = movieId, movieProjectionDeletionFailure = true });

        bool result = await _movieProjectionDataAccess.DeleteMovieProjectionAsync(projectionId);
        if (!result)
            return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = movieId, movieProjectionDeletionFailure = true });

        return RedirectToAction("EditMovie", "ContentAdmin", new { movieId = movieId, movieProjectionDeletionSuccess = true });
    }

}

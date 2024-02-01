using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.DataAccess.Repositories;
using CinemaApplication.MVC.Models;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaApplication.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieDataAccess _movieDataAccess;
        private readonly IBankCardDataAccess _bankCardDataAccess;
        private readonly IAuthenticationProcedures _authenticationProcedures;

        public HomeController(IMovieDataAccess movieDataAccess, IBankCardDataAccess bankCardDataAccess,
            IAuthenticationProcedures authenticationProcedures)
        {
            _movieDataAccess = movieDataAccess;
            _bankCardDataAccess = bankCardDataAccess;
            _authenticationProcedures = authenticationProcedures;
        }

        [AllowAnonymous]
        public IActionResult Index(bool failedAccountActivation, bool successfulAccountActivation, bool successfulSignIn, 
            bool successfulPasswordReset, bool failedPasswordReset)
        {
            ViewData["FailedAccountActivation"] = failedAccountActivation;
            ViewData["SuccessfulAccountActivation"] = successfulAccountActivation;
            ViewData["SuccessfulSignIn"] = successfulSignIn;
            ViewData["SuccessfulPasswordReset"] = successfulPasswordReset;
            ViewData["FailedPasswordReset"] = failedPasswordReset;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public async Task<IActionResult> CinemaProgram(bool createTicketFailure, bool createTicketSuccess, bool noSeatsLeft)
        {
            var result = await _movieDataAccess.GetMoviesAsync();
            List<Movie> movies = result.ToList();
            
            AppUser user = await _authenticationProcedures.GetCurrentUserAsync();
            IEnumerable<BankCard> result2;
            if (user is null)
            {
                result2 = null;
            }
            else
            {
                result2 = await _bankCardDataAccess.GetBankCardsOfUserAsync(user.Id);
            }
            List<BankCard> bankCards = result2 is not null ? result2.ToList() : null;

            CinemaProgramViewModel cinemaProgramViewModel = new()
            {
                UserId = user is null ? null : user!.Id, 
                Movies = movies,
                BankCards = bankCards!
            };

            ViewData["CreateTicketSuccess"] = createTicketSuccess;
            ViewData["CreateTicketFailure"] = createTicketFailure;
            ViewData["NoSeatsLeft"] = noSeatsLeft;

            return View(cinemaProgramViewModel);
        }
    }
}
using CinemaApplication.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaApplication.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
    }
}
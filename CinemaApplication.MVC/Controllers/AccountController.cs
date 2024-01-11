using AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization.Authorization;
using CinemaApplication.MVC.Models;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApplication.MVC.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticationProcedures _authenticationProcedures;
    private readonly IAuthorizationProcedures _authorizationProcedures;

    public AccountController(IAuthenticationProcedures authenticationProcedures, IAuthorizationProcedures authorizationProcedures)
    {
        _authenticationProcedures = authenticationProcedures;
        _authorizationProcedures = authorizationProcedures;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(RegisterModel registerModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser appUser = new AppUser();
        appUser.UserName = registerModel.Username;
        appUser.PhoneNumber = registerModel.PhoneNumber;
        appUser.Email = registerModel.Email;

        await _authenticationProcedures.RegisterUserAsync(appUser, registerModel.Password, false, registerModel.AccountType);
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(SignInModel signInModel)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }

        await _authenticationProcedures.SignInUserAsync(signInModel.Username, signInModel.Password, signInModel.RememberMe);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _authenticationProcedures.LogOutUserAsync();
        return RedirectToAction("Index", "Home");
    }
}

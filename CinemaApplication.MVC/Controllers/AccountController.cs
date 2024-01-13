using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization.Authorization;
using CinemaApplication.EmailServiceLibrary;
using CinemaApplication.MVC.Models;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CinemaApplication.MVC.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticationProcedures _authenticationProcedures;
    private readonly IAuthorizationProcedures _authorizationProcedures;
    private readonly IEmailService _emailService;

    public AccountController(IAuthenticationProcedures authenticationProcedures, IAuthorizationProcedures authorizationProcedures,
        IEmailService emailService)
    {
        _authenticationProcedures = authenticationProcedures;
        _authorizationProcedures = authorizationProcedures;
        _emailService = emailService;
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

        (string userId, string confirmationToken) = await _authenticationProcedures.RegisterUserAsync(appUser, registerModel.Password!, 
            false, registerModel.AccountType!);
        //maybe do a check here
        string message = "Click on the following link to confirm your email:";
        string? link = Url.Action("ConfirmEmail", "Account", new { userId = WebUtility.UrlEncode(userId), token = WebUtility.UrlEncode(confirmationToken) }, Request.Scheme);
        string? confirmationLink = $"{message} {link}";
        ViewData["EmailSendSuccessfully"] = await _emailService.SendEmailAsync(appUser.Email, "Email Confirmation", confirmationLink);

        return View("RegisterVerificationEmailMessage");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult SignIn(bool falseResetAccount, bool invalidCredentials)
    {
        ViewData["FalseResetAccount"] = falseResetAccount;
        ViewData["InvalidCredentials"] = invalidCredentials;
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

        bool result = await _authenticationProcedures.SignInUserAsync(signInModel.Username!, signInModel.Password!, signInModel.RememberMe);
        if (!result)
        {
            ViewData["FalseResetAccount"] = false;
            ViewData["InvalidCredentials"] = true;
            return View();
        }

        return RedirectToAction("Index", "Home" , new {SuccessfulSignIn = true});
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _authenticationProcedures.LogOutUserAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        bool succeeded = await _authenticationProcedures.ConfirmEmailAsync(userId, WebUtility.UrlDecode(token));

        if (!succeeded)
        {
            return RedirectToAction("Index", "Home", new { FailedAccountActivation = true });
        }
        return RedirectToAction("Index", "Home", new { SuccessfulAccountActivation = true });
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(string userId, string token)
    {
        AppUser appUser = await _authenticationProcedures.FindByUserIdAsync(userId);
        if(appUser is null)
        {
            //Add error here
            return RedirectToAction("Index", "Home");
        }

        ViewData["Username"] = appUser.UserName;
        ViewData["UserId"] = userId;
        ViewData["Token"] = token;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        bool succeeded = await _authenticationProcedures.ResetPasswordAsync(
            resetPasswordModel.UserId, WebUtility.UrlDecode(resetPasswordModel.Token), resetPasswordModel.Password);


        if (!succeeded)
        {
            return RedirectToAction("Index", "Home", new { FailedPasswordReset = true });
        }
        return RedirectToAction("Index", "Home", new { SuccessfulPasswordReset = true });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(string username, string email)
    {
        AppUser appUser;
        if(username is null)
        {
            appUser = await _authenticationProcedures.FindByEmailAsync(email);
        }
        else
        {
            appUser = await _authenticationProcedures.FindByUsernameAsync(username);
        }

        if(appUser is null)
        {
            return RedirectToAction("SignIn", "Account", new { falseResetAccount = true });
        }

        string resetToken = await _authenticationProcedures.CreateResetPasswordTokenAsync(appUser);

        //maybe do a check here
        string message = "Click on the following link to reset your account password:";
        string? link = Url.Action("ResetPassword", "Account", new { userId = WebUtility.UrlEncode(appUser.Id), 
            token = WebUtility.UrlEncode(resetToken) }, Request.Scheme);
        string? confirmationLink = $"{message} {link}";
        ViewData["EmailSendSuccessfully"] = await _emailService.SendEmailAsync(appUser.Email, "Email Confirmation", confirmationLink);

        return View("ResetPasswordEmailMessage");
    }
}

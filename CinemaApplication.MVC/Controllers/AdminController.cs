using CinemaApplication.AuthenticationAndAuthorization.Authentication;
using CinemaApplication.AuthenticationAndAuthorization.Authorization;
using CinemaApplication.EmailServiceLibrary;
using CinemaApplication.MVC.Models;
using CinemaApplication.MVC.Models.EditAccountModels;
using CinemaApplication.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CinemaApplication.MVC.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAuthenticationProcedures _authenticationProcedures;
    private readonly IAuthorizationProcedures _authorizationProcedures;
    private readonly IEmailService _emailService;

    public AdminController(IAuthenticationProcedures authenticationProcedures, IAuthorizationProcedures authorizationProcedures,
        IEmailService emailService)
    {
        _authenticationProcedures = authenticationProcedures;
        _authorizationProcedures = authorizationProcedures;
        _emailService = emailService;
    }
    
    public async Task<IActionResult> ManageUsers(bool userCreationSuccess, bool userDeletionSuccess, bool userDeletionSuccessEmailFailure,
        bool userDeletionFailure, bool userCreationSuccessEmailSuccess, bool userCreationSuccessEmailFailure)
    {
        List<AppUserWithRole> usersWithRoles = new();
        List<AppUser> users = await _authenticationProcedures.GetUsersAsync();
        users = users.OrderBy(user => user.UserName).ToList();
        users.Remove(await _authenticationProcedures.GetCurrentUserAsync());

        foreach (AppUser user in users)
        {
            string role = await _authorizationProcedures.GetUserRoleAsync(user.Id);
            role = role != "ContentAdmin" ? role : "Content Admin";
            usersWithRoles.Add(new AppUserWithRole() { AppUser = user, AppUserRole = role });
        }

        ViewData["UserCreationSuccess"] = userCreationSuccess;
        ViewData["UserCreationSuccessEmailSuccess"] = userCreationSuccessEmailSuccess;
        ViewData["UserCreationSuccessEmailFailure"] = userCreationSuccessEmailFailure;
        ViewData["UserDeletionSuccess"] = userDeletionSuccess;
        ViewData["UserDeletionSuccessEmailFailure"] = userDeletionSuccessEmailFailure;
        ViewData["UserDeletionFailure"] = userDeletionFailure;
        return View(usersWithRoles);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        AppUser user = await _authenticationProcedures.FindByUserIdAsync(userId);
        bool result = await _authenticationProcedures.DeleteUserAccountAsync(user);

        if(!result)
            return RedirectToAction("ManageUsers", "Admin", new {userDeletionFailure = true});

        result = await _emailService.SendEmailAsync(user.Email, "Account Deleted", 
            "Your account has been deleted by an administrator. If you have any questions you can contact us through our email kinnaskonstantinos0@gmail.com .");
        if (!result)
            return RedirectToAction("ManageUsers", "Admin", new {userDeletionSuccessEmailFailure = true});
        
        return RedirectToAction("ManageUsers", "Admin", new {userDeletionSuccess = true});
    }

    [HttpGet]
    public IActionResult CreateUser(bool duplicateUsername, bool duplicateEmail)
    {
        ViewData["DuplicateUsername"] = duplicateUsername;
        ViewData["DuplicateEmail"] = duplicateEmail;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserModel createUserModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var result = await _authenticationProcedures.FindByUsernameAsync(createUserModel.Username);
        if (result != null)
        {
            return CreateUser(true, false);
        }

        result = await _authenticationProcedures.FindByEmailAsync(createUserModel.Email);
        if (result != null)
        {
            return CreateUser(false, true);
        }

        AppUser appUser = new AppUser();
        appUser.UserName = createUserModel.Username;
        appUser.PhoneNumber = createUserModel.PhoneNumber;
        appUser.Email = createUserModel.Email;
        appUser.EmailConfirmed = createUserModel.IsEmailConfirmed;

        bool success;
        if(createUserModel.IsEmailConfirmed)
        {
            await _authenticationProcedures.RegisterUserAndConfirmEmailAsync(appUser, createUserModel.Password!, false, createUserModel.AccountType!);
            //maybe do a check here
            
            return RedirectToAction("ManageUsers", "Admin", new {userCreationSuccess = true});
        }

        (string userId, string confirmationToken) = await _authenticationProcedures.RegisterUserAsync(appUser, createUserModel.Password!,
            false, createUserModel.AccountType!);
        
        //maybe do a check here
        string message = "Click on the following link to confirm your email:";
        string? link = Url.Action("ConfirmEmail", "Account", new { userId = WebUtility.UrlEncode(userId), token = WebUtility.UrlEncode(confirmationToken) }, Request.Scheme);
        string? confirmationLink = $"{message} {link}";
        success = await _emailService.SendEmailAsync(appUser.Email, "Email Confirmation", confirmationLink);

        if(!success)
        {
            return RedirectToAction("ManageUsers", "Admin", new { userCreationSuccessEmailFailure = true });
        }

        return RedirectToAction("ManageUsers", "Admin", new { userCreationSuccessEmailSuccess = true });
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string userId, 
        bool duplicateUsernameError, bool duplicateEmailError,
        bool basicInformationChangeFailure, bool basicInformationChangeSuccess, bool basicInformationChangeSuccessEmailFailure,
        bool passwordChangeSuccess, bool passwordChangeFailure, bool passwordChangeSuccessEmailFailure,
        bool emailChangedSuccess, bool emailChangedFailure, bool emailChangedSuccessEmailFailure,
        bool accountStatusChangedSuccessEmailFailure, bool accountStatusChangedSuccess, bool accountStatusChangedFailure)
    {
        AppUser appUser = await _authenticationProcedures.FindByUserIdAsync(userId);
        AccountBasicSettingsViewModel accountBasicSettingsViewModel = new()
        {
            Username = appUser.UserName,
            PhoneNumber = appUser.PhoneNumber,
            AccountType = await _authorizationProcedures.GetUserRoleAsync(appUser.Id)
        };

        ChangePasswordModel changePasswordModel = new()
        {
            OldPassword = appUser.PasswordHash
        };
        ChangeEmailModel resetEmailModel = new()
        {
            OldEmail = appUser.Email
        };

        EditAccountModel editAccountModel = new()
        {
            AccountBasicSettingsViewModel = accountBasicSettingsViewModel,
            ChangePasswordModel = changePasswordModel,
            ChangeEmailModel = resetEmailModel,
            IsEmailConfirmed = appUser.EmailConfirmed
        };

        ViewData["UserId"] = appUser.Id;
        ViewData["DuplicateUsernameError"] = duplicateUsernameError;
        ViewData["DuplicateEmailError"] = duplicateEmailError;
        
        ViewData["BasicInformationChangeSuccess"] = basicInformationChangeSuccess;
        ViewData["BasicInformationChangeError"] = basicInformationChangeFailure;
        ViewData["BasicInformationChangeSuccessEmailFailure"] = basicInformationChangeSuccessEmailFailure;

        ViewData["PasswordChangeError"] = passwordChangeFailure;
        ViewData["PasswordChangeSuccess"] = passwordChangeSuccess;
        ViewData["PasswordChangeSuccessEmailFailure"] = passwordChangeSuccessEmailFailure;

        ViewData["EmailChangedSuccess"] = emailChangedSuccess;
        ViewData["EmailChangedFailure"] = emailChangedFailure;
        ViewData["EmailChangedSuccessEmailFailure"] = emailChangedSuccessEmailFailure;

        ViewData["AccountStatusChangedSuccessEmailFailure"] = accountStatusChangedSuccessEmailFailure;
        ViewData["AccountStatusChangedSuccess"] = accountStatusChangedSuccess;
        ViewData["AccountStatusChangedFailure"] = accountStatusChangedFailure;
        return View(editAccountModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditUserBasicAccountSettings(AccountBasicSettingsViewModel accountBasicSettingsViewModel, string userId, string oldUserRole)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("EditUser", "Admin", new { userId = userId });
        }

        AppUser appUser = await _authenticationProcedures.FindByUserIdAsync(userId);
        appUser.UserName = accountBasicSettingsViewModel.Username;
        appUser.PhoneNumber = accountBasicSettingsViewModel.PhoneNumber;

        if (appUser.UserName != accountBasicSettingsViewModel.Username)
        {
            if (await _authenticationProcedures.FindByUsernameAsync(accountBasicSettingsViewModel.Username) is not null)
            {
                return RedirectToAction("EditUser", "Admin", new { userId = userId, duplicateUsernameError = true });
            }
        }

        bool result = await _authenticationProcedures.UpdateUserAccountAsync(appUser);
        if (!result)
            return RedirectToAction("EditUser", "Admin", new { userId = userId, basicInformationChangeFailure = true });

        //maybe do a check here
        IdentityRole oldRole = null!;
        IdentityRole newRole = null!;
        if (oldUserRole != accountBasicSettingsViewModel.AccountType)
        {
            oldRole = await _authorizationProcedures.GetRoleByNameAsync(oldUserRole);
            newRole = await _authorizationProcedures.GetRoleByNameAsync(accountBasicSettingsViewModel.AccountType);
            await _authorizationProcedures.UpdateRoleOfUserAsync(userId, oldRole.Id, newRole.Id);
        }

        string message = "The following information has been updated by an administrator: " +
            $"\nNew Username: {appUser.UserName}\nNew PhoneNumber: {appUser.PhoneNumber}";
        if (oldUserRole != accountBasicSettingsViewModel.AccountType)
            message += $"\nNew Role: {newRole.Name}";

        result = await _emailService.SendEmailAsync(appUser.Email, "Account Information Updated", message);
        if(!result)
            return RedirectToAction("EditUser", "Admin", new { userId = userId, basicInformationChangeSuccessEmailFailure = true });

        return RedirectToAction("EditUser", "Admin", new { userId = userId, basicInformationChangeSuccess = true });
    }

    [HttpPost]
    public async Task<IActionResult> EditUserPassword(ChangePasswordModel changePasswordModel, string userId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("EditUser", "Admin", new { userId = userId });
        }

        AppUser appUser = await _authenticationProcedures.FindByUserIdAsync(userId);

        bool result = await _authenticationProcedures.ChangePasswordWithoutConfirmationAsync(appUser, changePasswordModel.NewPassword);
        if (!result)
            return RedirectToAction("EditUser", "Admin", new { userId = userId, passwordChangeFailure = true });

        string message = $"Your password has been updated by an administrator:\nNew Password: {changePasswordModel.NewPassword}";
        result = await _emailService.SendEmailAsync(appUser.Email, "Account Password Updated", message);

        if (!result)
            return RedirectToAction("EditUser", "Admin", new { userId = userId, passwordChangeSuccessEmailFailure = true });

        return RedirectToAction("EditUser", "Admin", new { userId = userId, passwordChangeSuccess = true });
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUserEmail(ChangeEmailModel changeEmailModel, string userId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("EditUser", "Admin", new { userId = userId });
        }

        if (await _authenticationProcedures.FindByEmailAsync(changeEmailModel.NewEmail) != null)
        {
            return RedirectToAction("EditUser", "Admin", new { userId = userId, duplicateEmailError = true });
        }
        
        //string oldEmail = appUser.Email;
        bool result = await _authenticationProcedures.ChangeEmailWithoutConfirmationAsync(userId, changeEmailModel.NewEmail);
        if (!result)
        {
            return RedirectToAction("EditUser", "Admin", new { userId = userId, emailChangedFailure = true });
        }

        string message = $"An administrator has changed your current account email {changeEmailModel.OldEmail} to your new email {changeEmailModel.NewEmail}. " +
            $"If you have any questions on the matter you can contact us through our email kinnaskonstantinos0@gmail.com";
        result = await _emailService.SendEmailAsync(changeEmailModel.OldEmail, "Account Email Change", message);
        if(!result)
            return RedirectToAction("EditUser", "Admin", new { userId = userId, emailChangedSuccessEmailFailure = true });

        return RedirectToAction("EditUser", "Admin", new { userId = userId, emailChangedSuccess = true });
    }

    [HttpPost]
    public async Task<IActionResult> ChangeEmailConfirmation(string currentEmailStatus, string newEmailStatus, string userId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("EditUser", "Admin", new { userId = userId });
        }

        if (!((currentEmailStatus.Trim().ToLower() == "activated" && newEmailStatus.Trim().ToLower() == "deactivated") ||
           (currentEmailStatus.Trim().ToLower() == "deactivated" && newEmailStatus.Trim().ToLower() == "activated")))
        {
            return RedirectToAction("EditUser", "Admin", new { userId = userId });
        }

        AppUser appUser = await _authenticationProcedures.FindByUserIdAsync(userId);

        bool shouldActivate = newEmailStatus.Trim().ToLower() == "activated" ? true : false;
        bool result = await _authenticationProcedures.EnableOrDisableEmailWithoutConfirmationAsync(userId, shouldActivate);
        if (!result)
            return RedirectToAction("EditUser", "Admin", new { userId = userId, accountStatusChangedFailure = true });

        if (shouldActivate)
        {
            string message = "Your email has been confirmed by an administrator and your account is now active.";
            result = await _emailService.SendEmailAsync(appUser.Email, "Account Activated", message);
        }
        else
        {
            string message = "Your account has been disabled by an administrator. " +
                $"If you have any questions as to why you can contact us through our email kinnaskonstantinos0@gmail.com";
            result = await _emailService.SendEmailAsync(appUser.Email, "Account Password Updated", message);
        }

        if (!result)
            return RedirectToAction("EditUser", "Admin", new { userId = userId, accountStatusChangedSuccessEmailFailure = true });

        return RedirectToAction("EditUser", "Admin", new { userId = userId, accountStatusChangedSuccess = true });
    }
}

namespace CinemaApplication.MVC.Models.EditAccountModels;

public class EditAccountModel
{
    public AccountBasicSettingsViewModel AccountBasicSettingsViewModel { get; set; } = new();
    public ChangePasswordModel ChangePasswordModel { get; set; } = new();
    public ChangeEmailModel ChangeEmailModel { get; set; } = new();
    public bool IsEmailConfirmed { get; set; }
}

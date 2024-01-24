using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;

public class BankCard
{
    public int Id { get; set; }
    [Required]
    public string? CardNumber { get; set; }
    [Required]
    public DateTime ExpirationDate { get; set; }
    [Required]
    public string? CardHolderName { get; set; }
    [Required]
    public string? CVC { get; set; }
    [Required]
    public string? UserId { get; set; }
}

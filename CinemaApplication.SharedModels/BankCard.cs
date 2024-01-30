using System.ComponentModel.DataAnnotations;

namespace CinemaApplication.SharedModels;

public class BankCard
{
    public int Id { get; set; }
    [Required]
    [RegularExpression("^[0-9]+$", ErrorMessage = "CardNumber must contain only numeric characters.")]
    [StringLength(19, MinimumLength = 10, ErrorMessage = "CardNumber must be between 10 and 19 digits.")]
    public string? CardNumber { get; set; }
    [Required]
    public DateTime ExpirationDate { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "CardHolderName must be less than or equal to 100 characters.")]
    public string? CardHolderName { get; set; }
    [Required]
    [RegularExpression("^[0-9]+$", ErrorMessage = "CVC must contain only numeric characters.")]
    [StringLength(4, MinimumLength = 3, ErrorMessage = "CVC must be between 3 and 4 digits.")]
    public string? CVC { get; set; }
    [Required]
    public string? UserId { get; set; }
}

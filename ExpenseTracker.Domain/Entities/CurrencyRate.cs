using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Entities;

public class CurrencyRate
{
    [Key]
    public int Id { get; set; }
    public Currency TargetCurrency { get; set; }
    public decimal RateToPLN { get; set; }
}

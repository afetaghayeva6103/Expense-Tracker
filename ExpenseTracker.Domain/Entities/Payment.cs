using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities;

public class Payment
{
    public Payment( int categoryId, decimal amount, Currency targetCurrency, DateTime paymentDate, PaymentStatus paymentStatus)
    {
        Amount = amount;
        TargetCurrency = targetCurrency;
        PaymentDate = paymentDate;
        PaymentStatus = paymentStatus;
        CategoryId = categoryId;
    }

    public Payment()
    {
        
    }

    [Key]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("ProjectId")]
    public Project Project { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    public decimal Amount { get; set; }
    public Currency TargetCurrency { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

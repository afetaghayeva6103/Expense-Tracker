using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities;

public class Payment
{
    [Key]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int CategoryId { get; set; }
    public int InvoiceId { get; set; }
    [ForeignKey("ProjectId")]
    public Project Project { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    [ForeignKey("InvoiceId")]
    public Invoice Invoice { get; set; }
    public decimal Amount { get; set; }
    public Currency TargetCurrency { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

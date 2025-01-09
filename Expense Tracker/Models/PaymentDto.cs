using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.Models;

public class PaymentDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName{ get; set; }
    public decimal Amount { get; set; }
    public Currency PaymentTargetCurrency { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

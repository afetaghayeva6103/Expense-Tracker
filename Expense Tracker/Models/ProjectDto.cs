using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.Models;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Currency TargetCurrency { get; set; } = Currency.AZN;
    public string Note { get; set; }
     public string? InvoiceNumber { get; set; }
    public string? InvoiceDocUrl { get; set; }
    public decimal? InvoiceAmount { get; set; }
    public Currency? InvoiceTargetCurrency { get; set; }
    public DateTime? InvoiceIssueDate { get; set; }

    public List<PaymentDto>? Payments { get; set; }
}

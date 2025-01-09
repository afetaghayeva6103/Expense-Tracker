using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.Models;

public class InvoiceDto
{
    public int ProjectId { get; set; }
    public string InvoiceNumber { get; set; }
    public string InvoiceDocUrl { get; set; }
    public decimal InvoiceAmount { get; set; }
    public Currency InvoiceTargetCurrency { get; set; }
    public DateTime InvoiceIssueDate { get; set; }
}

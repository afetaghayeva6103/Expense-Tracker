using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.Models;

public class EditProjectDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Currency TargetCurrency { get; set; }
    public string Note { get; set; }
}

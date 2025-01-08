using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.Models;

public class CategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public CategoryType CategoryType { get; set; }
}

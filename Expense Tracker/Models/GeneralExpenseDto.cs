using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.Models;

public class GeneralExpenseDto
{
    public int Id { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string ExpenseTypeString => ExpenseType.ToString();
    public string CurrencyString => Currency.ToString();
}

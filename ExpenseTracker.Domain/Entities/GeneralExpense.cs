using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities;

public class GeneralExpense
{
    public GeneralExpense(ExpenseType expenseType, decimal amount, Currency currency, DateTime expenseDate)
    {
        ExpenseType = expenseType;
        Amount = amount;
        Currency = currency;
        ExpenseDate = expenseDate;
    }
    public GeneralExpense()
    {
        
    }

    [Key]
    public int Id { get; set; }
    public ExpenseType ExpenseType{ get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public DateTime ExpenseDate { get; set; }
}

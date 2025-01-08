using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities;

public class Invoice
{
    [Key]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    [ForeignKey("ProjectId")]
    public Project Project { get; set; }
    public int? GeneralExpenseId { get; set; }
    [ForeignKey("GeneralExpenseId")]
    public GeneralExpense GeneralExpense { get; set; }
    public string Number { get; set; }
    public string DocUrl { get; set; }
    public decimal Amount { get; set; }
    public Currency TargetCurrency { get; set; }
    public DateTime IssueDate { get; set; }
}

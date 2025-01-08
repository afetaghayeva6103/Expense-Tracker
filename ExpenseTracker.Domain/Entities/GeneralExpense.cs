using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Entities;

public class GeneralExpense
{
    [Key]
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    [ForeignKey("Invoices")]
    public Invoice Invoice { get; set; }
    public ExpenseType ExpenseType{ get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public DateTime ExpenseDate { get; set; }
}

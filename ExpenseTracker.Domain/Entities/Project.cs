using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Currency TargetCurrency { get; set; }
    public string Note { get; set; }

    public string? InvoiceNumber { get; set; }
    public string? InvoiceDocUrl { get; set; }
    public decimal? InvoiceAmount { get; set; }
    public Currency? InvoiceTargetCurrency { get; set; }
    public DateTime? InvoiceIssueDate { get; set; }
    public List<Payment> Payments { get; set; }

    public Project(string title, string description, DateTime startDate, DateTime endDate, Currency targetCurrency, string note, 
        string? invoiceNumber, string? invoiceDocUrl, decimal? invoiceAmount, Currency? invoiceTargetCurrency, DateTime? invoiceIssueDate)
    {
        Title = title;
        Description = description;
        CreateDate = DateTime.Now;
        StartDate = startDate;
        EndDate = endDate;
        TargetCurrency = targetCurrency;
        Note = note;
        InvoiceNumber = invoiceNumber;
        InvoiceDocUrl = invoiceDocUrl;
        InvoiceAmount = invoiceAmount;
        InvoiceTargetCurrency = invoiceTargetCurrency;
        InvoiceIssueDate = invoiceIssueDate;
    }

    public Project()
    {

    }

}

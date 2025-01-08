﻿using ExpenseTracker.Domain.Enums;
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
    public List<Payment> Payments { get; set; }
    public List<Invoice> Invoices { get; set; }
}

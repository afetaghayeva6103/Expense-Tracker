using Expense_Tracker.DataAccess.Interfaces;
using ExpenseTracker.Domain.Context;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DataAccess.Concretes;

public class InvoiceRepository(AppDbContext context) : IInvoiceRepository
{
    public void Add(Invoice invoice)
    {
        context.Entry(invoice).State = EntityState.Added;
        context.SaveChanges();
    }

    public void Delete(Invoice invoice)
    {
        context.Entry(invoice).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public Invoice Get(int invoiceId)
    {
        return context.Invoices.FirstOrDefault(x => x.Id == invoiceId);
    }

    public List<Invoice> GetAllByProject(int projectId)
    {
        return context.Invoices.Where(x => x.ProjectId == projectId).ToList();
    }

    public void Update(Invoice invoice)
    {
        context.Entry(invoice).State = EntityState.Modified;
        context.SaveChanges();
    }
}

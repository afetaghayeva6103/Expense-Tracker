using Expense_Tracker.DataAccess.Interfaces;
using ExpenseTracker.Domain.Context;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DataAccess.Concretes;

public class PaymentRepository(AppDbContext context) : IPaymentRepository
{
    public void Add(Payment payment)
    {
        context.Entry(payment).State = EntityState.Added;
        context.SaveChanges();
    }

    public void Delete(Payment payment)
    {
        context.Entry(payment).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public Payment? Get(int paymentId)
    {
        return context.Payments.Include(x=>x.Project).Include(x=>x.Category).FirstOrDefault(x => x.Id == paymentId);
    }

    public List<Payment> GetAllByProject(int projectId)
    {
        return context.Payments.Where(x => x.ProjectId == projectId).ToList();
    }

    public void Update(Payment payment)
    {
        context.Entry(payment).State = EntityState.Modified;
        context.SaveChanges();
    }
}

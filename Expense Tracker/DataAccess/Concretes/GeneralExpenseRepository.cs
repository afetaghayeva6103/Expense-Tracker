using Expense_Tracker.DataAccess.Interfaces;
using ExpenseTracker.Domain.Context;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DataAccess.Concretes;

public class GeneralExpenseRepository(AppDbContext context) : IGeneralExpenseRepository
{
    public void Add(GeneralExpense expense)
    {
        context.Entry(expense).State = EntityState.Added;
        context.SaveChanges();
    }

    public void Delete(GeneralExpense expense)
    {
        context.Entry(expense).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public GeneralExpense? Get(int id)
    {
        return context.GeneralExpenses.FirstOrDefault(x => x.Id == id);
    }

    public List<GeneralExpense> GetAll()
    {
        return context.GeneralExpenses.ToList();
    }

    public void Update(GeneralExpense expense)
    {
        context.Entry(expense).State = EntityState.Modified;
        context.SaveChanges();
    }
}

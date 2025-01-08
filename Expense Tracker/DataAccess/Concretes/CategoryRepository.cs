using Expense_Tracker.DataAccess.Interfaces;
using ExpenseTracker.Domain.Context;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DataAccess.Concretes;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public void Add(Category category)
    {
        context.Entry(category).State = EntityState.Added;
        context.SaveChanges();
    }

    public void Delete(Category category)
    {
        context.Entry(category).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public Category? Get(int id)
    {
        return context.Categories.FirstOrDefault(x => x.Id == id);
    }

    public List<Category> GetAll()
    {
        return context.Categories.ToList();
    }

    public void Update(Category category)
    {
        context.Entry(category).State = EntityState.Modified;
        context.SaveChanges();
    }
}

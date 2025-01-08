using ExpenseTracker.Domain.Entities;

namespace Expense_Tracker.DataAccess.Interfaces;

public interface ICategoryRepository
{
    void Add(Category category);
    void Update(Category category);
    void Delete(Category category);
    List<Category> GetAll();
    Category Get(int id);
}

using ExpenseTracker.Domain.Entities;

namespace Expense_Tracker.DataAccess.Interfaces;

public interface IGeneralExpenseRepository
{
    void Add(GeneralExpense expense);
    void Update(GeneralExpense expense);
    void Delete(GeneralExpense expense);
    GeneralExpense Get(int id);
    List<GeneralExpense> GetAll();
}

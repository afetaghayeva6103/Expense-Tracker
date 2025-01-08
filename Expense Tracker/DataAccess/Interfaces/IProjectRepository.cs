using ExpenseTracker.Domain.Entities;

namespace Expense_Tracker.DataAccess.Interfaces;

public interface IProjectRepository
{
    void Add(Project project);
    void Update(Project project);
    void Delete(Project project);
    Project Get(int projectId);
    List<Project> GetAll();
}

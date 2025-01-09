using Expense_Tracker.DataAccess.Interfaces;
using ExpenseTracker.Domain.Context;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DataAccess.Concretes;

public class ProjectRepository(AppDbContext context) : IProjectRepository
{
    public void Add(Project project)
    {
        context.Entry(project).State = EntityState.Added;
        context.SaveChanges();
    }

    public void Delete(Project project)
    {
        context.Entry(project).State= EntityState.Deleted;
        context.SaveChanges();  
    }

    public Project? Get(int projectId)
    {
        return context.Projects.Include(x=>x.Payments).FirstOrDefault(x => x.Id == projectId);
    }

    public List<Project> GetAll()
    {
        return context.Projects.Include(x=>x.Payments).ToList();
    }

    public void Update(Project project)
    {
        context.Entry(project).State = EntityState.Modified;
        context.SaveChanges();
    }
}

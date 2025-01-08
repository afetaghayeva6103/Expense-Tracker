using AutoMapper;
using Expense_Tracker.Models;
using ExpenseTracker.Domain.Entities;
namespace Expense_Tracker.Profile;

public class ModelMapper: AutoMapper.Profile
{
    public ModelMapper()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<ProjectDto, Project>().ReverseMap();
    }
}

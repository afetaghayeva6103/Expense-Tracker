using Expense_Tracker.Models;
using ExpenseTracker.Domain.Entities;
namespace Expense_Tracker.Profile;

public class ModelMapper: AutoMapper.Profile
{
    public ModelMapper()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<ProjectDto, Project>().ReverseMap();
        CreateMap<GeneralExpense, GeneralExpenseDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>()
            .ForMember(x=>x.PaymentTargetCurrency, opt=>opt.MapFrom(x=>x.TargetCurrency))
            .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Title));
    }
}

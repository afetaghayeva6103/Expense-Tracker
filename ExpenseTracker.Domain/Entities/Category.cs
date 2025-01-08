using ExpenseTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Entities;

public class Category
{
    public Category(string title, CategoryType categoryType)
    {
        Title = title;
        CategoryType = categoryType;
    }

    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public CategoryType CategoryType { get; set; }
}

using AutoMapper;
using Expense_Tracker.DataAccess.Interfaces;
using Expense_Tracker.Models;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class CategoryController(ICategoryRepository categoryRepository, IMapper mapper) : Controller
    {
        public IActionResult Index()
        {
            var categories = categoryRepository.GetAll();
            var result = mapper.Map<List<CategoryDto>>(categories);
            return View(result);
        }

        public IActionResult AddOrEdit(int id)
        {
            if (id == 0)
                return View(new CategoryDto());
            else
            {
                var category = categoryRepository.Get(id);
                var result=mapper.Map<CategoryDto>(category);   
                return View(result);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id,Title,CategoryType")] CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                if (categoryDto.Id == 0)
                    categoryRepository.Add(new Category(categoryDto.Title, categoryDto.CategoryType));
                else
                {
                    var category = categoryRepository.Get(categoryDto.Id);
                    category.Title = categoryDto.Title;
                    category.CategoryType =categoryDto.CategoryType;
                    categoryRepository.Update(category);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = categoryRepository.Get(id);
            if (category != null)
            {
                categoryRepository.Delete(category);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

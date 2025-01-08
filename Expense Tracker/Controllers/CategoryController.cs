using Expense_Tracker.DataAccess.Interfaces;
using Expense_Tracker.Models;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class CategoryController(ICategoryRepository categoryRepository) : Controller
    {
        public IActionResult Index()
        {
            var categories = categoryRepository.GetAll();

            return View(categories);
        }

        public IActionResult Add(CategoryDto dto)
        {
            var newCategory = new Category(dto.Title, dto.CategoryType);
            return View(newCategory);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                if (categoryDto.Id == 0)
                    categoryRepository.Add(new Category(categoryDto.Title, categoryDto.CategoryType));
                else
                {
                    var category = categoryRepository.Get(categoryDto.Id);
                    categoryRepository.Update(category);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        // POST: Category/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpDelete("{id}")]
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

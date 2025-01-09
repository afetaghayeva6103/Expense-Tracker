using AutoMapper;
using Expense_Tracker.DataAccess.Interfaces;
using Expense_Tracker.Models;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers;

public class GeneralExpenseController(IGeneralExpenseRepository generalExpenseRepository, IMapper mapper) : Controller
{
    public IActionResult Index()
    {
        var expenses = generalExpenseRepository.GetAll();
        var result = mapper.Map<List<GeneralExpenseDto>>(expenses);
        return View(result);
    }

    public IActionResult Look(int id)
    {
        PopulateCurrencies();
        PopulateExpenseTypes();
        var expense = generalExpenseRepository.Get(id);
        var result = mapper.Map<GeneralExpenseDto>(expense);
        return View(result);
    }

    public IActionResult AddOrEdit(int id = 0)
    {
        PopulateCurrencies();
        PopulateExpenseTypes();
        if (id == 0)
            return View(new GeneralExpenseDto());
        else
        {
            var expense = generalExpenseRepository.Get(id);
            var result = mapper.Map<GeneralExpenseDto>(expense);
            return View(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddOrEdit([Bind("Id,ExpenseType, Amount, Currency, ExpenseDate")] GeneralExpenseDto dto)
    {
        if (ModelState.IsValid)
        {
            if (dto.Id == 0)
                generalExpenseRepository.Add(new GeneralExpense(dto.ExpenseType, dto.Amount, dto.Currency, dto.ExpenseDate));
            else
            {
                var expense = generalExpenseRepository.Get(dto.Id);
                expense.ExpenseType = dto.ExpenseType;
                expense.Amount = dto.Amount;
                expense.Currency = dto.Currency;
                expense.ExpenseDate = dto.ExpenseDate;
                generalExpenseRepository.Update(expense);
            }
            return RedirectToAction(nameof(Index));
        }
        PopulateCurrencies();
        PopulateExpenseTypes();
        return View(dto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var expense = generalExpenseRepository.Get(id);
        if (expense != null)
        {
            generalExpenseRepository.Delete(expense);
        }

        return RedirectToAction(nameof(Index));
    }

    [NonAction]
    public void PopulateCurrencies()
    {
        ViewBag.CurrencyList = Enum.GetValues(typeof(Currency))
                            .Cast<Currency>()
                            .Select(e => new { Text = e.ToString(), Value = (int)e })
                            .ToList();
    }

    [NonAction]
    public void PopulateExpenseTypes()
    {
        ViewBag.ExpenseTypes = Enum.GetValues(typeof(ExpenseType))
                            .Cast<ExpenseType>()
                            .Select(e => new { Text = e.ToString(), Value = (int)e })
                            .ToList();
    }
}

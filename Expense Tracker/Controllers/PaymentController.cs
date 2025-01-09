using AutoMapper;
using Expense_Tracker.DataAccess.Interfaces;
using Expense_Tracker.Models;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers;

public class PaymentController(IPaymentRepository paymentRepository, IMapper mapper, ICategoryRepository categoryRepository) : Controller
{
    public IActionResult AddOrEdit(int id = 0)
    {
        PopulateCurrencies();
        PopulateCategories();
        PopulatePaymentStatuses();
        if (id == 0)
            return View(new PaymentDto());
        else
        {
            var payment = paymentRepository.Get(id);
            var result = mapper.Map<PaymentDto>(payment);
            return View(result);
        }
    }

    public IActionResult Look(int id)
    {
        PopulateCurrencies();
        PopulateCategories();
        PopulatePaymentStatuses();
        var payment = paymentRepository.Get(id);
        var result = mapper.Map<PaymentDto>(payment);
        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddOrEdit([Bind("Id,PaymentDate,TargetCurrency,Amount, CategoryId, PaymentStatus")] PaymentDto dto)
    {
        if (ModelState.IsValid)
        {
            if (dto.Id == 0)
                paymentRepository.Add(new Payment());
            else
            {
                var payment = paymentRepository.Get(dto.Id);
                payment.Amount=dto.Amount;
                payment.PaymentDate = dto.PaymentDate;
                payment.CategoryId = payment.CategoryId;
                payment.TargetCurrency = payment.TargetCurrency;
                payment.PaymentStatus= dto.PaymentStatus;
            }
            return RedirectToAction(nameof(Index));
        }
        PopulateCurrencies();
        PopulateCategories();
        PopulatePaymentStatuses();
        return View(dto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var payment = paymentRepository.Get(id);
        if (payment != null)
        {
            paymentRepository.Delete(payment);
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
    public void PopulateCategories()
    {
        var CategoryCollection = categoryRepository.GetAll();
        Category DefaultCategory = new Category() { Id = 0, Title = "Choose Category" };
        CategoryCollection.Insert(0, DefaultCategory);
        ViewBag.Categories = CategoryCollection;
    }

    [NonAction]
    public void PopulatePaymentStatuses()
    {
        ViewBag.PaymentStatuses = Enum.GetValues(typeof(PaymentStatus))
                            .Cast<PaymentStatus>()
                            .Select(e => new { Text = e.ToString(), Value = (int)e })
                            .ToList();
    }
}

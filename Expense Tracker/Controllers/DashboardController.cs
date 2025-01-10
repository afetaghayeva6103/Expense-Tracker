using AutoMapper;
using Expense_Tracker.Models;
using ExpenseTracker.Domain.Context;
using ExpenseTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController(AppDbContext context, IMapper mapper) : Controller
    {
        public async Task<ActionResult> Index()
        {
            var startDate = DateTime.Now.AddYears(-1);
            var endDate = DateTime.Now;

            var payments = context.Payments.Include(x => x.Category);

            var totalIncome = payments.Where(x => x.Category.CategoryType == CategoryType.Income)
                        .Join(context.CurrencyRates,
                              payment => payment.TargetCurrency,
                              rate => rate.TargetCurrency,
                              (payment, rate) => new
                              {
                                  ConvertedAmount = payment.Amount * rate.RateToPLN
                              })
                        .Sum(x => x.ConvertedAmount);

            var totalExpense = payments.Where(x => x.Category.CategoryType == CategoryType.Expense)
                        .Join(context.CurrencyRates,
                              payment => payment.TargetCurrency,
                              rate => rate.TargetCurrency,
                              (payment, rate) => new
                              {
                                  ConvertedAmount = payment.Amount * rate.RateToPLN
                              })
                        .Sum(x => x.ConvertedAmount);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("pl-PL");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.TotalIncome = String.Format(culture, "{0:C0}", totalIncome);
            ViewBag.TotalExpense = String.Format(culture, "{0:C0}", totalExpense);


            // Total Balance
            decimal balance = totalIncome - totalExpense;
            ViewBag.Balance = String.Format(culture, "{0:C0}", balance);

            // Donut Chart - Expense by category
            ViewBag.DonutChartData = payments
                            .Where(i => i.Category.CategoryType == CategoryType.Expense)
                            .Join(context.CurrencyRates,
                            payment => payment.TargetCurrency,
                                  rate => rate.TargetCurrency,
                                  (payment, rate) => new
                                  {
                                      payment.Category.Id,
                                      ConvertedAmount = payment.Amount * rate.RateToPLN
                                  })
                            .GroupBy(j => j.Id)
                            .Select(k => new
                            {
                                amount = k.Sum(j => j.ConvertedAmount),
                                formattedAmount = k.Sum(j => j.ConvertedAmount).ToString("C0"),
                            })
                            .OrderByDescending(l => l.amount)
                            .ToList();

            //Spline Chart - Income vs Expense
            //Income
            List<SplineChartData> IncomeSummary = payments
                .Where(i => i.Category.CategoryType == CategoryType.Income)
                .Join(context.CurrencyRates,
                            payment => payment.TargetCurrency,
                                  rate => rate.TargetCurrency,
                                  (payment, rate) => new
                                  {
                                      payment.Category.Id,
                                      Date=payment.PaymentDate,
                                      ConvertedAmount = payment.Amount * rate.RateToPLN
                                  })
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    income = k.Sum(l => l.ConvertedAmount)
                })
                .ToList();

            //Expense
            List<SplineChartData> ExpenseSummary = payments
                .Where(i => i.Category.CategoryType == CategoryType.Expense)
                .Join(context.CurrencyRates,
                            payment => payment.TargetCurrency,
                                  rate => rate.TargetCurrency,
                                  (payment, rate) => new
                                  {
                                      payment.Category.Id,
                                      Date = payment.PaymentDate,
                                      ConvertedAmount = payment.Amount * rate.RateToPLN
                                  })
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    expense = k.Sum(l => l.ConvertedAmount)
                })
                .ToList();

            //Combine Income & Expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };

            //Recent Transactions
            var a= payments
                .OrderByDescending(j => j.PaymentDate)
                .Take(5)
                .ToList();

            ViewBag.RecentTransactions = mapper.Map<List<PaymentDto>>(a);

            //GeneralExpenses
            var generalExpenses = context.GeneralExpenses
                        .Join(context.CurrencyRates,
                              expense => expense.Currency,
                              rate => rate.TargetCurrency,
                              (expense, rate) => new
                              {
                                  expense.Id,
                                  expense.ExpenseDate,
                                  expense.ExpenseType,
                                  ExpenseTypeString = expense.ExpenseType.ToString(),
                                  Amount = expense.Amount * rate.RateToPLN
                              })
                        .OrderByDescending(j => j.ExpenseDate) // Order by ExpenseDate
                        .Take(5) // Take the most recent 5 expenses
                        .ToList();


            ViewBag.GeneralExpenses = generalExpenses;


            return View();
        }
    }

    public class SplineChartData
    {
        public string day { get; set; }
        public decimal income { get; set; }
        public decimal expense { get; set; }
    }
}

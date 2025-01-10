using Expense_Tracker.DataAccess.Interfaces;
using ExpenseTracker.Domain.Context;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.DataAccess.Concretes;

public class CurrencyRateRepository(AppDbContext context) : ICurrencyRateRepository
{
    public CurrencyRate? Get(Currency currencyId)
    {
        return context.CurrencyRates.FirstOrDefault(x => x.TargetCurrency == currencyId);
    }

    public List<CurrencyRate> GetAll()
    {
        return context.CurrencyRates.ToList();
    }
}

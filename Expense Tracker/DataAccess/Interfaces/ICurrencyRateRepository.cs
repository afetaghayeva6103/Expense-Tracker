using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;

namespace Expense_Tracker.DataAccess.Interfaces;

public interface ICurrencyRateRepository
{
    List<CurrencyRate> GetAll();
    CurrencyRate Get(Currency currencyId);
}

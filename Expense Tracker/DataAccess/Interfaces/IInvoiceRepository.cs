using ExpenseTracker.Domain.Entities;

namespace Expense_Tracker.DataAccess.Interfaces;

public interface IInvoiceRepository
{
    void Add(Invoice invoice);
    void Update(Invoice invoice);
    void Delete(Invoice invoice);
    Invoice Get(int invoiceId);
    List<Invoice> GetAllByProject(int projectId);
}

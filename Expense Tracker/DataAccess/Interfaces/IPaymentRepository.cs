using ExpenseTracker.Domain.Entities;

namespace Expense_Tracker.DataAccess.Interfaces;

public interface IPaymentRepository
{
    void Add(Payment payment);
    void Update(Payment payment);
    void Delete(Payment payment);
    Payment Get(int paymentId);
    List<Payment> GetAllByProject(int projectId);
}

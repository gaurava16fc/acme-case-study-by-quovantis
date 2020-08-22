using ACME.Backend.Core.Entities.Models;

namespace ACME.Backend.Core.Interfaces
{
    public interface IBankTransactionRepository : IRepository<BankTransactionDetail>
    {
    }
}

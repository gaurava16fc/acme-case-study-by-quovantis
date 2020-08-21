using ACME.Backend.Core.Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Backend.Core.Interfaces
{
    public interface ISavingAccountRepository : IRepository<SavingAccount>
    {
        IQueryable<SavingAccount> GetCustomerAllAccounts(int CustomerId);
        IQueryable<SavingAccount> GetCustomerOnlyActiveAccounts(int CustomerId);
        IQueryable<SavingAccount> GetCustomerAccountDetail(int CustomerId, string AccountNumber);
        double? DepositFund(int CustomerId, string AccountNumber, double AmountToDeposit);
        double? WithdrawFund(int CustomerId, string AccountNumber, double AmountToWithdraw);

    }
}

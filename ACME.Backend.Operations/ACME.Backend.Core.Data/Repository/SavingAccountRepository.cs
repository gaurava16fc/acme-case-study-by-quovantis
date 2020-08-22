using ACME.Backend.Core.Entities.Models;
using ACME.Backend.Core.Interfaces;
using System.Linq;

namespace ACME.Backend.Core.Data.Repository
{
    public class SavingAccountRepository : Repository<RepositoryDBContext, SavingAccount>, ISavingAccountRepository
    {
        public SavingAccountRepository(RepositoryDBContext repoContext) : base(repoContext)
        {
        }

        public IQueryable<SavingAccount> GetCustomerAccountDetail(int CustomerId, string AccountNumber)
        {
            return FindByCondition(sa => sa.CustomerId == CustomerId 
                                            && sa.AccountNumber.Trim().Equals(AccountNumber.Trim())
                                            && sa.IsActive
                                  ).AsQueryable();
        }

        public IQueryable<SavingAccount> GetCustomerOnlyActiveAccounts(int CustomerId)
        {
            return FindByCondition(sa => sa.CustomerId == CustomerId && sa.IsActive).AsQueryable();
        }

        public IQueryable<SavingAccount> GetCustomerAllAccounts(int CustomerId)
        {
            return FindByCondition(sa => sa.CustomerId == CustomerId).AsQueryable();
        }

        public double? DepositFund(int CustomerId, string AccountNumber, double AmountToDeposit)
        {
            double? totalFundsInAccount = 0;
            totalFundsInAccount = GetCustomerAccountBalance(CustomerId, AccountNumber);
            if (AmountToDeposit > 0)
                totalFundsInAccount += AmountToDeposit;
            return totalFundsInAccount;
        }

        public double? WithdrawFund(int CustomerId, string AccountNumber, double AmountToWithdraw)
        {
            double? totalFundsInAccount = 0;
            totalFundsInAccount = GetCustomerAccountBalance(CustomerId, AccountNumber);
            if (IsSufficientAvailableFundInAccount(AmountToWithdraw, totalFundsInAccount))
            {
                totalFundsInAccount -= AmountToWithdraw;
                return totalFundsInAccount;
            }
            return 0;
        }

        #region Helper Method(s)
        private double? GetCustomerAccountBalance(int CustomerId, string AccountNumber)
        {
            double? totalAmount = 0;
            var custAccount = GetCustomerAccountDetail(CustomerId, AccountNumber);
            totalAmount = custAccount.Sum(e => (double?)e.TotalBalance ?? 0);
            return totalAmount;
        }

        private bool IsSufficientAvailableFundInAccount(double withdrawlAmount, double? totalFundsInAccount)
        {
            return withdrawlAmount < totalFundsInAccount;
        }
        #endregion
    }
}

using ACME.Backend.Core.Entities.Models;
using ACME.Backend.Core.Interfaces;

namespace ACME.Backend.Core.Data.Repository
{
    public class BankTransactionRepository : Repository<RepositoryDBContext, BankTransactionDetail>, IBankTransactionRepository
    {
        public BankTransactionRepository(RepositoryDBContext repoContext) : base(repoContext)
        {
        }
    }
}

using ACME.Backend.Core.Entities.Models;
using ACME.Backend.Core.Interfaces;

namespace ACME.Backend.Core.Data.Repository
{
    public class CustomerRepository : Repository<RepositoryDBContext, Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryDBContext repoContext) : base(repoContext)
        {
        }
    }
}

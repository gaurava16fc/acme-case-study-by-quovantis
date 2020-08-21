using ACME.Backend.Core.Entities.Models;
using ACME.Backend.Core.Interfaces;

namespace ACME.Backend.Core.Data.Repository
{
    public class EmployeeRepository : Repository<RepositoryDBContext, Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryDBContext repoContext) : base(repoContext)
        {
        }
    }
}

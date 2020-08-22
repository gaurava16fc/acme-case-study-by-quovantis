using ACME.Backend.Core.Interfaces;

namespace ACME.Backend.Core.Data.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryDBContext _repositoryDBContext;

        private ICustomerRepository _customerRepository;
        private IEmployeeRepository _employeeRepository;
        private IAuthRepository _authRepository;
        private ISavingAccountRepository _savingAccountRepository;
        private IBankTransactionRepository _bankTransactionRepository;

        public RepositoryWrapper(RepositoryDBContext repositoryDBContext)
        {
            _repositoryDBContext = repositoryDBContext;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_repositoryDBContext);
                }
                return _customerRepository;
            }
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_repositoryDBContext);
                }
                return _employeeRepository;
            }
        }

        public IAuthRepository AuthRepository
        {
            get
            {
                if (_authRepository == null)
                {
                    _authRepository = new AuthRepository(_repositoryDBContext);
                }
                return _authRepository;
            }
        }
        
        public ISavingAccountRepository SavingAccountRepository
        {
            get
            {
                if (_savingAccountRepository == null)
                {
                    _savingAccountRepository = new SavingAccountRepository(_repositoryDBContext);
                }
                return _savingAccountRepository;
            }
        }
        
        public IBankTransactionRepository BankTransactionRepository
        {
            get
            {
                if (_bankTransactionRepository == null)
                {
                    _bankTransactionRepository = new BankTransactionRepository(_repositoryDBContext);
                }
                return _bankTransactionRepository;
            }
        }
    }
}

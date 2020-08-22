namespace ACME.Backend.Core.Interfaces
{
    public interface IRepositoryWrapper
    {
        IAuthRepository AuthRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ISavingAccountRepository SavingAccountRepository { get; }
        IBankTransactionRepository BankTransactionRepository { get; }
    }
}

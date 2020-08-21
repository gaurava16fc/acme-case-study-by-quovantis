using System;
using ACME.Backend.Core.Entities.Enums;

namespace ACME.Backend.Core.DTO
{
    public class BankTransactionRequestDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public double TransactionAmount { get; set; }
        public AppEnums.TransactionType TransactionType { get; set; }
        public DateTime CreatedOn { get; set; }

        public BankTransactionRequestDTO()
        {
            CreatedOn = DateTime.Now;
        }
    }

}

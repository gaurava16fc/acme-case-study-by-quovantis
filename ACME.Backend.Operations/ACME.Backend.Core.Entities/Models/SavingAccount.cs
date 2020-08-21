using System;
using System.ComponentModel.DataAnnotations;

namespace ACME.Backend.Core.Entities.Models
{
    public class SavingAccount
    {
        string _accountNumber = string.Empty;
        const int _minimumBalance = 1000;

        [Key]
        public int Id { get; set; }

        public string AccountNumber 
        { 
            get
            {
                _accountNumber = string.Empty;
                if (Id > 0)
                {
                    var new_account_number = "ACMEIN91110" + Id.ToString().PadLeft(5, '0');
                    _accountNumber = new_account_number;
                }
                return _accountNumber;
            }
            set
            {
                _accountNumber = value;
            }
        }

        public bool IsAccountLocked { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public double MinimumBalance { get; private set; } = _minimumBalance;

        public double TotalBalance { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; }

        public SavingAccount()
        {
            MinimumBalance = _minimumBalance; 
            _accountNumber = string.Empty;
        }
    }
}

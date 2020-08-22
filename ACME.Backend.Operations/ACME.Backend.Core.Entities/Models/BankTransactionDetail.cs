using System;
using System.ComponentModel.DataAnnotations;

namespace ACME.Backend.Core.Entities.Models
{
    public class BankTransactionDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public double TransactionAmount { get; set; }

        public DateTime RequestedOn { get; set; }

        public BankTransactionDetail()
        {
            RequestedOn = DateTime.Now;
        }
    }
}

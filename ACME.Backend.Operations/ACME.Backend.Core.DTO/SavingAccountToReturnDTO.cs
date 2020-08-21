using System;

namespace ACME.Backend.Core.DTO
{
    public class SavingAccountToReturnDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public bool IsAccountLocked { get; set; }
        public CustomerForDetailedDTO Customer { get; set; }
        public int CustomerId { get; set; }
        public double MinimumBalance { get; set; }
        public double TotalBalance { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

}

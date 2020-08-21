using System;

namespace ACME.Backend.Core.DTO
{
    public class EmployeeForDetailedDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

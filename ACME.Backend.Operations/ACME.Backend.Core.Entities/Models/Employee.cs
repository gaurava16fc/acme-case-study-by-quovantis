using System;
using System.ComponentModel.DataAnnotations;

namespace ACME.Backend.Core.Entities.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is a required field")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is a required field")]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOfJoining { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

using System.Collections.Generic;

namespace ACME.Backend.Core.Entities.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> MappedUsers { get; set; }
    }
}

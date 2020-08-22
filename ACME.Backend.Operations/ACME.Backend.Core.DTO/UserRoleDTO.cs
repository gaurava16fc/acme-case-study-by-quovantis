using System.Collections.Generic;

namespace ACME.Backend.Core.DTO
{
    public class UserRoleDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        //public ICollection<UserDetailsToReturnDTO> MappedUsers { get; set; }
    }
}

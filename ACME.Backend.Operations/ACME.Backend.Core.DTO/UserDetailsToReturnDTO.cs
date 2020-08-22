using System;

namespace ACME.Backend.Core.DTO
{
    public class UserDetailsToReturnDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public UserRoleDTO UserRole { get; set; }
        public int UserRoleId { get; set; }
        public int? UserMappedWithEntityId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
    }
}

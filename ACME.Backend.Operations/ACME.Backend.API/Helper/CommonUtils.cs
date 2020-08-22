using System.Security.Claims;

namespace ACME.Backend.API.Helper
{
    public class CommonUtils
    {
        public static string GetLoggedInUserRole(ClaimsPrincipal User) {
            return User.FindFirst(ClaimTypes.Role).Value;
        }

        public static int GetLoggedInUserId(ClaimsPrincipal User)
        {
            int _userId = -1;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out _userId);
            return _userId;
        }

        public static int GetLoggedInUserMappedEntityId(ClaimsPrincipal User)
        {
            int _userMappedEntityId = -1;
            int.TryParse(User.FindFirst(ClaimTypes.UserData).Value, out _userMappedEntityId);
            return _userMappedEntityId;
        }
    }
}

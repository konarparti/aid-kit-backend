using AidKit.Core.Enums;

namespace AidKit.WebApi.ViewModels.Response.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatusCode Status { get; set; }
        public UserRole UserRoleId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}

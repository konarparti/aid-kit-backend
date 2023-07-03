namespace AidKit.WebApi.ViewModels.Response.User
{
    public class UserAuthViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public Core.Enums.UserRole UserRoleId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}

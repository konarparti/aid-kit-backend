using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using AidKit.BLL.DTO.User;
using AidKit.BLL.Interfaces;
using AidKit.Core.Enums;
using AidKit.DAL;

namespace AidKit.BLL.Implementions
{
    public class UserManager : IUserManager
    {
        private readonly DataContext _dataContext;

        public UserManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<UserDTO?> AuthorizeUser(UserAuthDTO userAuthDto)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == userAuthDto.Login);

            if (user == null)
            {
                return null;
            }

            if (user.Status == (int)UserStatusCode.Blocked)
            {
                throw new AuthenticationException($"Пользователь {user.Login} заблокирован");
            }

            if (!BCrypt.Net.BCrypt.Verify(userAuthDto.Password, user.Password))
            {
                return null;
            }

            UserDTO userDto = new()
            {
                Id = user.Id,
                Login = user.Login,
                UserRoleId = (Core.Enums.UserRole)user.UserRoleId,
                Created = user.Created,
            };

            return userDto;
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (user == null)
                return null;

            var userDto = new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Login = user.Login,
                Email = user.Email,
                Status = (UserStatusCode)user.Status,
                UserRoleId = (UserRole)user.UserRoleId,
                Created = user.Created,
                Updated = user.Updated,
            };

            return userDto;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using AidKit.BLL.DTO.User;
using AidKit.BLL.Interfaces;
using AidKit.Core.Enums;
using AidKit.DAL;
using AidKit.DAL.Entities;
using UserRole = AidKit.Core.Enums.UserRole;

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

        public async Task<bool> CheckLoginAsync(string login)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == login);

            return user is null;
        }

        public async Task RegisterUserAsync(UserRegisterDTO userRegisterDTO)
        {
            var user = new User
            {
                Login = userRegisterDTO.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password),
                Created = DateTimeOffset.UtcNow,
                Email = userRegisterDTO.Email,
                FullName = userRegisterDTO.Name,
                Status = (int)UserStatusCode.Active,
                UserRoleId = (int)UserRole.User,
                ProfileImage = "",
            };

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
        }
    }
}

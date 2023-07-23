using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AidKit.BLL.DTO.User;

namespace AidKit.BLL.Interfaces
{
    public interface IUserManager
    {
        Task<UserDTO?> AuthorizeUser(UserAuthDTO userAuthDto);

        Task<UserDTO?> GetByIdAsync(int id);

        Task<bool> CheckLoginAsync(string login);

        Task RegisterUserAsync(UserRegisterDTO userRegisterDTO);
    }
}

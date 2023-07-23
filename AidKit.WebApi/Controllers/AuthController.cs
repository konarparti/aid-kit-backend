using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AidKit.BLL.DTO.User;
using AidKit.BLL.Interfaces;
using AidKit.WebApi.ViewModels.Requests.User;
using AidKit.WebApi.ViewModels.Response.User;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using AidKit.DAL.Entities;

namespace AidKit.WebApi.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AuthController(IUserManager userManager)
        {
            _userManager = userManager;

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("Login")]
        public async Task<ActionResult<UserAuthModel>> Login(UserAuthModel userAuthModel)
        {
            var userAuthDTO = new UserAuthDTO
            {
                Login = userAuthModel.Login,
                Password = userAuthModel.Password,
            };

            var userDto = await _userManager.AuthorizeUser(userAuthDTO);

            if (userDto == default)
            {
                return BadRequest(new { message = "Неверный логин или пароль." });
            }

            UserAuthViewModel userViewModel = new()
            {
                Id = userDto.Id,
                Login = userDto.Login,
                UserRoleId = userDto.UserRoleId,
                Created = userDto.Created,
                Updated = userDto.Updated,
            };

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, userDto.Login),
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Role, userDto.UserRoleId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
            };

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Выйти из портала.
        /// </summary>
        /// <response code='200'>Успешный выход.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost, Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("Register")]
        public async Task<ActionResult<UserAuthModel>> Register(UserRegisterModel userRegisterModel)
        {
            var userRegisterDTO = new UserRegisterDTO
            {
                Login = userRegisterModel.Login,
                Password = userRegisterModel.Password,
                Name = userRegisterModel.FullName,
                Email = userRegisterModel.Email,
            };

            var isLoginFree = await _userManager.CheckLoginAsync(userRegisterDTO.Login);

            if (!isLoginFree)
            {
                return BadRequest(new { message = $"Логин {userRegisterDTO.Login} уже занят" });
            }

            await _userManager.RegisterUserAsync(userRegisterDTO);

            return Ok();
        }
    }
}

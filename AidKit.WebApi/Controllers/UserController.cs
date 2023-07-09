using AidKit.Core.Сonstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using AidKit.BLL.Interfaces;
using AidKit.Core.Enums;
using AidKit.WebApi.ViewModels.Response.User;

namespace AidKit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// Получить данные текущего пользователя.
        /// </summary>
        [HttpGet("GetUserData")]
        [Authorize(Roles = UserStringRoles.ALL_USERS)]
        public async Task<ActionResult<UserViewModel>> GetUserData()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

            if (claimsIdentity == default)
            {
                return BadRequest(new { message = "Не удалось выполнить идентификацию пользователя" });
            }

            string? idString = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (idString == default)
            {
                return BadRequest(new { message = "Не удалось выполнить идентификацию пользователя" });
            }

            var id = int.Parse(idString);

            var userDto = await _userManager.GetByIdAsync(id);

            if (userDto == default)
            {
                return NotFound(new { message = $"Пользователь с id {id} не найден" });
            }

            UserViewModel userViewModel = new()
            {
                Id = userDto.Id,
                Login = userDto.Login,
                FullName = userDto.FullName,
                Email = userDto.Email,
                UserRoleId = userDto.UserRoleId,
                Status = userDto.Status,
                Created = userDto.Created,
                Updated = userDto.Updated,
            };

            return Ok(userViewModel);
        }
    }
}

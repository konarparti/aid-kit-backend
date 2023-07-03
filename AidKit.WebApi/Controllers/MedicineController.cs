using AidKit.BLL.Interfaces;
using AidKit.WebApi.ViewModels.Response.Medicine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AidKit.Core.Сonstants;
using AidKit.BLL.DTO.Medicine;

namespace AidKit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineManager _medicineManager;

        public MedicineController(IMedicineManager medicineManager)
        {
            _medicineManager = medicineManager;
        }

        /// <summary>
        /// Получить список всех лекарств
        /// </summary>
        /// <response code='200'>Список всех лекарств.</response>
        [HttpGet("GetAll")]
        [Authorize(Roles = UserStringRoles.ADMIN)]
        public async Task<ActionResult<IEnumerable<MedicineViewModel>>> GetAll()
        {
            var medicineDtos = await _medicineManager.GetAllAsync();

            var result = medicineDtos.Select(medicineDto => new MedicineViewModel
            {
                Id = medicineDto.Id,
                Name = medicineDto.Name,
                Amount = medicineDto.Amount,
                PathImage = medicineDto.PathImage,
                Available = medicineDto.Available,
                Expired = medicineDto.Expired,
                PainKindName = medicineDto.PainKindName,
                TypeMedicineName = medicineDto.TypeMedicineName,
                UserId = medicineDto.UserId,
            });

            return Ok(result);
        }

        /// <summary>
        /// Получить список всех лекарств по id пользователя
        /// </summary>
        /// <response code='200'>Список всех лекарств.</response>
        [HttpGet("GetAllByUserId/{id:int}")]
        [Authorize(Roles = UserStringRoles.ALL_USERS)]
        public async Task<ActionResult<IEnumerable<MedicineViewModel>>> GetAllByUserId(int id)
        {
            var medicineDtos = await _medicineManager.GetAllByUserIdAsync(id);

            var result = medicineDtos.Select(medicineDto => new MedicineViewModel
            {
                Id = medicineDto.Id,
                Name = medicineDto.Name,
                Amount = medicineDto.Amount,
                PathImage = medicineDto.PathImage,
                Available = medicineDto.Available,
                Expired = medicineDto.Expired,
                PainKindName = medicineDto.PainKindName,
                TypeMedicineName = medicineDto.TypeMedicineName,
                UserId = id,
            });

            return Ok(result);
        }

        /// <summary>
        /// Получить лекарство по Id.
        /// </summary>
        /// <param name="id">Id лекарства.</param>
        /// <response code='200'>Модель лекарства.</response>
        /// <response code='404'>Лекарство не найдено.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetById/{id:int}")]
        [Authorize(Roles = UserStringRoles.ALL_USERS)]
        public async Task<ActionResult<MedicineViewModel>> GetById(int id)
        {
            var medicine = await _medicineManager.GetByIdAsync(id);

            MedicineViewModel result =
            new()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Amount = medicine.Amount,
                PathImage = medicine.PathImage,
                Available = medicine.Available,
                Expired = medicine.Expired,
                PainKindName = medicine.PainKindName,
                TypeMedicineName = medicine.TypeMedicineName,
                UserId = id,
            };

            return Ok(result);
        }

    }
}

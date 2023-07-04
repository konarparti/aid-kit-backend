using AidKit.BLL.Interfaces;
using AidKit.WebApi.ViewModels.Response.Medicine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AidKit.Core.Сonstants;
using AidKit.BLL.DTO.Medicine;
using AidKit.Service.Interfaces;
using AidKit.WebApi.ViewModels.Requests.Medicine;
using Minio;

namespace AidKit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineManager _medicineManager;
        private readonly IFileStorageService _minioClient;

        public MedicineController(IMedicineManager medicineManager, IFileStorageService minioClient)
        {
            _medicineManager = medicineManager;
            _minioClient = minioClient;
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

        /// <summary>
        /// Добавить лекарство.
        /// </summary>
        /// <param name="medicineCreateModel">Модель создания лекарства.</param>
        /// <response code='200'>Id созданного лекарства.</response>
        [HttpPost("Create")]
        [Authorize(Roles = UserStringRoles.ALL_USERS)]
        public async Task<ActionResult<int>> Create([FromForm] MedicineCreateModel medicineCreateModel)
        {
            string? uniqueFileName = null;

            if (medicineCreateModel.ImageFile != null)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(medicineCreateModel.ImageFile.FileName);
                var fileExtension = Path.GetExtension(medicineCreateModel.ImageFile.FileName);
                uniqueFileName = $"{fileNameWithoutExtension}__{Guid.NewGuid()}{fileExtension}";

                await using (var fileStream = medicineCreateModel.ImageFile.OpenReadStream())
                {
                    await _minioClient.SaveFileAsync(uniqueFileName, FileStorageServiceConstants.MedicineImageBucketName, fileStream);
                }
            }

            var medicineDto = new MedicineDTO
            {
                Name = medicineCreateModel.Name,
                Expired = medicineCreateModel.Expired,
                Amount = medicineCreateModel.Amount,
                PathImage = uniqueFileName,
                Available = medicineCreateModel.Available,
                PainKindId = medicineCreateModel.PainKindId,
                TypeMedicineId = medicineCreateModel.TypeMedicineId,
                UserId = medicineCreateModel.UserId,
            };

            var id = await _medicineManager.CreateAsync(medicineDto);

            return CreatedAtAction(nameof(Create), id);
        }

        /// <summary>
        /// Изменить данные о лекарстве.
        /// </summary>
        /// <param name="medicineUpdateModel">Модель изменения данных.</param>
        /// <response code='200'>Статус операции.</response>
        /// <response code='404'>Данные не найдены.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("Update")]
        [Authorize(Roles = UserStringRoles.ALL_USERS)]
        public async Task<IActionResult> Update([FromForm] MedicineUpdateModel medicineUpdateModel)
        {
            var medicine = await _medicineManager.GetByIdAsync(medicineUpdateModel.Id);

            string? uniqueFileName = medicineUpdateModel.ImageFileName;

            if (medicineUpdateModel.ImageFile != null)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(medicineUpdateModel.ImageFile.FileName);
                var fileExtension = Path.GetExtension(medicineUpdateModel.ImageFile.FileName);
                uniqueFileName = $"{fileNameWithoutExtension}__{Guid.NewGuid()}{fileExtension}";

                await using (var fileStream = medicineUpdateModel.ImageFile.OpenReadStream())
                {
                    await _minioClient.SaveFileAsync(uniqueFileName, FileStorageServiceConstants.MedicineImageBucketName, fileStream);
                }
            }

            var medicineDTO = new MedicineDTO
            {
                Id = medicineUpdateModel.Id,
                Name = medicineUpdateModel.Name,
                Expired = medicineUpdateModel.Expired,
                Amount = medicineUpdateModel.Amount,
                PathImage = uniqueFileName,
                Available = medicineUpdateModel.Available,
                PainKindId = medicineUpdateModel.PainKindId,
                TypeMedicineId = medicineUpdateModel.TypeMedicineId,
                UserId = medicineUpdateModel.UserId,
            };

            await _medicineManager.UpdateAsync(medicineDTO);

            return Ok();
        }

    }
}

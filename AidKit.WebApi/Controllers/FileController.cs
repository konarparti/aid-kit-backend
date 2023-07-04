using AidKit.Core.Сonstants;
using AidKit.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio.Exceptions;
using System.Data;
using System.Text.RegularExpressions;
using AidKit.Core.ViewModels;
using AidKit.WebApi.ViewModels.Requests.File;
using Minio.DataModel;

namespace AidKit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;

        public FileController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
        }

        /// <summary>
        /// Получить изображение.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <response code='200'>Файл изображения.</response>
        /// <response code='400'>Неверный запрос.</response>
        /// <response code='404'>Изображение не найдено.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetImage/{fileName}")]
        public async Task<ActionResult> GetImage(string fileName)
        {
            var pattern = @"^(.+)\.(jpg|png|jpeg)";

            if (string.IsNullOrEmpty(fileName) || !Regex.IsMatch(fileName, pattern))
            {
                return BadRequest("Неверное имя или формат файла");
            }

            var fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var contentType = fileExtension == "png" ? "image/png" : "image/jpeg";

            try
            {
                var image = await _fileStorageService.GetFileAsync(fileName,
                    FileStorageServiceConstants.MedicineImageBucketName);
                return File(image, contentType);
            }
            catch (MinioException)
            {
                return NotFound("Изображение не найдено");
            }
        }

        /// <summary>
        /// Сохранить изображение.
        /// </summary>
        /// <param name="saveFileModel">Изображение.</param>
        /// <response code='200'>Изображение сохранено.</response>
        /// <response code='400'>Неверный запрос.</response>
        /// <response code="403">Выполнение операции запрещено.</response>
        /// <response code='503'>Ошибка при сохранении изображения.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [Authorize(Roles = UserStringRoles.ALL_USERS)]
        [HttpPost("SaveImage")]
        public async Task<ActionResult> SaveImage([FromForm] SaveFileModel saveFileModel)
        {
            var image = saveFileModel.Upload;

            var pattern = @"^(.+)\.(jpg|png|jpeg)";

            if (string.IsNullOrEmpty(image.FileName) || !Regex.IsMatch(image.FileName, pattern))
            {
                return BadRequest("Неверное имя или формат файла");
            }

            var bucket = FileStorageServiceConstants.MedicineImageBucketName;

            if (string.IsNullOrWhiteSpace(bucket))
                return BadRequest("Неверное название контейнера");

            try
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(image.FileName);
                var fileExtension = Path.GetExtension(image.FileName);
                var uniqueFileName = $"{fileNameWithoutExtension}__{Guid.NewGuid()}{fileExtension}";

                await using (var fileStream = image.OpenReadStream())
                {
                    await _fileStorageService.SaveFileAsync(uniqueFileName, bucket, fileStream);
                }

                var saveImageViewModel = new SaveImageViewModel(uniqueFileName, $"/api/File/GetImage/{uniqueFileName}");

                return Ok(saveImageViewModel);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Произошла ошибка при сохранении изображения");
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AidKit.WebApi.ViewModels.Requests.File
{
    public class SaveFileModel
    {
        /// <summary>
        /// загружаемый.
        /// </summary>
        [Required(ErrorMessage = "Файл обязателен.")]
        public IFormFile Upload { get; set; }

        /// <summary>
        /// Имя контейнера
        /// </summary>
        public string? BucketName { get; set; }
    }
}

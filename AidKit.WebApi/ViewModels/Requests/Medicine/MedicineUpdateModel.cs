using System.ComponentModel.DataAnnotations;

namespace AidKit.WebApi.ViewModels.Requests.Medicine
{
    public class MedicineUpdateModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор должен быть указан.")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Нвзвание должно быть указано.")]
        public string Name { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ImageFileName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Срок годности должен быть указан.")]
        public DateTimeOffset Expired { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Количество должно быть указано.")]
        public int Amount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Признак наличия должен быть указан.")]
        public bool Available { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор пользователя должен быть указан.")]
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор типа боли должен быть указан.")]
        public int PainKindId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор вида лекарства должен быть указан.")]
        public int TypeMedicineId { get; set; }
    }
}

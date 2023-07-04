namespace AidKit.WebApi.ViewModels.Requests.Medicine
{
    public class MedicineCreateModel
    {
        public string Name { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ImageFileName { get; set; }
        public DateTimeOffset Expired { get; set; }
        public int Amount { get; set; }
        public bool Available { get; set; }
        public int UserId { get; set; }
        public int PainKindId { get; set; }
        public int TypeMedicineId { get; set; }
    }
}

namespace AidKit.WebApi.ViewModels.Response.Medicine
{
    public class MedicineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PathImage { get; set; }
        public DateTimeOffset Expired { get; set; }
        public int Amount { get; set; }
        public bool Available { get; set; }

        public string TypeMedicineName { get; set; }
        public string PainKindName { get; set; }
        public int? UserId { get; set; }
    }
}

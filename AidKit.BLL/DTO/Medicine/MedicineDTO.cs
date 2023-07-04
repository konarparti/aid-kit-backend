using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidKit.BLL.DTO.Medicine
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PathImage { get; set; }
        public DateTimeOffset Expired { get; set; }
        public int Amount { get; set; }
        public bool Available { get; set; }

        public string TypeMedicineName { get; set; }
        public int TypeMedicineId { get; set; }
        public string PainKindName { get; set; }
        public int PainKindId { get; set; }
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidKit.DAL.Entities
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public DateTimeOffset Expired { get; set; }
        public int Amount { get; set; }
        public bool Available { get; set; }

        #region Навигационные свойства

        public int TypeMedicineId { get; set; }
        public TypeMedicine TypeMedicine { get; set; }

        public int PainKindId { get; set; }
        public PainKind PainKind { get; set; }

        #endregion
    }
}

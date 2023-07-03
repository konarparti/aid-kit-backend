using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidKit.DAL.Entities
{
    public class PainKind : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Навигационные свойства

        public IEnumerable<Medicine> Medicines { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AidKit.BLL.DTO.Medicine;

namespace AidKit.BLL.Interfaces
{
    public interface IMedicineManager
    {
        Task<IEnumerable<MedicineDTO>> GetAllAsync();

        Task<IEnumerable<MedicineDTO>> GetAllByUserIdAsync(int id);

        Task<MedicineDTO> GetByIdAsync(int id);

        Task<int> CreateAsync(MedicineDTO medicineDTO);

        Task UpdateAsync(MedicineDTO medicineDTO);
    }
}

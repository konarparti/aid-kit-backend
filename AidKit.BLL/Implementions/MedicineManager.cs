using AidKit.BLL.DTO.Medicine;
using AidKit.BLL.Interfaces;
using AidKit.Core.Exceptions;
using AidKit.DAL;
using Microsoft.EntityFrameworkCore;

namespace AidKit.BLL.Implementions
{
    public class MedicineManager : IMedicineManager
    {
        private readonly DataContext _context;

        public MedicineManager(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicineDTO>> GetAllAsync()
        {
            return await _context.Medicines
                .AsNoTracking()
                .Include(x => x.PainKind)
                .Include(x => x.TypeMedicine)
                .Select(x => new MedicineDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Amount = x.Amount,
                    PathImage = x.PathImage,
                    Available = x.Available,
                    Expired = x.Expired,
                    PainKindName = x.PainKind.Name,
                    TypeMedicineName = x.TypeMedicine.Name,
                    UserId = x.UserId,
                }).ToListAsync();

        }

        public async Task<IEnumerable<MedicineDTO>> GetAllByUserIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == default)
            {
                throw new NotFoundException($"Пользователь с id {id} не найден");
            }

            return await _context.Medicines
                .AsNoTracking()
                .Include(x => x.PainKind)
                .Include(x => x.TypeMedicine)
                .Where(x => x.UserId == id)
                .Select(x => new MedicineDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Amount = x.Amount,
                    PathImage = x.PathImage,
                    Available = x.Available,
                    Expired = x.Expired,
                    PainKindName = x.PainKind.Name,
                    TypeMedicineName = x.TypeMedicine.Name,
                    UserId = x.UserId,
                }).ToListAsync();
        }

        public async Task<MedicineDTO> GetByIdAsync(int id)
        {
            var medicine = await _context.Medicines
                .AsNoTracking()
                .Include(x => x.PainKind)
                .Include(x => x.TypeMedicine)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (medicine == null)
            {
                throw new NotFoundException($"Лекарство с id {id} не найдено");
            }

            MedicineDTO medicineDto = new()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Amount = medicine.Amount,
                PathImage = medicine.PathImage,
                Available = medicine.Available,
                Expired = medicine.Expired,
                PainKindName = medicine.PainKind.Name,
                TypeMedicineName = medicine.TypeMedicine.Name,
                UserId = medicine.UserId,
            };

            return medicineDto;
        }
    }
}

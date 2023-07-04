using AidKit.BLL.DTO.Medicine;
using AidKit.BLL.Interfaces;
using AidKit.Core.Exceptions;
using AidKit.DAL;
using AidKit.DAL.Entities;
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

        public async Task<int> CreateAsync(MedicineDTO medicineDTO)
        {
            var medicine = new Medicine
            {
                Name = medicineDTO.Name,
                Amount = medicineDTO.Amount,
                PathImage = medicineDTO.PathImage,
                Available = medicineDTO.Available,
                Expired = medicineDTO.Expired,
                PainKindId = medicineDTO.PainKindId,
                TypeMedicineId = medicineDTO.TypeMedicineId,
                UserId = medicineDTO.UserId,
                Created = DateTimeOffset.UtcNow,
            };

            await _context.AddAsync(medicine);
            await _context.SaveChangesAsync();

            return medicine.Id;
        }

        public async Task UpdateAsync(MedicineDTO medicineDTO)
        {
            var medicine = await _context.Medicines.FirstOrDefaultAsync(n => n.Id == medicineDTO.Id)
                           ?? throw new NotFoundException($"Лекарство с id {medicineDTO.Id} не найдено");

            medicine.Name = medicineDTO.Name;
            medicine.Amount = medicineDTO.Amount;
            medicine.PathImage = medicineDTO.PathImage;
            medicine.Available = medicineDTO.Available;
            medicine.Expired = medicineDTO.Expired;
            medicine.PainKindId = medicineDTO.PainKindId;
            medicine.TypeMedicineId = medicineDTO.TypeMedicineId;
            medicine.UserId = medicineDTO.UserId;
            medicine.Updated = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FirstOrDefaultAsync(n => n.Id == id)
                           ?? throw new NotFoundException($"Лекарство с id {id} не найдено");

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
        }
    }
}

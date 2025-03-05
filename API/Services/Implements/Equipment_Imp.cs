using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Xml;

namespace API.Services.Implements
{
    public class Equipment_Imp : Equipment_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public Equipment_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Equipment> AddANewEquipment(Equipment equipment)
        {
            await _dBContext.Equipment.AddAsync(equipment);
            await _dBContext.SaveChangesAsync();
            return equipment;
        }

        public async Task<Equipment> DeleteAnExistEquipment(string equipCode)
        {
            var getEquipment = await GetTheEquipmentByEquipCode(equipCode);
            if (getEquipment == null) { return getEquipment; }

            _dBContext.Equipment.Remove(getEquipment);
            await _dBContext.SaveChangesAsync();

            return getEquipment;
        }

        public async Task<Equipment> EditAnExistEquipment(Equipment equipment)
        {
            var getEquipment = await GetTheEquipmentByEquipCode(equipment.EquipCode);
            if(getEquipment == null) { return getEquipment; }

            getEquipment.BranchCode = equipment.BranchCode;
            getEquipment.EquipName = equipment.EquipName;
            getEquipment.Status = equipment.Status;
            getEquipment.Note = equipment.Note;
            getEquipment.StaffUpdate = equipment.StaffUpdate;
            getEquipment.AdminUpdate = equipment.AdminUpdate;
            getEquipment.IsReceived = equipment.IsReceived;
            getEquipment.IsDeleted = equipment.IsDeleted;

            await _dBContext.SaveChangesAsync();

            return getEquipment;
        }

        public async Task<List<Equipment>> GetEquipmentList(int limit)
        {
            if(limit <= 0) { return await _dBContext.Equipment.ToListAsync(); }
            return await _dBContext.Equipment.Take(limit).ToListAsync();
        }

        public async Task<Equipment> GetTheEquipmentByEquipCode(string equipCode)
        {
            return await _dBContext.Equipment.Where(x => x.EquipCode == equipCode).FirstOrDefaultAsync();
        }

        public async Task<List<Equipment>> GetTheEquipmentByEquipName(string equipName)
        {
            return await _dBContext.Equipment.Where(x => x.EquipName.Contains(equipName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
    }
}

using Models;

namespace API.Services.Interfaces
{
    public interface Equipment_Int
    {
        Task<List<Equipment>> GetEquipmentList();
        Task<Equipment> GetTheEquipmentByEquipCode(string equipCode);
        Task<List<Equipment>> GetTheEquipmentByEquipName(string equipName);
        Task<Equipment> AddANewEquipment(Equipment equipment);
        Task<Equipment> EditAnExistEquipment(Equipment equipment);
        Task<Equipment> DeleteAnExistEquipment(string equipCode);
    }
}

using Models;

namespace Client_FAU.Business.Interfaces
{
    public interface Equipment_Int
    {
        Task<List<Equipment>> GetEquipmentList(int limit);
        Task<Equipment> GetTheEquipmentByEquipCode(string equipCode);
        Task<List<Equipment>> GetTheEquipmentBySearchString(string str, int limit);
        Task<Equipment> AddANewEquipment(Equipment equipment);
        Task<Equipment> EditAnExistEquipment(Equipment equipment);
        Task<Equipment> DeleteAnExistEquipment(string equipCode);
    }
}

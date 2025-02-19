using Models;

namespace API.Services.Interfaces
{
    public interface Equipment_Int
    {
        List<Equipment> GetEquipmentList();
        Equipment GetTheEquipmentByEquipCode(string equipCode);
        List<Equipment> GetTheEquipmentByEquipName(string equipName);
        Equipment AddANewEquipment(Equipment equipment);
        Equipment EditAnExistEquipment(Equipment equipment);
        Equipment DeleteAnExistEquipment(string equipCode);
    }
}

using Models;

namespace Client_FAU.Business.Interfaces
{
    public interface Role_Int
    {
        Task<List<Role>> GetRoleList(int limit);
        Task<Role> GetTheRoleByOrderNumber(int orderNumber);
        Task<List<Role>> GetTheRolesBySearchString(string str, int limit);
        Task<Role> AddANewRole(Role role);
        Task<Role> EditAnExistRole(Role role);
        Task<Role> DeleteAnExistRole(int orderNumber);
    }
}

using Models;

namespace API.Services.Interfaces
{
    public interface Role_Int
    {
        Task<List<Role>> GetRoleList();
        Task<Role> GetTheRoleByOrderNumber(int orderNumber);
        Task<List<Role>> GetTheRoleByRoleName(string roleName);
        Task<Role> AddANewRole(Role role);
        Task<Role> EditAnExistRole(Role role);
        Task<Role> DeleteAnExistRole(int orderNumber);
    }
}

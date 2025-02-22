using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Services.Implements
{
    public class Role_Imp : Role_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public Role_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Role> AddANewRole(Role role)
        {
            await _dBContext.Roles.AddAsync(role);
            await _dBContext.SaveChangesAsync();
            return role;
        }

        public async Task<Role> DeleteAnExistRole(int orderNumber)
        {
            var getRole = await GetTheRoleByOrderNumber(orderNumber);
            if(getRole == null) { return getRole; }

            _dBContext.Roles.Remove(getRole);
            await _dBContext.SaveChangesAsync();

            return getRole;
        }

        public async Task<Role> EditAnExistRole(Role role)
        {
            var getRole = await GetTheRoleByOrderNumber(role.OrderNumber);
            if(getRole == null) { return getRole; }

            getRole.RoleName = role.RoleName;

            await _dBContext.SaveChangesAsync();

            return getRole;
        }

        public async Task<List<Role>> GetRoleList()
        {
            return await _dBContext.Roles.ToListAsync();
        }

        public async Task<Role> GetTheRoleByOrderNumber(int orderNumber)
        {
            return await _dBContext.Roles.Where(x => x.OrderNumber == orderNumber).FirstOrDefaultAsync();
        }

        public Task<List<Role>> GetTheRoleByRoleName(string roleName)
        {
            return _dBContext.Roles.Where(x => x.RoleName.Contains(roleName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
    }
}

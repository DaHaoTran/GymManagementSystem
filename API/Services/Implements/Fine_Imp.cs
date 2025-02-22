using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Formats.Tar;

namespace API.Services.Implements
{
    public class Fine_Imp : Fine_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public Fine_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Fine> AddANewFine(Fine fine)
        {
            await _dBContext.Fines.AddAsync(fine);
            await _dBContext.SaveChangesAsync();
            return fine;
        }

        public async Task<Fine> DeleteAnExistFine(Guid fineCode)
        {
            var getFine = await GetTheFineByFineCode(fineCode);
            if(getFine == null) { return getFine; }

            _dBContext.Fines.Remove(getFine);
            await _dBContext.SaveChangesAsync();

            return getFine;
        }

        public async Task<Fine> EditAnExistFine(Fine fine)
        {
            var getFine = await GetTheFineByFineCode(fine.FineCode);
            if(getFine == null) { return getFine; }

            getFine.CustomerCode = fine.CustomerCode;
            getFine.Date = fine.Date;
            getFine.Reason = fine.Reason;
            getFine.IsCompensated = fine.IsCompensated;
            getFine.AdminNote = fine.AdminNote;
            getFine.StaffNote = fine.StaffNote;
            getFine.UpdateBy = fine.UpdateBy;

            await _dBContext.SaveChangesAsync();

            return getFine;
        }

        public async Task<List<Fine>> GetFineList()
        {
            return await _dBContext.Fines.ToListAsync();
        }

        public async Task<Fine> GetTheFineByFineCode(Guid fineCode)
        {
            return await _dBContext.Fines.Where(x => x.FineCode == fineCode).FirstOrDefaultAsync();
        }

        public async Task<List<Fine>> GetTheFinesByCustomerCode(string customerCode)
        {
            return await _dBContext.Fines.Where(x => x.CustomerCode == customerCode).ToListAsync();
        }
    }
}

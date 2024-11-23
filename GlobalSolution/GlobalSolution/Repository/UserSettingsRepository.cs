using GlobalSolution.Interfaces;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Repository
{
    public class UserSettingRepository : IUserSettingsRepository
    {
        private readonly dbContext _context;

        public UserSettingRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserSetting>> GetAllAsync()
        {
            return await _context.UserSettings.ToListAsync();
        }

        public async Task<UserSetting?> GetByIdAsync(int id)
        {
            return await _context.UserSettings.FindAsync(id);
        }

        public async Task AddAsync(UserSetting setting)
        {
            _context.UserSettings.Add(setting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSetting setting)
        {
            _context.UserSettings.Update(setting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var setting = await _context.UserSettings.FindAsync(id);
            if (setting != null)
            {
                _context.UserSettings.Remove(setting);
                await _context.SaveChangesAsync();
            }
        }
    }
}

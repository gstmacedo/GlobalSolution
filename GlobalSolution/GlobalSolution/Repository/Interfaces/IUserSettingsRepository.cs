using GlobalSolution.Models;

namespace GlobalSolution.Interfaces
{
    public interface IUserSettingsRepository
    {
        Task<IEnumerable<UserSetting>> GetAllAsync();
        Task<UserSetting?> GetByIdAsync(int id);
        Task AddAsync(UserSetting setting);
        Task UpdateAsync(UserSetting setting);
        Task DeleteAsync(int id);
    }
}

using GlobalSolution.Models;

namespace GlobalSolution.Interfaces
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(int id);
        Task AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task DeleteAsync(int id);
    }
}

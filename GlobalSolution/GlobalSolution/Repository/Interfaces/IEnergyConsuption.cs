using GlobalSolution.Models;

namespace GlobalSolution.Interfaces
{
    public interface IEnergyConsumptionRepository
    {
        Task<IEnumerable<EnergyConsumption>> GetAllAsync();
        Task<EnergyConsumption?> GetByIdAsync(int id);
        Task AddAsync(EnergyConsumption consumption);
        Task UpdateAsync(EnergyConsumption consumption);
        Task DeleteAsync(int id);
    }
}

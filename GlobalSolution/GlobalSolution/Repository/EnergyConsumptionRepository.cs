using GlobalSolution.Interfaces;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Repository
{
    public class EnergyConsumptionRepository : IEnergyConsumptionRepository
    {
        private readonly dbContext _context;

        public EnergyConsumptionRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnergyConsumption>> GetAllAsync()
        {
            return await _context.EnergyConsumptions.ToListAsync();
        }

        public async Task<EnergyConsumption?> GetByIdAsync(int id)
        {
            return await _context.EnergyConsumptions.FindAsync(id);
        }

        public async Task AddAsync(EnergyConsumption consumption)
        {
            _context.EnergyConsumptions.Add(consumption);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EnergyConsumption consumption)
        {
            _context.EnergyConsumptions.Update(consumption);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var consumption = await _context.EnergyConsumptions.FindAsync(id);
            if (consumption != null)
            {
                _context.EnergyConsumptions.Remove(consumption);
                await _context.SaveChangesAsync();
            }
        }
    }
}

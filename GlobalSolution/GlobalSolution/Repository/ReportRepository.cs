using GlobalSolution.Interfaces;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly dbContext _context;

        public ReportRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task AddAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}

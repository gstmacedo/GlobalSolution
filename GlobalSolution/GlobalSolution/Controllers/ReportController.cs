using Microsoft.AspNetCore.Mvc;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly dbContext _context;

        public ReportController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _context.Reports.ToListAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, Report report)
        {
            if (id != report.Id) return BadRequest();

            _context.Entry(report).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergyConsumptionController : ControllerBase
    {
        private readonly dbContext _context;

        public EnergyConsumptionController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnergyConsumptions()
        {
            var consumptions = await _context.EnergyConsumptions.ToListAsync();
            return Ok(consumptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnergyConsumption(int id)
        {
            var consumption = await _context.EnergyConsumptions.FindAsync(id);
            if (consumption == null) return NotFound();
            return Ok(consumption);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnergyConsumption(EnergyConsumption consumption)
        {
            _context.EnergyConsumptions.Add(consumption);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEnergyConsumption), new { id = consumption.Id }, consumption);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnergyConsumption(int id, EnergyConsumption consumption)
        {
            if (id != consumption.Id) return BadRequest();

            _context.Entry(consumption).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnergyConsumption(int id)
        {
            var consumption = await _context.EnergyConsumptions.FindAsync(id);
            if (consumption == null) return NotFound();

            _context.EnergyConsumptions.Remove(consumption);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

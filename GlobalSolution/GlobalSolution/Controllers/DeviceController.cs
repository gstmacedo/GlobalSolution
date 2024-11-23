using Microsoft.AspNetCore.Mvc;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly dbContext _context;

        public DeviceController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _context.Devices.ToListAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();
            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, Device device)
        {
            if (id != device.Id) return BadRequest();

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

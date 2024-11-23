using Microsoft.AspNetCore.Mvc;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSettingController : ControllerBase
    {
        private readonly dbContext _context;

        public UserSettingController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSettings()
        {
            var settings = await _context.UserSettings.ToListAsync();
            return Ok(settings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSetting(int id)
        {
            var setting = await _context.UserSettings.FindAsync(id);
            if (setting == null) return NotFound();
            return Ok(setting);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserSetting(UserSetting setting)
        {
            _context.UserSettings.Add(setting);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserSetting), new { id = setting.Id }, setting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserSetting(int id, UserSetting setting)
        {
            if (id != setting.Id) return BadRequest();

            _context.Entry(setting).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSetting(int id)
        {
            var setting = await _context.UserSettings.FindAsync(id);
            if (setting == null) return NotFound();

            _context.UserSettings.Remove(setting);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

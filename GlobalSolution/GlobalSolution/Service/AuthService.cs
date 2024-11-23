using GlobalSolution.Models;
using Microsoft.AspNetCore.Identity;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GlobalSolution.Services
{
    public class AuthService
    {
        private readonly dbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(dbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.SenhaHash, password);
            if (verificationResult != PasswordVerificationResult.Success)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> Register(User user, string password)
        {
            var existingUser = await _dbContext.Users.AnyAsync(u => u.Email == user.Email);
            if (existingUser)
            {
                return false;
            }

            user.SenhaHash = _passwordHasher.HashPassword(user, password);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

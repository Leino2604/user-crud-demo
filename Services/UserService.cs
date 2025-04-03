using Microsoft.EntityFrameworkCore;
using user_crud_demo.Data;
using user_crud_demo.Models;

namespace user_crud_demo.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            _logger.LogInformation("Getting all users");
            return await _context.Users.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _logger.LogInformation("Creating new user: {Email}", user.Email);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public bool ValidateLoginAsync(string email, string password)
        {
            _logger.LogInformation("Validating login for: {Email}", email);

            // Hard code email + Password
            if (email == "admin@leino.com" && password == "Leino123!")
            {
                return true;
            }

            return false;
        }
    }
}
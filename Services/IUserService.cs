using user_crud_demo.Models;

namespace user_crud_demo.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        bool ValidateLoginAsync(string email, string password);
    }
}
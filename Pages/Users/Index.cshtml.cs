using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using user_crud_demo.Models;
using user_crud_demo.Services;

namespace user_crud_demo.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IUserService userService, ILogger<IndexModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IList<User> Users { get; set; } = new List<User>();

        public async Task OnGetAsync()
        {
            _logger.LogInformation("Loading user grid page");
            Users = (await _userService.GetAllUsersAsync()).ToList();
        }
    }
}
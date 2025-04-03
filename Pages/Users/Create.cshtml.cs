using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using user_crud_demo.Models;
using user_crud_demo.Services;

namespace user_crud_demo.Pages.Users
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IUserService userService, ILogger<CreateModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required(ErrorMessage = "First name is required")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } = string.Empty;

            [Display(Name = "Middle Name")]
            public string? MiddleName { get; set; }

            [Required(ErrorMessage = "Last name is required")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            public string Email { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new User
            {
                FirstName = Input.FirstName,
                MiddleName = Input.MiddleName,
                LastName = Input.LastName,
                Email = Input.Email
            };

            try
            {
                _logger.LogInformation("Creating new user: {Email}", user.Email);
                await _userService.CreateUserAsync(user);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the user.");
                return Page();
            }
        }
    }
}
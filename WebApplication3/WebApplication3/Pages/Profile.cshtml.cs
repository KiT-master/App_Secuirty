using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication3.Model;
using WebApplication3.Services;

namespace WebApplication3.Pages
{
    public class ProfileModel : PageModel
    {
        
        [BindProperty]
        public CustomUser User { get; set; }

        [BindProperty]
        public IFormFile? Upload { get; set; }
        [BindProperty]
        public string creditCardNo { get; set; }

        private readonly UserService _userService;
        private LogginService _logginService;

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ILogger<ProfileModel> _logger;

        private readonly SignInManager<CustomUser> _signInMaganer;
        private readonly PasswordService _passwordService;

        public ProfileModel(UserService userService, IHttpContextAccessor contextAccessor,ILogger<ProfileModel> logger, SignInManager<CustomUser> signInManager, PasswordService passwordService, LogginService logginService)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _signInMaganer = signInManager;
            _passwordService = passwordService;
            _logginService = logginService;
        }

        public IActionResult OnGet()
        {
            var email = _signInMaganer.Context.User.Identity.Name;
            if (email.IsNullOrEmpty())
            {
                return RedirectToPage("/Errors/Timeout");
            }

            User = _userService.getByEmail(email);
             StringToBytesConverter converter;
            
            creditCardNo = _passwordService.decrypt(User.CreditCardNo);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User != null)
            {
                await _signInMaganer.SignOutAsync();
                _logginService.Log(User.Email, "Logout", "/Logout");

                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");
        }
    }
}

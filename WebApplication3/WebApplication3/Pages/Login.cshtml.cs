using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using reCAPTCHA.AspNetCore.Attributes;
using WebApplication3.Helpers;
using WebApplication3.Model;
using WebApplication3.Services;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{

    public class LoginModel : PageModel
    {

        private readonly ILogger<LoginModel> _logger;
        private UserManager<CustomUser> _userManager { get; }
        private SignInManager<CustomUser> _signInManager { get; }
        
        private  LogginService _logginService { get; set; }

        [BindProperty]
        public string Captcha { get; set; }

        [BindProperty]
        public Login LModel { get; set; }

        [BindProperty]
        public Boolean RemberMe { get; set; }

        private IHttpContextAccessor _contextAccessor { get; set; }

        public LoginModel(UserManager<CustomUser> userManager,
        SignInManager<CustomUser> signInManager,
        IHttpContextAccessor contextAccessor,
        ILogger<LoginModel> logger,LogginService logginService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _logginService = logginService;
        }

   
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            { 


                var identityResult = await _signInManager.PasswordSignInAsync(LModel.Email,LModel.Password, RemberMe,true);
                if (identityResult.Succeeded)
                {
                    

                   // _contextAccessor.HttpContext.Session.SetString("Email", LModel.Email);
                    _logger.LogInformation("User: " + LModel.Email + " Logged in");
                    _logginService.Log(LModel.Email, "Login", "/Login");
                    return RedirectToPage("Index");
                }
                else { 

                    var Error = "Wrong email or password!!";
                    if(identityResult.IsLockedOut == true)
                    {
                         Error = "Too many attempets to log in account is locked out!!";
                    }


                    ModelState.AddModelError("", Error); 
                    }
                
            }
            return Page();
        }

        public void OnGet()
        {
        }


    }
}

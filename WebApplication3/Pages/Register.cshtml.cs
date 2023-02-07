using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Web;
using WebApplication3.Model;
using WebApplication3.Services;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public IFormFile? Upload { get; set; }
        private UserManager<CustomUser> userManager { get; }
        private SignInManager<CustomUser> signInManager { get; }

        private IWebHostEnvironment _environment;

        private readonly PasswordService _passwordService;

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<CustomUser> userManager,
        SignInManager<CustomUser> signInManager, IWebHostEnvironment environment,PasswordService passwordService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._environment = environment;
            this._passwordService = passwordService;
        }



        public void OnGet()
        {
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
    

            if (ModelState.IsValid)
            {


                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    RModel.Photo = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }
                else
                {
                    ModelState.AddModelError("", "Photo Cannot be empty!");
                    return Page();
                }

                var encryptedCreditCard = _passwordService.encrypt(RModel.CreditCardNo.ToString());

                var user = new CustomUser()
                {
                    UserName = HttpUtility.HtmlEncode(RModel.Email),
                    Email = HttpUtility.HtmlEncode(RModel.Email),
                    FullName = HttpUtility.HtmlEncode(RModel.FullName),
                    CreditCardNo = encryptedCreditCard,
                    Gender = HttpUtility.HtmlEncode(RModel.Gender),
                    MobileNo = HttpUtility.HtmlEncode(RModel.MobileNo),
                    DeliveryAddress = HttpUtility.HtmlEncode(RModel.DeliveryAddress),
                    PasswordHash = RModel.Password,
                    Photo = RModel.Photo,
                    AboutMe = HttpUtility.HtmlEncode(RModel.AboutMe)

                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }







    }
}

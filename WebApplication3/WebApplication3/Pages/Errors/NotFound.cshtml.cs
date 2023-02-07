using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication3.Pages.Errors
{
    public class NotFoundModel : PageModel
    {
        [BindProperty]
        public int Errocode { get; set; }
        public void OnGet(int errorCode)
        {
            Errocode = errorCode;
        }
    }
}

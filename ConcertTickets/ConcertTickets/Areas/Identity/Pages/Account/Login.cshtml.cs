using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ConcertTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConcertTickets.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly UserManager<CustomUser> _userManager;


        public LoginModel(SignInManager<CustomUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "E-mail is verplicht.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Wachtwoord is verplicht.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Onthoud mij")]
            public bool RememberMe { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Inloggen met e-mail en wachtwoord
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index"); // Ga naar de homepagina
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ongeldige inlogpoging.");
                return Page();
            }
        }
    }
}

using ConcertTickets.Models;  // Zorg dat CustomUser correct is toegevoegd
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ConcertTickets.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        // Constructor die UserManager en SignInManager injecteert
        public RegisterModel(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;  // UserManager voor gebruikersbeheer
            _signInManager = signInManager;  // SignInManager voor inloggen na registratie
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Voornaam is verplicht.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Familienaam is verplicht.")]
            public string LastName { get; set; }

            public string? MemberCardNumber { get; set; }

            [Required(ErrorMessage = "E-mailadres is verplicht.")]
            [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Wachtwoord is verplicht.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Bevestig het wachtwoord.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            Console.WriteLine("OnPostAsync aangeroepen");
            returnUrl ??= "/Identity/Account/Manage";
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is niet geldig.");
                return Page();
            }

            var user = new CustomUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                MemberCardNumber = Input.MemberCardNumber
            };

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}

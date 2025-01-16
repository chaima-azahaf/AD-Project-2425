using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConcertTickets.ViewModels
{
    public class OrderFormViewModel
    {
        public int ConcertId { get; set; }
        public string TicketType { get; set; }
        public decimal Price { get; set; }
        public int AvailableTickets { get; set; }

        [Required(ErrorMessage = "Geef het aantal gewenste tickets in (minimaal 1, maximaal 5).")]
        [Range(1, 5, ErrorMessage = "Je mag maximaal 5 tickets bestellen.")]
        public int NumberOfTickets { get; set; }

        [Required(ErrorMessage = "Je moet akkoord gaan met de algemene voorwaarden.")]
        public bool TermsAccepted { get; set; }

        [ValidateNever]
        public string UserId { get; set; }

        [ValidateNever]
        public string UserName { get; set; }
    }
}

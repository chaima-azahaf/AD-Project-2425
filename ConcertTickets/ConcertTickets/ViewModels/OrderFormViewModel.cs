using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConcertTickets.ViewModels
{
    public class OrderFormViewModel
    {
        public int TicketOfferId { get; set; }

        public string TicketType { get; set; }

        public decimal Price { get; set; }

        public int AvailableTickets { get; set; }

        public string ConcertName { get; set; }

        public DateTime ConcertDate { get; set; }

        public string ConcertLocation { get; set; }

        public bool HasMemberCard { get; set; }

        [Required(ErrorMessage = "Geef het aantal gewenste tickets in (minimaal 1, maximaal 5).")]
        [Range(1, 5, ErrorMessage = "Je mag maximaal 5 tickets bestellen.")]
        public int NumberOfTickets { get; set; }

        [Required(ErrorMessage = "Je moet akkoord gaan met de algemene voorwaarden.")]
        public bool AcceptTerms { get; set; }
        public string UserId { get; internal set; }
        public string UserName { get; internal set; }
        public double FinalPrice { get; internal set; }
    }
}

namespace ConcertTickets.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Gebruiker Id uit AspNetUsers
        public string UserName { get; set; }  // Naam van de gebruiker
        public int NumTickets { get; set; }
        public double TotalPrice { get; set; }
        public bool Paid { get; set; }  // True indien betaald
        public bool DiscountApplied { get; set; }  // True indien korting toegepast

        // Foreign Key en navigatie property naar TicketOffer
        public int TicketOfferId { get; set; }
        public TicketOffer TicketOffer { get; set; }
        public int NumberOfTickets { get; internal set; }
    }

}

namespace ConcertTickets.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        // Navigatie property
        public ICollection<TicketOffer> TicketOffers { get; set; }
    }

}

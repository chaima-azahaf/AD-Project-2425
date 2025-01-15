namespace ConcertTickets.Models
{
    public class TicketOffer
    {
        public int Id { get; set; }
        public string TicketType { get; set; }  // Golden Circle, Standing, Seated
        public int NumTickets { get; set; }
        public double Price { get; set; }

        // Foreign Key en navigatie property naar Concert
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }
    }

}

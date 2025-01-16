namespace ConcertTickets.ViewModels
{
    public class TicketOfferUpdateViewModel
    {
        public int TicketOfferId { get; set; }
        public int NumberOfTickets { get; set; }
        public int NumberOfTicketsSold { get; internal set; }
    }
}


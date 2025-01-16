namespace ConcertTickets.ViewModels
{
    public class ConcertViewModel
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public List<TicketOfferViewModel> TicketOffers { get; set; } = new List<TicketOfferViewModel>();
        public int AvailableTickets => TicketOffers.Sum(t => t.NumTickets);
    }

    public class TicketOfferViewModel
    {
        public string TicketType { get; set; }
        public int NumTickets { get; set; }
        public decimal Price { get; set; }
    }
}

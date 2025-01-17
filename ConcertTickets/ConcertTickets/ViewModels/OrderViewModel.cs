namespace ConcertTickets.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string TicketType { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }
        public int OrderId { get; internal set; }
        public string ConcertName { get; internal set; }
        public DateTime ConcertDate { get; set; }
        public bool HasMemberCard { get; internal set; }
        public object OrderDate { get; internal set; }
    }
}


namespace ConcertTickets.Controllers
{
    internal class OrderSuccessViewModel
    {
        public int OrderId { get; set; }
        public string ConcertName { get; set; }
        public DateTime ConcertDate { get; set; }
        public string TicketType { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalPrice { get; set; }
        public object OrderDate { get; set; }
    }
}
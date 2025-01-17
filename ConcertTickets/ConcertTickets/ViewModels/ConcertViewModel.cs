using ConcertTickets.Models;
using System;
using System.Collections.Generic;

namespace ConcertTickets.ViewModels
{
    public class ConcertViewModel
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string ArtistPicture { get; set; }
        public List<TicketOfferViewModel> TicketOffers { get; set; }
        public string ArtistPictureUrl => $"/img/{Artist.Replace(" ", "").ToLower()}.png";

        public int TotalTicketsAvailable { get; set; }
    }

    public class TicketOfferViewModel
    {
        public int Id { get; set; }
        public string TicketType { get; set; }
        public decimal Price { get; set; }
        public int AvailableTickets { get; set; }
        public int TicketOfferId { get; internal set; }
        public int NumTickets { get; internal set; }
        public DateTime ConcertDate { get; internal set; }
        public string ConcertName { get; internal set; }
        public Concert Concert { get; internal set; }
        public double FinalPrice { get; internal set; }
        public string ConcertLocation { get; internal set; }
    }
}

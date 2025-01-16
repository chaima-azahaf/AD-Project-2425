using Microsoft.AspNetCore.Identity;


namespace ConcertTickets.Models
{
    public class CustomUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MemberCardNumber { get; set; }
        public bool HasMemberCard { get; internal set; }
    }
}


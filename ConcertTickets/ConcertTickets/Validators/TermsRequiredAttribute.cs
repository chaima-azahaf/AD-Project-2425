using System.ComponentModel.DataAnnotations;

namespace ConcertTickets.Attributes
{
    public class TermsRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is bool booleanValue)
            {
                return booleanValue;
            }
            return false;
        }
    }
}

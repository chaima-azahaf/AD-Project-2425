using System.ComponentModel.DataAnnotations;

public class MemberCardNumberValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var memberCardNumber = value as string;

        if (string.IsNullOrEmpty(memberCardNumber)) return ValidationResult.Success;

        if (!memberCardNumber.StartsWith("ODI") || memberCardNumber.Length != 13)
        {
            return new ValidationResult("Het lidkaartnummer moet starten met 'ODI' en uit 13 tekens bestaan.");
        }

        if (!int.TryParse(memberCardNumber.Substring(3), out _))
        {
            return new ValidationResult("De laatste 10 karakters moeten cijfers zijn.");
        }

        return ValidationResult.Success;
    }
}

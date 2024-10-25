using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CopitosCase.Contracts.Requests
{
    namespace CopitosCase.Models
    {
        public class PersonDto
        {
            [JsonPropertyName("anrede")]
            public string Salutation { get; set; }

            [Required(ErrorMessage = "Firstname is required.")]
            [JsonPropertyName("vorname")]
            public string Firstname { get; set; }

            [Required(ErrorMessage = "Lastname is required.")]
            [JsonPropertyName("nachname")]
            public string Lastname { get; set; }

            [Required(ErrorMessage = "Birthdate is required.")]
            [DataType(DataType.Date, ErrorMessage = "Birthdate must be a valid date.")]
            [BirthdateInThePast(ErrorMessage = "Birthdate must be in the past.")]
            [JsonPropertyName("geburtsdatum")]
            public DateTime Birthdate { get; set; }
            [JsonPropertyName("adresse")]
            public string Address { get; set; }
            [JsonPropertyName("plz")]
            [Required(ErrorMessage = "Zipcode is required.")]
            [RegularExpression(@"^\d{5}$", ErrorMessage = "Zipcode must be exactly 5 numeric characters.")]
            public string Zipcode { get; set; }
            [JsonPropertyName("ort")]
            public string City { get; set; }
            [JsonPropertyName("land")]
            [StringLength(2, MinimumLength = 2, ErrorMessage = "Country must be exactly 2 characters if provided.")]
            public string Country { get; set; }
        }

        public class BirthdateInThePastAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime birthdate)
                {
                    if (birthdate >= DateTime.Now)
                    {
                        return new ValidationResult(ErrorMessage ?? "Birthdate must be in the past.");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }

}

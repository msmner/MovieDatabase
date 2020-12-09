namespace MovieDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ContactForm
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]

        public string Phone { get; set; }

        [Required]
        [MaxLength(150)]

        public string Message { get; set; }

        public string ReCaptcha { get; set; }
    }
}

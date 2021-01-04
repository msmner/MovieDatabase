namespace MovieDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Common.Models;

    public class MessageContactForm : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(300)]
        public string Message { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}

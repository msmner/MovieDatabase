namespace MovieDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Common.Models;

    public class Genre : BaseModel<int>
    {
        [Required]
        public string Type { get; set; }
    }
}

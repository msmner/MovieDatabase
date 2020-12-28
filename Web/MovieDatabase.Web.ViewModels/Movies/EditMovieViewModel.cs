namespace MovieDatabase.Web.ViewModels.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class EditMovieViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(100)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Quote { get; set; }

        public string NewImageUrl { get; set; }

        [Required(ErrorMessage = "Please select a file!")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public int[] GenreIds { get; set; }

        public IEnumerable<SelectListItem> NewGenres { get; set; }
    }
}

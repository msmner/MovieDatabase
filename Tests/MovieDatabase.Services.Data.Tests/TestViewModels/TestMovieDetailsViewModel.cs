namespace MovieDatabase.Services.Data.Tests.TestViewModels
{
    using System.Linq;

    using AutoMapper;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class TestMovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string[] Genres { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, TestMovieDetailsViewModel>()
                .ForMember(x => x.Genres, opt => opt.MapFrom(y => y.MovieGenres.Select(g => g.Genre.Type)));
        }
    }
}

namespace MovieDatabase.Web.ViewModels.Users
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class UsersMovieViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public int ReviewId { get; set; }

        public int VotesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, UsersMovieViewModel>()
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(y => y.Review.Comments.Count))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(y => y.Review.Votes.Sum(x => (int)x.Type)));
        }
    }
}

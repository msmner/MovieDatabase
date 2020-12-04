﻿namespace MovieDatabase.Web.ViewModels.Movies
{
    using AutoMapper;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;
    using System;
    using System.Linq;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int ReviewId { get; set; }

        public string Quote { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public int? VotesCount { get; set; }

        public string[] Genres { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(y => y.Review.Comments.Count))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(y => y.Review.Votes.Sum(x => (int)x.Type)))
                .ForMember(x => x.Genres, opt => opt.MapFrom(y => y.Genres.Select(g => g.Type)));
        }
    }
}

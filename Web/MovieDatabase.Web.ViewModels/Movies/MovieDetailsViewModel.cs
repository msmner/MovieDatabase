﻿namespace MovieDatabase.Web.ViewModels.Movies
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int ReviewId { get; set; }

        public string Quote { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public int? VotesCount { get; set; }

        public string[] MovieGenres { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(y => y.Review.Comments.Count))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(y => y.Review.Votes.Sum(x => (int)x.Type)))
                .ForMember(x => x.MovieGenres, opt => opt.MapFrom(y => y.MovieGenres.Select(g => g.Genre.Type)))
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(y => y.Review.Comments.Count()));
        }
    }
}

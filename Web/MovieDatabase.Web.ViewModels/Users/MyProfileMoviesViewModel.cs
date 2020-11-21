namespace MovieDatabase.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class MyProfileMoviesViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public int ReviewId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MyProfileMoviesViewModel>()
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(y => y.Review.Comments.Count));
        }
    }
}

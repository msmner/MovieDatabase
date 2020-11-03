using Microsoft.EntityFrameworkCore.Internal;
using MovieDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieDatabase.Data.Seeding
{
    public class MoviesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Movies.Any())
            {
                return;
            }

            var movies = new List<string> { "Test1", "Test2", "Test3" };
            foreach (var item in movies)
            {
                var review = new Review { Content = item };

                await dbContext.Movies.AddAsync(new Movie
                {
                    Title = item,
                    Description = item,
                    ImageUrl = item,
                    Rating = 4,
                    MovieQuotes = item,
                    Review = review,
                });
            }
        }
    }
}

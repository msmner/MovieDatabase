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
                await dbContext.Movies.AddAsync(new Movie
                {
                    Title = item,
                    ImageUrl = "https://www.google.com/search?q=movie+image&sxsrf=ALeKk00DhNvBasslSA8E3Hb0WYBjCHWLVQ:1604489122252&tbm=isch&source=iu&ictx=1&fir=Q69cNILfsDIsdM%252CO-JjnKYwk-JsrM%252C_&vet=1&usg=AI4_-kSuSip6KiTXvn73lZp3C6MCElg7Cw&sa=X&ved=2ahUKEwjjrrXh4-jsAhVP-aQKHfbdC5sQ9QF6BAgKEFc#imgrc=Q69cNILfsDIsdM",
                });
            }
        }
    }
}

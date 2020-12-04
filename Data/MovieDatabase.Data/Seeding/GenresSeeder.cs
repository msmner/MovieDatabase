namespace MovieDatabase.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Models;

    public class GenresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Genres.Any())
            {
                return;
            }

            IDictionary<int, string> genres = new Dictionary<int, string>();

            genres.Add(1, "action");
            genres.Add(2, "comedy");
            genres.Add(3, "thriller");
            genres.Add(4, "drama");
            genres.Add(5, "horror");
            genres.Add(6, "documentary");

            foreach (var genre in genres)
            {
                dbContext.Genres.Add(new Genre { Id = genre.Key, Type = genre.Value });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

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

            IList<string> genres = new List<string>();

            genres.Add("action");
            genres.Add("comedy");
            genres.Add("thriller");
            genres.Add("drama");
            genres.Add("horror");
            genres.Add("documentary");

            foreach (var genre in genres)
            {
                dbContext.Genres.Add(new Genre { Type = genre });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

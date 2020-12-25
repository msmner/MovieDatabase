namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Data.Repositories;
    using Xunit;

    public class GenresServiceTests : IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRepository<Genre> genresRepository;

        public GenresServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
            this.genresRepository = new EfRepository<Genre>(this.dbContext);
        }

        [Fact]
        public async void CheckIfGetGenresWorks()
        {
            var service = await this.SetUp();
            var genres = await service.GetGenresAsync();
            Assert.Single(genres);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        private async Task<GenresService> SetUp()
        {
            var genre = new Genre { Id = 1, Type = "horror" };
            await this.dbContext.Genres.AddAsync(genre);
            await this.dbContext.SaveChangesAsync();
            return new GenresService(this.genresRepository);
        }
    }
}

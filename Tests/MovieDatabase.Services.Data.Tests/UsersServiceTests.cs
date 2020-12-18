namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Data.Repositories;
    using MovieDatabase.Services.Data.Tests.TestViewModels;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels;
    using Xunit;

    public class UsersServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private ApplicationDbContext dbContext;

        public UsersServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.moviesRepository = new EfDeletableEntityRepository<Movie>(this.dbContext);

            AutoMapperConfig.RegisterMappings(typeof(TestMovieDetailsViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task TestGetMyMovies()
        {
            var service = await this.SetUp();
            var movies = service.GetMyMovies<TestMovieDetailsViewModel>("test", 1, 5);
            Assert.Single(movies);
        }

        [Fact]
        public async Task TestGetUserByMovieId()
        {
            var service = await this.SetUp();
            var userId = service.GetUserByMovieId(6);
            Assert.Equal("test", userId);
        }

        private async Task<UsersService> SetUp()
        {
            var movie = new Movie { Id = 6, UserId = "test" };
            var secondMovie = new Movie { Id = 7, UserId = "secondTest" };

            this.dbContext.Movies.Add(movie);
            this.dbContext.Movies.Add(secondMovie);
            await this.dbContext.SaveChangesAsync();

            return new UsersService(this.moviesRepository);
        }
    }
}

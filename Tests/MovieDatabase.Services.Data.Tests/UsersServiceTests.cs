using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data;
using MovieDatabase.Data.Common.Repositories;
using MovieDatabase.Data.Models;
using MovieDatabase.Data.Repositories;
using MovieDatabase.Services.Data.Tests.TestViewModels;
using MovieDatabase.Services.Mapping;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MovieDatabase.Services.Data.Tests
{
    public class UsersServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private ApplicationDbContext dbContext;

        public UsersServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("db").Options;
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
            var userId = service.GetUserByMovieId(1);
            Assert.Equal("test", userId);
        }

        private async Task<UsersService> SetUp()
        {
            var movie = new Movie { Id = 1, UserId = "test", Title = "test", Description = "test", ImageUrl = "http://res.cloudinary.com/msmner/image/upload/v1607534147/entnuyacriutx2ykmezc.jpg", ReviewId = 1, Quote = "test" };
            var secondMovie = new Movie { Id = 2, UserId = "secondTest" };

            await this.dbContext.Movies.AddRangeAsync(movie, secondMovie);
            await this.dbContext.SaveChangesAsync();

            return new UsersService(this.moviesRepository);
        }
    }
}

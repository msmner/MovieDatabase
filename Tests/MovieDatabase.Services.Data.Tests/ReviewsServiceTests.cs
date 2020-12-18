namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Data.Repositories;
    using MovieDatabase.Services.Data.Tests.TestViewModels;
    using MovieDatabase.Services.Mapping;
    using Xunit;

    public class ReviewsServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private ApplicationDbContext dbContext;

        public ReviewsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.moviesRepository = new EfDeletableEntityRepository<Movie>(this.dbContext);
            this.reviewsRepository = new EfDeletableEntityRepository<Review>(this.dbContext);

            AutoMapperConfig.RegisterMappings(typeof(TestReviewDetailsViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task TestAddReviewAsync()
        {
            var service = await this.SetUp();
            var secondMovie = new Movie { Id = 2 };

            this.dbContext.Movies.Add(secondMovie);
            await this.dbContext.SaveChangesAsync();

            await service.AddReviewAsync(2, "test", 1);
            var review = this.dbContext.Reviews.FirstOrDefault(x => x.Id == 2);
            Assert.Equal(2, review.Id);
        }

        [Fact]
        public async Task TestGetReviewByMovieId()
        {
            var service = await this.SetUp();
            var review = service.GetReviewByMovieId<TestReviewDetailsViewModel>(1);
            Assert.Equal(1, review.MovieId);
        }

        private async Task<ReviewsService> SetUp()
        {
            var movie = new Movie { Id = 1, UserId = "test", Title = "test", Description = "test", ImageUrl = "http://res.cloudinary.com/msmner/image/upload/v1607534147/entnuyacriutx2ykmezc.jpg", ReviewId = 1, Quote = "test" };
            var review = new Review { Id = 1, MovieId = 1, Rating = 1, Content = "test" };
            movie.Review = review;

            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return new ReviewsService(this.reviewsRepository, this.moviesRepository);
        }
    }
}

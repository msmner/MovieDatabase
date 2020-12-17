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
    using Xunit;

    public class CommentsServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly ApplicationDbContext dbContext;

        public CommentsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("db").Options;
            this.dbContext = new ApplicationDbContext(options);
            this.commentsRepository = new EfDeletableEntityRepository<Comment>(this.dbContext);
            this.moviesRepository = new EfDeletableEntityRepository<Movie>(this.dbContext);
            this.reviewsRepository = new EfDeletableEntityRepository<Review>(this.dbContext);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task TestCreateCommentShouldCreate()
        {
            CommentsService service = await this.SetUp();
            var movieId = await service.CreateAsync("test", "test", 1, 3);
            Assert.Equal(2, this.dbContext.Comments.Count());
        }

        [Fact]
        public async void TestDeleteWorks()
        {
            CommentsService service = await this.SetUp();
            await service.Delete(1);
            Assert.Empty(this.dbContext.Comments);
        }

        [Fact]
        public async void TestDeleteThrowsIfNotFound()
        {
            CommentsService service = await this.SetUp();
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.Delete(2));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        [Fact]
        public async void FindReviewByCommentIdWorks()
        {
            CommentsService service = await this.SetUp();
            var id = service.FindReviewByCommentId(1);
            Assert.Equal(1, id);
        }

        [Fact]
        public async void FindReviewByCommentIdThrows()
        {
            CommentsService service = await this.SetUp();
            var exception = Assert.Throws<NullReferenceException>(() => service.FindReviewByCommentId(2));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        private async Task<CommentsService> SetUp()
        {
            var movie = new Movie { Id = 1, UserId = "test", Title = "test", Description = "test", ImageUrl = "test", ReviewId = 1, Quote = "test" };
            var review = new Review { Id = 1, MovieId = 1, Content = "test", Rating = 1 };
            var comment = new Comment { Id = 1, ReviewId = 1 };

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();

            return new CommentsService(this.commentsRepository, this.moviesRepository, this.reviewsRepository);
        }
    }
}

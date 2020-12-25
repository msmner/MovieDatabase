namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MovieDatabase.Data;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Data.Repositories;
    using MovieDatabase.Services.Data.Tests.TestViewModels;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels.Comments;
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
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
            this.commentsRepository = new EfDeletableEntityRepository<Comment>(this.dbContext);
            this.moviesRepository = new EfDeletableEntityRepository<Movie>(this.dbContext);
            this.reviewsRepository = new EfDeletableEntityRepository<Review>(this.dbContext);

            AutoMapperConfig.RegisterMappings(typeof(TestCommentDetailsViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task TestCreateCommentShouldCreate()
        {
            var service = await this.SetUp();
            var movieId = await service.CreateAsync("test", "test", 1, 3);
            Assert.Equal(2, this.dbContext.Comments.Count());
        }

        [Fact]
        public async void TestDeleteWorks()
        {
            var service = await this.SetUp();
            await service.DeleteAsync(1);
            Assert.Empty(this.dbContext.Comments);
        }

        [Fact]
        public async void TestDeleteThrowsIfNotFound()
        {
            var service = await this.SetUp();
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.DeleteAsync(2));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        [Fact]
        public async void TestGetReviewByCommentIdAsync()
        {
            var service = await this.SetUp();
            var review = await service.GetReviewByCommentIdAsync(1);
            Assert.Equal(1, review.Id);
        }

        [Fact]
        public async void GetReviewByCommentIdReturnsNull()
        {
            var service = await this.SetUp();
            var review = await service.GetReviewByCommentIdAsync(2);
            Assert.Null(review);
        }

        [Fact]
        public async void TestUpdateAsyncWorks()
        {
            var service = await this.SetUp();
            var viewModel = new EditCommentViewModel() { Id = 1, Content = "updated" };
            await service.UpdateAsync(viewModel);
            var comment = await this.dbContext.Comments.FirstOrDefaultAsync(x => x.Id == 1);
            Assert.Equal("updated", comment.Content);
        }

        [Fact]
        public async void TestGetCommentByIdAsyncWorks()
        {
            var service = await this.SetUp();
            var comment = await service.GetCommentByIdAsync<TestCommentDetailsViewModel>(1);
            Assert.Equal("test", comment.Content);
        }

        [Fact]
        public async void TestGetCommentByIdAsyncReturnsNull()
        {
            var service = await this.SetUp();
            var comment = await service.GetCommentByIdAsync<TestCommentDetailsViewModel>(2);
            Assert.Null(comment);
        }

        private async Task<CommentsService> SetUp()
        {
            var movie = new Movie { Id = 1, UserId = "test", Title = "test", Description = "test", ImageUrl = "test", Quote = "test" };
            var review = new Review { Id = 1, Content = "test", Rating = 1, MovieId = 1 };
            var comment = new Comment { Id = 1, ReviewId = 1, Content = "test" };

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();

            return new CommentsService(this.commentsRepository, this.moviesRepository, this.reviewsRepository);
        }
    }
}

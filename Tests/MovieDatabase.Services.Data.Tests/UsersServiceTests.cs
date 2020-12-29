namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ForumSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Data.Repositories;
    using MovieDatabase.Services.Data.Tests.TestViewModels;
    using MovieDatabase.Services.Mapping;
    using Xunit;

    public class UsersServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Vote> votesRepository;
        private ApplicationDbContext dbContext;

        public UsersServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.moviesRepository = new EfDeletableEntityRepository<Movie>(this.dbContext);
            this.commentsRepository = new EfDeletableEntityRepository<Comment>(this.dbContext);
            this.reviewsRepository = new EfDeletableEntityRepository<Review>(this.dbContext);
            this.usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.dbContext);
            this.votesRepository = new EfRepository<Vote>(this.dbContext);

            AutoMapperConfig.RegisterMappings(typeof(TestMovieDetailsViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task TestGetMovies()
        {
            var service = await this.SetUp();
            var movies = await service.GetMoviesByUserAsync<TestMovieDetailsViewModel>("test", 1, 5);
            Assert.Single(movies);
        }

        [Fact]
        public async Task TestGetReviews()
        {
            var service = await this.SetUp();
            var reviews = await service.GetReviewsByUserAsync<TestReviewDetailsViewModel>("test");
            Assert.Equal(2, reviews.Count());
        }

        [Fact]
        public async Task TestGetComments()
        {
            var service = await this.SetUp();
            var comments = await service.GetCommentsByUserAsync<TestCommentDetailsViewModel>("test");
            Assert.Equal(2, comments.Count());
        }

        [Fact]
        public async Task TestGetUserByMovieId()
        {
            var service = await this.SetUp();
            var user = await service.GetUserByMovieIdAsync(6);
            Assert.Equal("test", user.Id);
        }

        [Fact]
        public async Task TestCommentsCountByUserIdWorks()
        {
            var service = await this.SetUp();
            var count = service.CommentsCountByUserId("test");
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task TestVotesCountByUserIdWorks()
        {
            var service = await this.SetUp();
            var count = service.VotesCountByUserId("test");
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetMostCommentedReviewsByUserIdWorks()
        {
            var service = await this.SetUp();
            var reviews = await service.GetMostCommentedReviewsByUserId<TestReviewStatisticsViewModel>("test");
            Assert.Equal(7, reviews.ToList()[0].Id);
        }

        [Fact]
        public async Task GetMostVotedReviewsByUserIdWorks()
        {
            var service = await this.SetUp();
            var reviews = await service.GetMostVotedReviewsByUserId<TestReviewStatisticsViewModel>("test");
            Assert.Equal(7, reviews.ToList()[0].Id);
        }

        private async Task<UsersService> SetUp()
        {
            var movie = new Movie { Id = 6, UserId = "test" };
            var secondMovie = new Movie { Id = 7, UserId = "secondTest" };
            var review = new Review { Id = 1, UserId = "test" };
            var secondReview = new Review { Id = 7, UserId = "test" };
            var comment = new Comment { Id = 1, UserId = "test", ReviewId = 7 };
            var secondComment = new Comment { Id = 2, UserId = "test", ReviewId = 7 };
            var user = new ApplicationUser { Id = "test" };
            var vote = new Vote { Id = 1, UserId = "test", ReviewId = 7 };
            var secondVote = new Vote { Id = 2, UserId = "test", ReviewId = 7 };

            movie.User = user;

            await this.dbContext.Votes.AddRangeAsync(vote, secondVote);
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.Comments.AddRangeAsync(comment, secondComment);
            await this.dbContext.Movies.AddRangeAsync(movie, secondMovie);
            await this.dbContext.Reviews.AddRangeAsync(review, secondReview);
            await this.dbContext.SaveChangesAsync();

            return new UsersService(this.moviesRepository, this.reviewsRepository, this.commentsRepository, this.usersRepository, this.votesRepository);
        }
    }
}

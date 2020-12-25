namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Data;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Data.Repositories;
    using Xunit;

    public class VotesServiceTests : IDisposable
    {
        private readonly IRepository<Vote> votesRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private ApplicationDbContext dbContext;

        public VotesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.votesRepository = new EfRepository<Vote>(this.dbContext);
            this.moviesRepository = new EfDeletableEntityRepository<Movie>(this.dbContext);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task TestGetVotesForReviewReturnsCorrectCount()
        {
            var service = await this.SetUp();
            var votes = service.GetVotesCount(1, 0);
            Assert.Equal(1, votes);
        }

        [Fact]
        public async Task TestGetVotesForCommentReturnsCorrectCount()
        {
            var service = await this.SetUp();
            var votes = service.GetVotesCount(0, 1);
            Assert.Equal(1, votes);
        }

        [Fact]
        public async Task TestVoteAsyncChangesReviewVoteIfVoteAlreadyInBase()
        {
            var service = await this.SetUp();
            await service.VoteAsync(1, 0, "test", false);
            var vote = this.dbContext.Votes.FirstOrDefault(x => x.ReviewId == 1);
            Assert.Equal(-1, (int)vote.Type);
        }

        [Fact]
        public async Task TestVoteAsyncChangesCommentVoteIfVoteAlreadyInBase()
        {
            var service = await this.SetUp();
            await service.VoteAsync(0, 1, "test", false);
            var vote = this.dbContext.Votes.FirstOrDefault(x => x.CommentId == 1);
            Assert.Equal(-1, (int)vote.Type);
        }

        [Fact]
        public async Task TestVoteAsyncCreatesReviewVoteIfItDoesntExist()
        {
            var service = await this.SetUp();
            await service.VoteAsync(1, 0, "test2", true);
            var review = await this.dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == 1);
            Assert.Equal(2, review.Votes.Count());
        }

        [Fact]
        public async Task TestVoteAsyncCreatesCommentVoteIfItDoesntExist()
        {
            var service = await this.SetUp();
            await service.VoteAsync(0, 1, "test2", true);
            var comment = await this.dbContext.Comments.FirstOrDefaultAsync(x => x.Id == 1);
            Assert.Equal(2, comment.Votes.Count());
        }

        private async Task<VotesService> SetUp()
        {
            var review = new Review { Id = 1 };
            var comment = new Comment { Id = 1 };
            var reviewVote = new Vote { Id = 1, ReviewId = 1, Type = VoteType.UpVote, UserId = "test" };
            var commentVote = new Vote { Id = 2, CommentId = 1, Type = VoteType.UpVote, UserId = "test" };
            review.Votes.Add(reviewVote);
            comment.Votes.Add(commentVote);

            var movie = new Movie { Id = 1, ReviewId = 1 };

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.Votes.AddRangeAsync(reviewVote, commentVote);
            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();

            return new VotesService(this.votesRepository);
        }
    }
}

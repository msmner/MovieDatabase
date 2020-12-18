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
            var votes = service.GetVotesForReview(1);
            Assert.Equal(1, votes);
        }

        [Fact]
        public async Task TestVoteAsyncChangesVoteIfVoteAlreadyInBase()
        {
            var service = await this.SetUp();
            await service.VoteAsync(1, "test", false);
            var vote = this.dbContext.Votes.FirstOrDefault(x => x.ReviewId == 1);
            Assert.Equal(-1, (int)vote.Type);
        }

        [Fact]
        public async Task TestVoteAsyncCreatesVoteIfItDoesntExist()
        {
            var service = await this.SetUp();
            await service.VoteAsync(2, "test", true);
            var movie = this.dbContext.Movies.FirstOrDefault(x => x.ReviewId == 2);
            Assert.Single(movie.Votes);
        }

        private async Task<VotesService> SetUp()
        {
            var review = new Review { Id = 1 };
            var vote = new Vote { Id = 1, ReviewId = 1, Type = VoteType.UpVote, UserId = "test" };
            review.Votes.Add(vote);

            var movie = new Movie { Id = 2, ReviewId = 2 };

            this.dbContext.Movies.Add(movie);
            await this.dbContext.Votes.AddRangeAsync(vote);
            await this.dbContext.SaveChangesAsync();

            return new VotesService(this.votesRepository, this.moviesRepository);
        }
    }
}

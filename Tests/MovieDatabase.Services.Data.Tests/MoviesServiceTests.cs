namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
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

    public class MoviesServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IRepository<Genre> genresRepository;
        private readonly IRepository<Vote> votesRepository;
        private readonly IRepository<MovieGenre> movieGenresRepository;
        private readonly ApplicationDbContext dbContext;

        public MoviesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.commentsRepository = new EfDeletableEntityRepository<Comment>(this.dbContext);
            this.moviesRepository = new EfDeletableEntityRepository<Movie>(this.dbContext);
            this.reviewsRepository = new EfDeletableEntityRepository<Review>(this.dbContext);
            this.genresRepository = new EfRepository<Genre>(this.dbContext);
            this.votesRepository = new EfRepository<Vote>(this.dbContext);
            this.movieGenresRepository = new EfRepository<MovieGenre>(this.dbContext);

            AutoMapperConfig.RegisterMappings(typeof(TestMovieDetailsViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task TestCreateMovieWorks()
        {
            var service = await this.SetUp();
            var ids = new[] { 1 };
            var movieId = await service.CreateMovieAsync("test2", "test2", "test2", ids, "test2", "test2");
            Assert.Equal(2, this.dbContext.Movies.Count());
        }

        [Fact]
        public async Task IsMovieCreatorLoggedInReturnsTrue()
        {
            var service = await this.SetUp();
            var check = await service.IsMovieCreatorLoggedIn("test", 1);
            Assert.True(check);
        }

        [Fact]
        public async Task IsMovieCreatorLoggedInReturnsFalse()
        {
            var service = await this.SetUp();
            var check = await service.IsMovieCreatorLoggedIn("testtest", 1);
            Assert.False(check);
        }

        [Fact]
        public async void CheckDeleteWorks()
        {
            var service = await this.SetUp();
            await service.Delete(1);

            Assert.Empty(this.dbContext.Movies);
            Assert.Empty(this.dbContext.Reviews);
            Assert.Empty(this.dbContext.Comments);
        }

        [Fact]
        public async Task CheckDeleteThrows()
        {
            var service = await this.SetUp();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => service.Delete(3));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        [Fact]
        public async Task CheckGetTop10MoviesWithHighesVotesCountWorks()
        {
            var service = await this.SetUp();
            var secondMovie = new Movie { Id = 2, UserId = "test2", Title = "test2", Description = "test", ImageUrl = "test2", ReviewId = 2, Quote = "test2" };
            var anotherVote = new Vote { Id = 2, UserId = "test2", ReviewId = 2, Type = VoteType.UpVote };
            secondMovie.Votes.Add(anotherVote);

            await this.dbContext.Votes.AddAsync(anotherVote);
            await this.dbContext.Movies.AddAsync(secondMovie);
            await this.dbContext.SaveChangesAsync();
            var movies = service.GetTop10MoviesWithHighestRating<TestMovieDetailsViewModel>();

            Assert.Equal("test2", movies.ToList()[0].UserId);
        }

        [Fact]
        public async Task TestGetByIdWorks()
        {
            var service = await this.SetUp();
            var result = service.GetByIdAsync<TestMovieDetailsViewModel>(1);
            Assert.Equal("test", result.UserId);
        }

        [Fact]
        public async Task TestGetMoviesCountByUserId()
        {
            var service = await this.SetUp();
            var count = service.GetMoviesCountByUserId("test");
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task TestGetMoviesByGenre()
        {
            //var service = await this.SetUp();
            //var genres = new List<Genre> { new Genre { Type = "horror" } };
            //var secondMovie = new Movie { UserId = "anotherTest", MovieGenres = genres };
            //this.dbContext.Movies.Add(secondMovie);
            //await this.dbContext.SaveChangesAsync();
            //var movies = service.GetMoviesByGenre<TestMovieDetailsViewModel>("horror");
            //Assert.Equal("anotherTest", movies.ToList()[0].UserId);
            //Assert.Equal(2, movies.Count());
        }

        [Fact]
        public async Task TestGetMoviesCountByGenre()
        {
            var service = await this.SetUp();
            var count = service.GetMoviesCountByGenre("horror");
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task TestGetMoviesByTitle()
        {
            var service = await this.SetUp();
            var movies = service.GetMoviesByTitleAsync<TestMovieDetailsViewModel>("test");
            Assert.Single(movies);
        }

        private async Task<MoviesService> SetUp()
        {
            var movie = new Movie { Id = 1, UserId = "test", Title = "test", ReviewId = 1 };
            var review = new Review { Id = 1, MovieId = 1, Content = "test", Rating = 1 };
            var comment = new Comment { Id = 1, Content = "test", UserId = "test", ReviewId = 1, ParentId = 3 };
            var genre = new Genre { Id = 1, Type = "horror" };
            var user = new ApplicationUser { Id = "test" };
            var vote = new Vote { Id = 1, UserId = "test", ReviewId = 1, Type = VoteType.DownVote };
            movie.Votes.Add(vote);
            // movie.Genres.Add(genre);

            this.dbContext.Users.Add(user);
            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.Genres.AddAsync(genre);
            await this.dbContext.Votes.AddAsync(vote);
            await this.dbContext.SaveChangesAsync();

            return new MoviesService(this.moviesRepository, this.genresRepository, this.reviewsRepository, this.commentsRepository, this.movieGenresRepository);
        }
    }
}

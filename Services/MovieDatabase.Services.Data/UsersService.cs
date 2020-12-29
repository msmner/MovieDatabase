namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Vote> votesRepository;

        public UsersService(IDeletableEntityRepository<Movie> moviesRepository, IDeletableEntityRepository<Review> reviewsRepository, IDeletableEntityRepository<Comment> commentsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository, IRepository<Vote> votesRepository)
        {
            this.moviesRepository = moviesRepository;
            this.reviewsRepository = reviewsRepository;
            this.commentsRepository = commentsRepository;
            this.usersRepository = usersRepository;
            this.votesRepository = votesRepository;
        }

        public async Task<IEnumerable<T>> GetMoviesByUserAsync<T>(string userId, int page, int itemsPerPage)
        {
            var movies = await this.moviesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>().ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<T>> GetReviewsByUserAsync<T>(string userId)
        {
            return await this.reviewsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetCommentsByUserAsync<T>(string userId)
        {
            return await this.commentsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByMovieIdAsync(int? movieId)
        {
            return await this.usersRepository.All().Where(x => x.Movies.Any(x => x.Id == movieId)).FirstOrDefaultAsync();
        }

        public int CommentsCountByUserId(string userId)
        {
            return this.commentsRepository.All().Where(x => x.UserId == userId).Count();
        }

        public int VotesCountByUserId(string userId)
        {
            return this.votesRepository.All().Where(x => x.UserId == userId).Count();
        }

        public async Task<IEnumerable<T>> GetMostCommentedReviewsByUserId<T>(string userId)
        {
            return await this.reviewsRepository.All().Where(x => x.UserId == userId).OrderByDescending(x => x.Comments.Count()).To<T>().Take(5).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetMostVotedReviewsByUserId<T>(string userId)
        {
            return await this.reviewsRepository.All().Where(x => x.UserId == userId).OrderByDescending(x => x.Votes.Count()).To<T>().Take(5).ToListAsync();
        }
    }
}

namespace MovieDatabase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public UsersService(IDeletableEntityRepository<Movie> moviesRepository, IDeletableEntityRepository<Review> reviewsRepository, IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.moviesRepository = moviesRepository;
            this.reviewsRepository = reviewsRepository;
            this.commentsRepository = commentsRepository;
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
            var movie = await this.moviesRepository.All().Where(x => x.Id == movieId).FirstOrDefaultAsync();
            return movie.User;
        }
    }
}

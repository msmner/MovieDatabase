namespace MovieDatabase.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository, IDeletableEntityRepository<Movie> moviesRepository, IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.commentsRepository = commentsRepository;
            this.moviesRepository = moviesRepository;
            this.reviewsRepository = reviewsRepository;
        }

        public async Task<int> CreateAsync(string content, string userId, int reviewId, int? parentId)
        {
            var comment = new Comment
            {
                Content = content,
                UserId = userId,
                ReviewId = reviewId,
                ParentId = parentId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();

            var movie = this.moviesRepository.All().Where(x => x.Review.Id == comment.ReviewId).FirstOrDefault();
            return movie.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await this.commentsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.commentsRepository.Delete(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<Review> GetReviewByCommentIdAsync(int id)
        {
            return await this.reviewsRepository.All().FirstOrDefaultAsync(x => x.Comments.Any(y => y.Id == id));
        }

        public async Task UpdateAsync(EditCommentViewModel input)
        {
            var comment = await this.commentsRepository.All().FirstOrDefaultAsync(x => x.Id == input.Id);

            comment.Content = input.Content;

            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<T> GetCommentByIdAsync<T>(int id)
        {
           return await this.commentsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }
    }
}

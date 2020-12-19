namespace MovieDatabase.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task Delete(int id)
        {
            var comment = this.commentsRepository.All().FirstOrDefault(x => x.Id == id);
            this.commentsRepository.Delete(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public int FindReviewByCommentId(int id)
        {
            return this.reviewsRepository.All().FirstOrDefault(x => x.Comments.Any(y => y.Id == id)).Id;
        }

        public async Task UpdateAsync(int id, EditCommentViewModel input)
        {
            var comment = this.commentsRepository.All().FirstOrDefault(x => x.Id == id);

            comment.Id = id;
            comment.Content = input.Content;

            await this.commentsRepository.SaveChangesAsync();
        }

        public T GetCommentById<T>(int id)
        {
           return this.commentsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }
    }
}

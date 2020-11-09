namespace MovieDatabase.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository, IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.commentsRepository = commentsRepository;
            this.moviesRepository = moviesRepository;
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
    }
}

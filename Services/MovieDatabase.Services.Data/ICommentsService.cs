namespace MovieDatabase.Services.Data
{
    using System.Threading.Tasks;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, int reviewId, int? parentId);

        Task DeleteAsync(int id);

        Task<Review> GetReviewByCommentIdAsync(int id);

        Task<T> GetCommentByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditCommentViewModel input);
    }
}

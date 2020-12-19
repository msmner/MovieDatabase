namespace MovieDatabase.Services.Data
{
    using System.Threading.Tasks;

    using MovieDatabase.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, int reviewId, int? parentId);

        Task Delete(int id);

        int FindReviewByCommentId(int id);

        T GetCommentById<T>(int id);

        Task UpdateAsync(int id, EditCommentViewModel input);
    }
}

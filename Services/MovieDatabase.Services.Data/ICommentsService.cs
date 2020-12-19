namespace MovieDatabase.Services.Data
{
    using MovieDatabase.Web.ViewModels.Comments;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, int reviewId, int? parentId);

        Task Delete(int id);

        int FindReviewByCommentId(int id);

        T GetCommentById<T>(int id);

        Task UpdateAsync(int id, EditCommentViewModel input);
    }
}

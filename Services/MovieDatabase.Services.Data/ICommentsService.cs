namespace MovieDatabase.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, int reviewId, int? parentId);
    }
}

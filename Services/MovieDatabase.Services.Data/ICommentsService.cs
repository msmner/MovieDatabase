using System.Threading.Tasks;

namespace MovieDatabase.Services.Data
{
    public interface ICommentsService
    {
        Task<int> CreateAsync(string content, string userId, int reviewId, int? parentId);
    }
}

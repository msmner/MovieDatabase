namespace MovieDatabase.Services.Data
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels.Reviews;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task AddReviewAsync(int movieId, string content, int rating, string firstQuote, string secondQuote, string thirdQuote);

        T GetReviewByMovieId<T>(int movieId);
    }
}

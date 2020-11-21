namespace MovieDatabase.Services.Data
{
    using System.Threading.Tasks;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels.Reviews;

    public interface IReviewsService
    {
        Task AddReviewAsync(int movieId, string content, int rating, string firstQuote, string secondQuote, string thirdQuote);

        T GetReviewByMovieId<T>(int movieId);
    }
}

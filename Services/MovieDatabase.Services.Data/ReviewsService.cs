namespace MovieDatabase.Services.Data
{
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.ViewModels.Reviews;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task AddReviewAsync(int movieId, string content, int rating, string firstQuote, string secondQuote, string thirdQuote)
        {
            var review = new Review
            {
                MovieId = movieId,
                Content = content,
                Rating = rating,
                FirstQuote = firstQuote,
                SecondQuote = secondQuote,
                ThirdQuote = thirdQuote,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }
    }
}

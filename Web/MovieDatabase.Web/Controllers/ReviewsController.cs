﻿namespace MovieDatabase.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;
    using MovieDatabase.Services.Mapping;
    using MovieDatabase.Web.ViewModels.Reviews;
    using System.Threading.Tasks;

    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public IActionResult Create(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewInputViewModel input, int id)
        {
            await this.reviewsService.AddReviewAsync(id,input.Content,input.Rating,input.FirstQuote,input.SecondQuote,input.ThirdQuote);

            return this.Redirect("/");
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.reviewsService.GetReviewByMovieId<ReviewDetailsViewModel>(id);
            return this.View(viewModel);
        }
    }
}

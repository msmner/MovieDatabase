namespace MovieDatabase.Web.ViewModels.Reviews
{
    using System.Collections.Generic;

    public class AllReviewsViewModel
    {
        public IEnumerable<SingleReviewViewModel> Reviews { get; set; }
    }
}

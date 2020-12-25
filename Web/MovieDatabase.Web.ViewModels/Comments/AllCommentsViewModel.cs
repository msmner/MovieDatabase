namespace MovieDatabase.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    public class AllCommentsViewModel
    {
        public IEnumerable<SingleCommentViewModel> Comments { get; set; }
    }
}

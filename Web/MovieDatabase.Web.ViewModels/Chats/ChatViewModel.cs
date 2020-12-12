namespace MovieDatabase.Web.ViewModels.Chats
{
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Mapping;

    public class ChatViewModel : IMapFrom<Movie>
    {
        public string Title { get; set; }
    }
}

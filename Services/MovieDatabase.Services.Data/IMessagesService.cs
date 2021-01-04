namespace MovieDatabase.Services.Data
{
    using System.Threading.Tasks;

    public interface IMessagesService
    {
        Task<string> SendContactMessage(string message, string email);
    }
}

namespace MovieDatabase.Services.Data
{
    using System.Threading.Tasks;

    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<MessageContactForm> messagesRepository;

        public MessagesService(IDeletableEntityRepository<MessageContactForm> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task<string> SendContactMessage(string message, string email)
        {
            await this.messagesRepository.AddAsync(new MessageContactForm { Message = message, UserEmail = email });
            await this.messagesRepository.SaveChangesAsync();
            return email;
        }
    }
}

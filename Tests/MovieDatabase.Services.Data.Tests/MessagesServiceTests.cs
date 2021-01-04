namespace MovieDatabase.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data;
    using MovieDatabase.Data.Common.Repositories;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Data.Repositories;
    using Xunit;

    public class MessagesServiceTests : IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDeletableEntityRepository<MessageContactForm> messagesRepository;

        public MessagesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
            this.messagesRepository = new EfDeletableEntityRepository<MessageContactForm>(this.dbContext);
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckSendContactMessageWorks()
        {
            var service = await this.SetUp();
            var email = await service.SendContactMessage("test", "test@test.test");
            Assert.Equal("test@test.test", email);
        }

        private async Task<MessagesService> SetUp()
        {
            var message = new MessageContactForm { Id = 1, Message = "test", UserEmail = "test@test.test" };
            await this.dbContext.Messages.AddAsync(message);
            await this.dbContext.SaveChangesAsync();
            return new MessagesService(this.messagesRepository);
        }
    }
}

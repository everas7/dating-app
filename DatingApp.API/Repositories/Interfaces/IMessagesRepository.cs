using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Domain.Users.Requests;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Repositories.Interfaces
{
    public interface IMessagesRepository
    {
        Task<Message> Get(int id);
        Task<PaginatedList<Message>> GetMessagesForUser();
        Task GetMessageThread(int userId, int recipientId);
        Task Create(Message message);
        Task Delete(Message message);
    }
}
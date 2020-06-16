using System;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Repositories.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DatingApp.API.Helpers;
using System.Linq;
using DatingApp.API.Domain.Users.Requests;
using Helpers;

namespace DatingApp.API.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly DataContext _context;
        public MessagesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Message message)
        {
            _context.Messages.Add(message);
            var success = await _context.SaveChangesAsync() > 0;

            if (!success)
                throw new Exception("Problem saving message!");
        }

        public async Task Delete(Message message)
        {
            _context.Messages.Remove(message);
            if (!(await _context.SaveChangesAsync() > 0))
                throw new Exception("Problem saving changes");
        }

        public async Task<Message> Get(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public Task<PaginatedList<Message>> GetMessagesForUser()
        {
            throw new NotImplementedException();
        }

        public Task GetMessageThread(int userId, int recipientId)
        {
            throw new NotImplementedException();
        }
    }
}
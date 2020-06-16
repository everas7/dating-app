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
using DatingApp.API.Domain.Messages.Requests;

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

        public async Task Update(Message message)
        {
            _context.Messages.Update(message);
            if (!(await _context.SaveChangesAsync() > 0))
                throw new Exception("Problem saving changes");
        }


        public async Task<Message> Get(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<PaginatedList<Message>> GetMessagesForUser(MessageListRequest request)
        {
            var messages = _context.Messages
                .Include(u => u.Sender).ThenInclude(s => s.Photos)
                .Include(u => u.Recipient).ThenInclude(r => r.Photos)
                .AsQueryable();


            switch (request.MessageContainer)
            {
                case MessageContainer.Inbox:
                    messages = messages.Where(m => m.RecipientId == request.UserId && !m.RecipientDeleted);
                    break;
                case MessageContainer.Outbox:
                    messages = messages.Where(m => m.SenderId == request.UserId && !m.SenderDeleted);
                    break;
                default:
                    messages = messages.Where(m => m.RecipientId == request.UserId && !m.IsRead && !m.RecipientDeleted);
                    break;
            }

            messages = messages.OrderByDescending(m => m.MessageSent);
            return await PaginatedList<Message>.CreateAsync(messages, request.Page, request.PerPage);
        }

        public async Task<List<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(s => s.Photos)
                .Include(u => u.Recipient).ThenInclude(r => r.Photos)
                .Where(m => (m.SenderId == userId && m.RecipientId == recipientId && !m.SenderDeleted) ||
                    (m.SenderId == recipientId && m.RecipientId == userId && !m.RecipientDeleted))
                .OrderBy(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }
    }
}
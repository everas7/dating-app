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
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Like like)
        {
            _context.Likes.Add(like);
            var success = await _context.SaveChangesAsync() > 0;

            if (!success)
                throw new Exception("Problem saving like!");
        }

        public async Task Delete(Like like)
        {
            _context.Likes.Remove(like);
            if (!(await _context.SaveChangesAsync() > 0))
                throw new Exception("Problem saving changes");
        }
        public async Task<Like> Get(int likerId, int likeeId)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(l => l.LikerId == likerId && l.LikeeId == likeeId);
        }
    }
}
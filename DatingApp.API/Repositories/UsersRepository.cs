using System;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Repositories.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatingApp.API.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public Task Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.Include(u => u.Photos).ToListAsync();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
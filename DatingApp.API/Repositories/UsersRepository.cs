using System;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Repositories.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DatingApp.API.Helpers;
using System.Linq;

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

        public async Task<PaginatedList<User>> GetAll(PaginationParams paginationParams)
        {
            var users = _context.Users.Include(u => u.Photos).AsQueryable();
            return await PaginatedList<User>.CreateAsync(users, paginationParams.Page, paginationParams.PerPage);

        }

        public async Task Update(User user)
        {
            _context.Update(user);
            var success = await _context.SaveChangesAsync() > 0;

            if (!success)
                throw new Exception("Problem saving changes!");
        }
    }
}
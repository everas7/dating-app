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

        public async Task<PaginatedList<User>> GetAll(RequestForUserList request)
        {
            var users = _context.Users
                .Include(u => u.Photos)
                .AsQueryable();

            users = users.Where(u => u.Id != request.UserId);
            users = users.Where(u => u.Gender == request.Gender);

            if (request.MinAge > 0)
            {
                var maxDOB = DateTime.Now.AddYears(-request.MinAge);
                users = users.Where(u => u.DateOfBirth <= maxDOB);
            }
            if (request.MaxAge > 0)
            {
                var minDOB = DateTime.Now.AddYears(-request.MaxAge-1);
                users = users.Where(u => u.DateOfBirth >= minDOB);
            }

            return await PaginatedList<User>.CreateAsync(users, request.Page, request.PerPage);

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
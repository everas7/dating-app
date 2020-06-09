using System;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.Repositories.Interfaces;

namespace DatingApp.API.Repositories
{
    public class PhotosRepository : IPhotosRepository
    {
        private readonly DataContext _context;
        public PhotosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Photo> Get(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task Create(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
            if (!(await _context.SaveChangesAsync() > 0))
                throw new Exception("Problem saving photo");
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Photo photo)
        {
            throw new System.NotImplementedException();
        }
    }
}
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

        public async Task Delete(Photo photo)
        {
            _context.Photos.Remove(photo);
            if (!(await _context.SaveChangesAsync() > 0))
                throw new Exception("Problem saving changes");
        }

        public async Task Update(Photo photo)
        {
            _context.Photos.Update(photo);
            if (!(await _context.SaveChangesAsync() > 0))
                throw new Exception("Problem saving changes");
        }
    }
}
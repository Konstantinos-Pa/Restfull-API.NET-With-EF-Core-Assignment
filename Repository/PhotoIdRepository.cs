using Assignment.Models;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository
{
    public class PhotoIdRepository:IPhotoIdRepository
    {
        private readonly PostgresDbContext _context;
        public PhotoIdRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<List<PhotoId>> GetPhotoIdsAsync()
        {
            return await _context.photoIds.ToListAsync();
        }

        public async Task<PhotoId> GetPhotoIdByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from GetPhotoIdByIdAsync)");
            }
            PhotoId? photoId = await _context.photoIds.FirstOrDefaultAsync(p => p.Id == id);
            if (photoId == null)
            {
                throw new Exception("PhotoId not found (Thrown from GetPhotoIdByIdAsync)");
            }
            return photoId;
        }

        public async Task AddPhotoIdAsync(PhotoId photoId)
        {
            if (photoId == null)
            {
                throw new ArgumentNullException(nameof(photoId) + "Is Null (Thrown from AddPhotoIdAsync)");
            }
            await _context.photoIds.AddAsync(photoId);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhotoIdAsync(int id, PhotoId photoId)
        {
            if (photoId == null)
            {
                throw new ArgumentNullException(nameof(photoId) + "Is Null (Thrown from UpdatePhotoIdAsync)");
            }
            else if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from UpdatePhotoIdAsync)");
            }
            PhotoId existingPhotoId = await GetPhotoIdByIdAsync(id);

            existingPhotoId.PhotoIdImage = photoId.PhotoIdImage;
            existingPhotoId.PhotoIdNumber = photoId.PhotoIdNumber;
            existingPhotoId.DateOfIssue = photoId.DateOfIssue;
            existingPhotoId.CandidateNumber = photoId.CandidateNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePhotoIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from DeletePhotoIdAsync)");
            }
            PhotoId photoId = await GetPhotoIdByIdAsync(id);
            _context.photoIds.Remove(photoId);
            await _context.SaveChangesAsync();
        }
    }
}
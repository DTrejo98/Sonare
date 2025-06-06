using Microsoft.EntityFrameworkCore;
using Sonare.Data;
using Sonare.Models;
using Sonare.Interfaces;

namespace Sonare.Repositories
{
    public class ClipRepository : IClipRepository
    {
        private readonly AppDbContext _context;

        public ClipRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Clip>> GetAllClips()
        {
            return await _context.Clips.ToListAsync();
        }

        public async Task<Clip?> GetClipById(int id)
        {
            return await _context.Clips.FindAsync(id);
        }

        public async Task<Clip?> CreateClip(Clip clip)
        {
            var result = await _context.Clips.AddAsync(clip);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Clip?> UpdateClip(int id, Clip clip)
        {
            var existingClip = await _context.Clips.FindAsync(id);
            if (existingClip == null) return null;

            existingClip.Title = clip.Title;
            existingClip.Description = clip.Description;
            existingClip.MediaUrl = clip.MediaUrl;
            existingClip.IsFinalMix = clip.IsFinalMix;
            await _context.SaveChangesAsync();
            return existingClip;
        }

        public async Task<Clip> DeleteClip(int id)
        {
            var clip = await _context.Clips.FindAsync(id);
            if (clip == null) throw new Exception("Clip not found");

            _context.Clips.Remove(clip);
            await _context.SaveChangesAsync();
            return clip;
        }
    }
}

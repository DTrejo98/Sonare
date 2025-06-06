using Microsoft.EntityFrameworkCore;
using Sonare.Data;
using Sonare.Models;
using Sonare.Interfaces;

namespace Sonare.Repositories
{
    public class ClipCollaboratorRepository : IClipCollaboratorRepository
    {
        private readonly AppDbContext _context;

        public ClipCollaboratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClipCollaborator>> GetAllClipCollaborators()
        {
            return await _context.ClipCollaborators.ToListAsync();
        }

        public async Task<ClipCollaborator?> GetClipCollaboratorByIds(int clipId, int userId)
        {
            return await _context.ClipCollaborators
                .FirstOrDefaultAsync(c => c.ClipId == clipId && c.UserId == userId);
        }

        public async Task<ClipCollaborator?> CreateClipCollaborator(ClipCollaborator collaborator)
        {
            await _context.ClipCollaborators.AddAsync(collaborator);
            await _context.SaveChangesAsync();
            return collaborator;
        }

        public async Task<ClipCollaborator> DeleteClipCollaborator(int clipId, int userId)
        {
            var collaborator = await GetClipCollaboratorByIds(clipId, userId);
            if (collaborator == null) throw new Exception("ClipCollaborator not found");

            _context.ClipCollaborators.Remove(collaborator);
            await _context.SaveChangesAsync();
            return collaborator;
        }
    }
}

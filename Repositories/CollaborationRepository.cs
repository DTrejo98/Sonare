using Microsoft.EntityFrameworkCore;
using Sonare.Data;
using Sonare.Models;
using Sonare.Interfaces;

namespace Sonare.Repositories
{
    public class CollaborationRepository : ICollaborationRepository
    {
        private readonly AppDbContext _context;

        public CollaborationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Collaboration>> GetAllCollaborations()
        {
            return await _context.Collaborations.ToListAsync();
        }

        public async Task<Collaboration?> GetCollaborationById(int id)
        {
            return await _context.Collaborations.FindAsync(id);
        }

        public async Task<Collaboration?> CreateCollaboration(Collaboration collaboration)
        {
            var result = await _context.Collaborations.AddAsync(collaboration);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Collaboration> DeleteCollaboration(int id)
        {
            var collab = await _context.Collaborations.FindAsync(id);
            if (collab == null) throw new Exception("Collaboration not found");

            _context.Collaborations.Remove(collab);
            await _context.SaveChangesAsync();
            return collab;
        }
    }
}

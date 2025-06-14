using Sonare.Models;

namespace Sonare.Interfaces
{
    public interface ICollaborationService
    {
        Task<List<Collaboration>> GetAllCollaborations();
        Task<Collaboration?> GetCollaborationById(int id);
        Task<Collaboration?> CreateCollaboration(Collaboration collaboration);
        Task<Collaboration?> UpdateCollaboration(int id, Collaboration updatedCollaboration);
        Task<Collaboration> DeleteCollaboration(int id);
    }
}

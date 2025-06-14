using Sonare.Models;

namespace Sonare.Interfaces
{
    public interface IClipCollaboratorService
    {
        Task<List<ClipCollaborator>> GetAllClipCollaborators();
        Task<ClipCollaborator?> GetClipCollaboratorByIds(int clipId, int userId);
        Task<ClipCollaborator?> CreateClipCollaborator(ClipCollaborator collaborator);
        Task<ClipCollaborator> DeleteClipCollaborator(int clipId, int userId);
    }
}

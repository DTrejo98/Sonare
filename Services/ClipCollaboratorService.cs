using Sonare.Interfaces;
using Sonare.Models;

namespace Sonare.Services
{
    public class ClipCollaboratorService : IClipCollaboratorService
    {
        private readonly IClipCollaboratorRepository _clipCollaboratorRepository;

        public ClipCollaboratorService(IClipCollaboratorRepository clipCollaboratorRepository)
        {
            _clipCollaboratorRepository = clipCollaboratorRepository;
        }

        public async Task<List<ClipCollaborator>> GetAllClipCollaborators()
        {
            return await _clipCollaboratorRepository.GetAllClipCollaborators();
        }

        public async Task<ClipCollaborator?> GetClipCollaboratorByIds(int clipId, int userId)
        {
            return await _clipCollaboratorRepository.GetClipCollaboratorByIds(clipId, userId);
        }

        public async Task<ClipCollaborator?> CreateClipCollaborator(ClipCollaborator collaborator)
        {
            return await _clipCollaboratorRepository.CreateClipCollaborator(collaborator);
        }

        public async Task<ClipCollaborator> DeleteClipCollaborator(int clipId, int userId)
        {
            return await _clipCollaboratorRepository.DeleteClipCollaborator(clipId, userId);
        }
    }
}

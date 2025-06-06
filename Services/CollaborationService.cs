using Sonare.Interfaces;
using Sonare.Models;

namespace Sonare.Services
{
    public class CollaborationService : ICollaborationService
    {
        private readonly ICollaborationRepository _collaborationRepository;

        public CollaborationService(ICollaborationRepository collaborationRepository)
        {
            _collaborationRepository = collaborationRepository;
        }

        public async Task<List<Collaboration>> GetAllCollaborations()
        {
            return await _collaborationRepository.GetAllCollaborations();
        }

        public async Task<Collaboration?> GetCollaborationById(int id)
        {
            return await _collaborationRepository.GetCollaborationById(id);
        }

        public async Task<Collaboration?> CreateCollaboration(Collaboration collaboration)
        {
            return await _collaborationRepository.CreateCollaboration(collaboration);
        }

        public async Task<Collaboration> DeleteCollaboration(int id)
        {
            return await _collaborationRepository.DeleteCollaboration(id);
        }
    }
}

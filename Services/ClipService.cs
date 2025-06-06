using Sonare.Interfaces;
using Sonare.Models;

namespace Sonare.Services
{
    public class ClipService : IClipService
    {
        private readonly IClipRepository _clipRepository;

        public ClipService(IClipRepository clipRepository)
        {
            _clipRepository = clipRepository;
        }

        public async Task<List<Clip>> GetAllClips()
        {
            return await _clipRepository.GetAllClips();
        }

        public async Task<Clip?> GetClipById(int id)
        {
            return await _clipRepository.GetClipById(id);
        }

        public async Task<Clip?> CreateClip(Clip clip)
        {
            return await _clipRepository.CreateClip(clip);
        }

        public async Task<Clip?> UpdateClip(int id, Clip clip)
        {
            return await _clipRepository.UpdateClip(id, clip);
        }

        public async Task<Clip> DeleteClip(int id)
        {
            return await _clipRepository.DeleteClip(id);
        }
    }
}

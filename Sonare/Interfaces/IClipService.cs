using Sonare.Models;

namespace Sonare.Interfaces
{
    public interface IClipService
    {
        Task<List<Clip>> GetAllClips();
        Task<Clip?> GetClipById(int id);
        Task<Clip?> CreateClip(Clip clip);
        Task<Clip?> UpdateClip(int id, Clip clip);
        Task<Clip> DeleteClip(int id);
    }
}

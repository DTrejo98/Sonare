using Sonare.Models;

namespace Sonare.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> CreateComment(Comment comment);
        Task<Comment?> UpdateComment(int id, Comment comment);
        Task<Comment> DeleteComment(int id);
    }
}

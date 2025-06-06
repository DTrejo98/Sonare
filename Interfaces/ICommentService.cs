using Sonare.Models;

namespace Sonare.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> GetCommentById(int id);
        Task<Comment?> CreateComment(Comment comment);
        Task<Comment?> UpdateComment(int id, Comment comment);
        Task<Comment> DeleteComment(int id);
    }
}

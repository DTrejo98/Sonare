using Sonare.Interfaces;
using Sonare.Models;

namespace Sonare.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _commentRepository.GetAllComments();
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            return await _commentRepository.GetCommentById(id);
        }

        public async Task<Comment?> CreateComment(Comment comment)
        {
            return await _commentRepository.CreateComment(comment);
        }

        public async Task<Comment?> UpdateComment(int id, Comment comment)
        {
            return await _commentRepository.UpdateComment(id, comment);
        }

        public async Task<Comment> DeleteComment(int id)
        {
            return await _commentRepository.DeleteComment(id);
        }
    }
}

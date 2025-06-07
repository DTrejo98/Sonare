using Microsoft.EntityFrameworkCore;
using Sonare.Data;
using Sonare.Models;
using Sonare.Interfaces;

namespace Sonare.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> CreateComment(Comment comment)
        {
            var result = await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Comment?> UpdateComment(int id, Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null) return null;

            existingComment.Body = comment.Body;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) throw new Exception("Comment not found");

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}

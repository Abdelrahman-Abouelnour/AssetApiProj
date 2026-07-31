using AssetApi.Data;
using AssetApi.Interfaces;
using AssetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var ComModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (ComModel == null) return null;
            _context.Comments.Remove(ComModel);
            await _context.SaveChangesAsync();
            return ComModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var com = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (com == null) return null;
            return com;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment com)
        {
            var comment = await _context.Comments.FindAsync(id);
            if(comment == null) return null;
            comment.Title = com.Title;
            comment.Content = com.Content;
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}

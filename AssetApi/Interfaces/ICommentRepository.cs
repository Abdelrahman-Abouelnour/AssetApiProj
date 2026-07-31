using AssetApi.Dtos.Comment;
using AssetApi.Models;

namespace AssetApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, Comment com);
        Task<Comment?> DeleteAsync(int id);
    }
}

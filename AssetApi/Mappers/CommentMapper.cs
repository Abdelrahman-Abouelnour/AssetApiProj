using AssetApi.Dtos.Comment;
using AssetApi.Models;

namespace AssetApi.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto toCommentDto( this Comment commentModel) 
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }
        public static Comment toCommentFromCreate(this CreateCommentDto commentDto, int stockID)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockID
            };
        }
        public static Comment toCommentFromUpdate(this UpdateCommentDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }

    }
}

using AssetApi.Dtos.Comment;
using AssetApi.Interfaces;
using AssetApi.Mappers;
using AssetApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssetApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _Commentrepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository repository, IStockRepository stockRepo)
        {
            _Commentrepo = repository;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var comments = await _Commentrepo.GetAllAsync();
            var commentsDto = comments.Select(s => s.toCommentDto());
            return Ok(commentsDto);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var com = await _Commentrepo.GetByIdAsync(id);
            if(com == null) return NotFound();
            return Ok(com.toCommentDto());
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto )
        {
            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = commentDto.toCommentFromCreate(stockId);
            await _Commentrepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.toCommentDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateCommentDto comDto)
        {
            var comment = await _Commentrepo.UpdateAsync(id, comDto.toCommentFromUpdate());
            if (comment == null) return NotFound("Comment not found");
            return Ok(comment.toCommentDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _Commentrepo.DeleteAsync(id);
            if (stockModel == null) return NotFound();
            return NoContent();
        }
    }
}

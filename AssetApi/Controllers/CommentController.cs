using AssetApi.Dtos.Comment;
using AssetApi.Extensions;
using AssetApi.Interfaces;
using AssetApi.Mappers;
using AssetApi.Models;
using AssetApi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _Commentrepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository repository, IStockRepository stockRepo, UserManager<AppUser> userManager)
        {
            _Commentrepo = repository;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }
            var comments = await _Commentrepo.GetAllAsync();
            var commentsDto = comments.Select(s => s.toCommentDto());
            return Ok(commentsDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var com = await _Commentrepo.GetByIdAsync(id);
            if(com == null) return NotFound();
            return Ok(com.toCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.toCommentFromCreate(stockId);
            commentModel.AppUserId = appUser.Id;
            await _Commentrepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.toCommentDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateCommentDto comDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _Commentrepo.UpdateAsync(id, comDto.toCommentFromUpdate());
            if (comment == null) return NotFound("Comment not found");
            return Ok(comment.toCommentDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _Commentrepo.DeleteAsync(id);
            if (stockModel == null) return NotFound();
            return NoContent();
        }
    }
}

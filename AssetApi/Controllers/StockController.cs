using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetApi.Data;
using AssetApi.Dtos.Stock;
using AssetApi.Interfaces;
using AssetApi.Mappers;
using AssetApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace   AssetApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetallAsync();
            var stockDto = stocks.Select(s => s.toStockDto());
            return Ok(stockDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock =  await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.toStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto StockDto)
        {
            var stockModel = StockDto.toStockFromCreateDto();
            await _stockRepository.CreateAsync(stockModel);
            
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.toStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _stockRepository.UpdateAsync(id, updateDto);
            if (stockModel == null) return NotFound();
            
            return Ok(stockModel.toStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            var stockModel = await _stockRepository.DeleteAsync(id);
            if(stockModel == null) return NotFound();
            return NoContent();
        }
    }
}
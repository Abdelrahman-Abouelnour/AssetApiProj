using AssetApi.Extensions;
using AssetApi.Interfaces;
using AssetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetApi.Controllers
{
    [Route("api/Portoflio")]
    [ApiController]
    public class PortofolioController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortofolioController(UserManager<AppUser> userManager, IStockRepository repository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepo = repository;
            _portfolioRepo = portfolioRepository;
        }
        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetPortofolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return NotFound();
            }
            var portofolio = await _portfolioRepo.GetUserPortfolio(appUser);

            return Ok(portofolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null || appUser == default)
            {
                return NotFound();
            }
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Stock not found");
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if(userPortfolio.Any(p => p.Symbol.ToLower() == stock.Symbol.ToLower()))
            {
                return BadRequest("Stock already in portfolio");
            }
            var portfolio = new Portofolio
            {
                AppuserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioRepo.CreateAsync(portfolio);
            if(portfolio == null)
            {
                return StatusCode(500, "Error creating portfolio");
            }
            return Created();
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemovePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null) return BadRequest("Stock not found");
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower());
            if (filteredStock.Count() > 0)
            {
                await _portfolioRepo.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock is not in the portfolio");
            }
            
            
            return Ok();
        }
    }
}

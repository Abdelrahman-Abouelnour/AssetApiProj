using AssetApi.Data;
using AssetApi.Interfaces;
using AssetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetApi.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portofolio> CreateAsync(Portofolio portofolio)
        {
            await _context.Portofolios.AddAsync(portofolio);
            await _context.SaveChangesAsync();
            return portofolio;
        }

        public async Task<Portofolio> DeletePortfolio(AppUser user, string symbol)
        {
            var portModel = await _context.Portofolios.FirstOrDefaultAsync(p => p.AppuserId == user.Id && p.Stock.Symbol == symbol);
            if(portModel == null)
            {
                return null;
            }
            _context.Portofolios.Remove(portModel);
            await _context.SaveChangesAsync();
            return portModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portofolios
                .Where(p => p.AppuserId == user.Id)
                .Select(p => new Stock
                {
                    Id = p.StockId,
                    Symbol = p.Stock.Symbol,
                    CompanyName = p.Stock.CompanyName,
                    Purchase = p.Stock.Purchase,
                    LastDiv = p.Stock.LastDiv,
                    Industry = p.Stock.Industry,
                    MarketCap = p.Stock.MarketCap
                })
                .ToListAsync();
        }

    }
}

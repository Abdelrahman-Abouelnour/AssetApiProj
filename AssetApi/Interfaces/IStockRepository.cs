using AssetApi.Dtos.Stock;
using AssetApi.Helpers;
using AssetApi.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace AssetApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetallAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}

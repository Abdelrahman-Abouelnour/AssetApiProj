using AssetApi.Models;

namespace AssetApi.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portofolio> CreateAsync(Portofolio portofolio);
        Task<Portofolio> DeletePortfolio(AppUser user, string symbol);
    }
}

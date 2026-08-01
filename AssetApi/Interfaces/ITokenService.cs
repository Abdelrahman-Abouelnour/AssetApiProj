using AssetApi.Models;

namespace AssetApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

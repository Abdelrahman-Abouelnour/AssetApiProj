using Microsoft.AspNetCore.Identity;

    

namespace AssetApi.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portofolio> Portofolios { get; set; } = new List<Portofolio>();

    }
}

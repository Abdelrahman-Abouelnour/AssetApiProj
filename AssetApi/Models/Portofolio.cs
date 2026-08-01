using System.ComponentModel.DataAnnotations.Schema;

namespace AssetApi.Models
{
    [Table("Portofolios")]

    public class Portofolio
    {
        public string AppuserId { get; set; }
        public int StockId { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}

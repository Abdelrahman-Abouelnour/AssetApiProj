namespace AssetApi.Helpers
{
    public class QueryObject
    {
        public string? SortBy { get; set; } = null;
        public bool isDesc { get; set; } = false;
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
    }
}

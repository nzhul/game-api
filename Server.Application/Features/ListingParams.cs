namespace Server.Data
{
    public class ListingParams
    {
        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;

        public string Search { get; set; }

        public string OrderBy { get; set; }

        public OrderDirection OrderDirection { get; set; }
    }

    public enum OrderDirection
    {
        Ascending,
        Descending
    }
}

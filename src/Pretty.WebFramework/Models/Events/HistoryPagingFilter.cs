namespace Pretty.WebFramework.Models
{
    public class HistoryPagingFilter : IPagingable
    {
        public HistoryPagingFilter()
        {
            PageSize = int.MaxValue;
        }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}

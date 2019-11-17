namespace Pretty.WebFramework.Models
{
    public class Pagingable : IPagingable
    {
        public Pagingable()
        {
            PageSize = 10;
        }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}

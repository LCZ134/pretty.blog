namespace Pretty.WebFramework.Models
{
    public class DanmuPagingModel 
    {
        public DanmuPagingModel()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Fontesize { get; set; }

        public string Content { get; set; }

    }
}

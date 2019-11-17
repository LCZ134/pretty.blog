namespace Pretty.Core.Domain.Navigations
{
    public class Navigation: BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

    }
}

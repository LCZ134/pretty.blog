namespace Pretty.Core.Domain.Settings
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string Extends { get; set; }
    }
}

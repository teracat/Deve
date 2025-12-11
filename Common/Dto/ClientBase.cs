namespace Deve.Dto
{
    public class ClientBase : ModelId
    {
        public string Name { get; set; } = string.Empty;
        public string? TradeName { get; set; }
        public string? TaxName { get; set; }

        public string DisplayName => TradeName ?? Name;
    }
}

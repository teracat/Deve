namespace Deve.Dto
{
    public class Country : ModelId
    {
        public string Name { get; set; } = string.Empty;
        public string IsoCode { get; set; } = string.Empty;
    }
}

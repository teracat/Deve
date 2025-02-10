namespace Deve.Model
{
    public class City : ModelId
    {
        public string Name { get; set; } = string.Empty;

        public long StateId { get; set; } = 0;
        public string State { get; set; } = string.Empty;
        public long CountryId { get; set; } = 0;
        public string Country { get; set; } = string.Empty;
    }
}

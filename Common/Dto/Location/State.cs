namespace Deve.Dto
{
    public class State : ModelId
    {
        public string Name { get; set; } = string.Empty;

        public long CountryId { get; set; } = 0;
        public string Country { get; set; } = string.Empty;
    }
}

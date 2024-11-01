namespace Deve
{
    public class Location
    {
        public string? Address { get; set; }

        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}

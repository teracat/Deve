namespace Deve.Model
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

        public string FullAddress
        {
            get
            {
                string res = Address ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(res) && (!string.IsNullOrWhiteSpace(PostalCode) || !string.IsNullOrWhiteSpace(City)))
                {
                    res += ". ";
                }

                res += PostalCode;

                if (!string.IsNullOrWhiteSpace(PostalCode) && !string.IsNullOrWhiteSpace(City))
                {
                    res += " ";
                }

                res += City;

                if (!string.IsNullOrWhiteSpace(res) && !string.IsNullOrWhiteSpace(State))
                {
                    res += ", ";
                }

                res += State;

                if (!string.IsNullOrWhiteSpace(res) && !string.IsNullOrWhiteSpace(Country))
                {
                    res += $" ({Country})";
                }

                return res;
            }
        }
    }
}

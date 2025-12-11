namespace Deve.Dto
{
    public class CriteriaLocation : CriteriaId
    {
        public string? PostalCode { get; set; }

        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }

        public CriteriaLocation()
        {
        }

        public CriteriaLocation(CriteriaClientBasic other)
            : base(other)
        {
            PostalCode = other.PostalCode;
            City = other.City;
            State = other.State;
            Country = other.Country;
            CityId = other.CityId;
            StateId = other.StateId;
            CountryId = other.CountryId;
        }
    }
}

namespace Deve.Dto
{
    public class Criteria
    {
        public int? Limit { get; set; } = Constants.DefaultCriteriaLimit;
        public int? Offset { get; set; }
        public string? OrderBy { get; set; }

        public Criteria()
        {
        }

        public Criteria(Criteria other)
        {
            Limit = other.Limit;
            Offset = other.Offset;
            OrderBy = other.OrderBy;
        }
    }
}

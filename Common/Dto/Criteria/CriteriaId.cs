namespace Deve.Dto
{
    public class CriteriaId : Criteria
    {
        public long? Id { get; set; }

        public CriteriaId()
        {
        }

        public CriteriaId(long? id)
        {
            Id = id;
        }

        public CriteriaId(CriteriaId other)
            : base(other)
        {
            Id = other.Id;
        }
    }
}

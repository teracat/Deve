namespace Deve.Dto
{
    public class CriteriaIdName : CriteriaId
    {
        public string? Name { get; set; }

        public CriteriaIdName()
        {
        }

        public CriteriaIdName(long? id)
        {
            Id = id;
        }

        public CriteriaIdName(long? id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}

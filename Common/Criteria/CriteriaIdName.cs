namespace Deve
{
    public class CriteriaIdName : CriteriaId
    {
        public string? Name { get; set; }

        public CriteriaIdName()
        {
        }

        public CriteriaIdName(long? id, string? name = null)
        {
            Id = id;
            Name = name;
        }
    }
}

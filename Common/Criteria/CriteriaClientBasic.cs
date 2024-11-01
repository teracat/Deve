namespace Deve
{
    public class CriteriaClientBasic : CriteriaLocation
    {
        public string? Name { get; set; }

        public CriteriaClientBasic()
        {
        }

        public CriteriaClientBasic(CriteriaClientBasic other)
            : base(other)
        {
            Name = other.Name;
        }
    }
}

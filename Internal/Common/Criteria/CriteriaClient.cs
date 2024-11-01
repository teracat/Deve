namespace Deve.Internal
{
    public class CriteriaClient : CriteriaClientBasic
    {
        public ClientStatus? Status { get; set; }

        public CriteriaClient()
        {
        }

        public CriteriaClient(CriteriaClientBasic other)
            : base(other)
        {
        }
    }
}

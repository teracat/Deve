namespace Deve.Internal
{
    public class Client : External.Client
    {
        public ClientStatus Status { get; set; } = ClientStatus.Inactive;
        public decimal Balance { get; set; } = 0;
    }
}
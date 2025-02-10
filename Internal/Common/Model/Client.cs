namespace Deve.Internal.Model
{
    public class Client : External.Model.Client
    {
        public ClientStatus Status { get; set; } = ClientStatus.Inactive;
        public decimal Balance { get; set; } = 0;
    }
}
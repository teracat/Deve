namespace Deve.Internal.Dto
{
    public class Client : External.Dto.Client
    {
        public ClientStatus Status { get; set; } = ClientStatus.Inactive;
        public decimal Balance { get; set; } = 0;
    }
}
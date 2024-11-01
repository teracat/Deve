namespace Deve.External
{
    public class Client : ClientBase
    {
        public string? TaxId { get; set; }

        public Location Location { get; set; } = new Location();
    }
}

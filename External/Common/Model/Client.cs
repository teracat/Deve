using Deve.Model;

namespace Deve.External.Model
{
    public class Client : ClientBase
    {
        public string? TaxId { get; set; }

        public Location Location { get; set; } = new Location();
    }
}

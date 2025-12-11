using Deve.Dto;

namespace Deve.External.Dto
{
    public class Client : ClientBase
    {
        public string? TaxId { get; set; }

        public Location Location { get; set; } = new Location();
    }
}

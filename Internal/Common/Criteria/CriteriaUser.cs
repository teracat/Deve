using Deve.Criteria;

namespace Deve.Internal.Criteria
{
    public class CriteriaUser : CriteriaIdName
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public CriteriaActiveType OnlyActive { get; set; } = CriteriaActiveType.OnlyActive;
    }
}

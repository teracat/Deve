using Deve.Auth.Jwt;
using Deve.Auth;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManagerJwt Tests.
    /// </summary>
    public class TestTokenManagerJwt : TestTokenManagerBase
    {
        protected override ITokenManager CreateTokenManager()
        {
            return new TokenManagerJwt(ApiConstants.ApiAuthDefaultScheme);
        }

        protected override string GetExpiredToken()
        {
            return "eyJhbGciOiJBMjU2S1ciLCJlbmMiOiJBMjU2Q0JDLUhTNTEyIiwidHlwIjoiSldUIiwiY3R5IjoiSldUIn0.cGRi0QgUEw3WC4XPdQsju0mIbAk-8a0FU_B0FW7d8--hp-sq6l7aq86K17wAYQ64i1SGxsd-ui7uEfWqf3afwIHdEaDugrXx.fW18M8bI94RBYXdZsrLP4Q.HDBN_Y6tMCYHwv6zKeU-miQyyizHpDNCa_-NCHvqC5CVXlCPzIreU5-mDPu8OC4VPlVTXRkhoMfhlIM9maF4vUPuQ33c5VtOiC1vR1k7MH36Qx93hawF1kJKCHCAoTPuIj89YJ_T88jc_8BBW-4a5bhbeGUAAea7dbbaeB-e5q_Ib4jaPpnawh8qVNdTbUuu-RVZr94Abejneu719HtBLS0Ifx-tmXL7oMKdv5taKz5IsvHwiGiYBzKBzqNsVHlofsDEpI2SdqGdFK0Oa9RatoZy9P1rqs9cTNkwP33GMoI.hX7Ju-hwOdU1x7aNmN-xzDo5JDTET_pZ_F5uV5KzEHE";
        }
    }
}

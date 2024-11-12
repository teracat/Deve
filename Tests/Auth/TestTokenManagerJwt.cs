using Deve.Auth.Jwt;
using Deve.Auth;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManagerJwt Tests.
    /// </summary>
    public class TestTokenManagerJwt : TestTokenManagerBase
    {
        protected override ITokenManager CreateTokenManager() => new TokenManagerJwt();

        protected override string GetExpiredToken() => "eyJhbGciOiJBMjU2S1ciLCJlbmMiOiJBMjU2Q0JDLUhTNTEyIiwidHlwIjoiSldUIiwiY3R5IjoiSldUIn0.rUpEl5eE-zpdw_c7I1Qrt0yGA0qQdvHAl9nZ7HQupUMUYaH6ZTdgV_RfX5I7TeOSPEYQAd73UOxgheelTycJj-A0DHN1aU7S.jJLRWAqjjejvbX5pBf_N3w.h9VDs2Ai8v0LAR1NxDUvlu-Zb8swwFCdDJxi_ZEeMjH6dt-YJ2-aBSsXezCVGyi6bmuz-9p30JmiR5BR_q8TQ0EXPbAXJXX2SRWVV6fh9EHE9iZHlAesVAC7Sux2P8oo01J_ASELQA9P0uzLFDA2OJypmVogtq7FEOXr1yMTlRCNZua9f7R2OsWyAOKowhM9LfxWZLDnlBE3gxd2o_bHm622SNgIwWleH_3yu50T1Qxp0Rb93Ozkf9E0v_lKmgr-ighVfsxCoS16sX218oSBj8jl-ZOt2ki5gexqPxj-8uY.MoiQXEPOfFycZkKFgwgAVuVuWFr0lkBQsKqBxxl3CtI";
    }
}

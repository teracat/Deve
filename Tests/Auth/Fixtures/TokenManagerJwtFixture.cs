using Deve.Auth.TokenManagers;
using Deve.Auth.TokenManagers.Jwt;

namespace Deve.Tests.Auth.Fixtures;

public class TokenManagerJwtFixture : ITokenManagerFixture
{
    public ITokenManager TokenManager { get; private set; }
    public string TokenExpired => "eyJhbGciOiJBMjU2S1ciLCJlbmMiOiJBMjU2Q0JDLUhTNTEyIiwidHlwIjoiSldUIiwiY3R5IjoiSldUIn0.FXnhHRO0INpPQnXD5sAl2jPdAs_ad05QAOmlus6e-kqTgluMuZXWuD2tpucLvt_jWSy346hEicyHPd9BnuaF-VawB0Y7k-go.9wmOEJCWCnuWzT1eIGAkDA.UfiZxAQlMiWQ0cHlWWVG8lObbYxhk7SjpVSiXa92fHBFGIk7mfR37Y7b6bqLCkQoc0DEfQPAOaJd-Zv73_MmCtpOiPYLXp5IE8lTQktS-HbrkJidSzrEH2svBZovaoHbqPjvmb41RWf5EbMA8MHkad76jhla4YaJ0yRdD4csEh0UiiiFIUC95jIUMPfzxWG-xvWwBL0_XcvLkLJ9HqvA0yJ4JrkqvFtiaR_ICmUnvvn3DY3M22QiJwxYRcz81iULXspcZEWjH1cR99XMAMKGDdFLc_vBkg2K_J4YDjNmUko.7qnrYgudVryIWs2Q7sG4CKdyGdgwaWuvbOB1dMsJyhI";

    public TokenManagerJwtFixture()
    {
        TokenManager = new JwtTokenManager(TestsConstants.JwtSigningSecretKey, TestsConstants.JwtEncryptionSecretKey);
    }
}

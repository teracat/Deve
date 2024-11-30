using Deve.Auth;
using Deve.Auth.Jwt;

namespace Deve.Tests.Auth
{
    public class FixtureTokenManagerJwt : IFixtureTokenManager
    {
        public ITokenManager TokenManager { get; private set; }
        public string TokenExpired => "eyJhbGciOiJBMjU2S1ciLCJlbmMiOiJBMjU2Q0JDLUhTNTEyIiwidHlwIjoiSldUIiwiY3R5IjoiSldUIn0.NNvfJlOsjd7QAu1PT_pAf_3IgXbMFFuIyXFuxWEdbtmVZQ1Ps9T2sIMkHR7rkQjJndz-Z93s5RJkfxagIHOMjSCykGjK9ctk.0vO2AZ9lEPwIsv3NjpkPBQ.lUGt4nU0hm0XOhzU4_u1T8HNxg7BZ8LvLVhkJ3mrwaY4JQDh53xcEh-FfRRWYeuUZ-vMVFGwKs8qmsqHagM9wikf4p-EfD1HwwK05FmB7-YD_9fmo-5ShZHJyd9Pmpwu11kJ7wHKyBcdwEuYLa3E6A4rZRiZH--EupdIJanO2C8tBCb9AB5Et_XrzO8oBKPbJVnl37daqnXFllYcVehTLfSqSDqZF9-ebIkdLHhyM62W0FQeNI35ps-rt5y8-jHQ30GDPmrNHBlp1H3SFw1SD3UDVghDzyNpk00UPfw8GK8.5NOvQb3k6gUAjDPvqr0z27iR1dEAIIZJUzbMRSLIC9M";

        public FixtureTokenManagerJwt()
        {
            TokenManager = new TokenManagerJwt();
        }
    }
}

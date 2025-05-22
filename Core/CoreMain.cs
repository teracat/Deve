using Deve.Authenticate;
using Deve.Data;
using Deve.DataSource;
using Deve.Auth;
using Deve.Auth.UserIdentityService;
using Deve.Core.Shield;
using Deve.Internal.Data;
using Deve.Cache;

namespace Deve.Core
{
    /// <summary>
    /// Main Core class.
    /// </summary>
    public class CoreMain : ICore
    {
        #region Static Fields
        private static readonly IShield _shield = new ShieldMain();
        #endregion

        #region Fields
        private CoreAuth? _coreAuth;
        private CoreCountry? _coreCountry;
        private CoreState? _coreState;
        private CoreCity? _coreCity;
        private CoreClient? _coreClient;
        private CoreClientBasic? _coreClientBasic;
        private CoreUser? _coreUser;
        private CoreStats? _coreStats;
        #endregion

        #region Properties
        /// <summary>
        /// Source to get the data.
        /// </summary>
        public IDataSource DataSource { get; private set; }

        /// <summary>
        /// Access to the Auth.
        /// </summary>
        public IAuth Auth { get; private set; }

        /// <summary>
        /// Access to the Shield.
        /// </summary>
        public IShield Shield => _shield;

        /// <summary>
        /// Global options.
        /// </summary>
        public IDataOptions Options { get; set; }

        /// <summary>
        /// Service to get the user identity.
        /// </summary>
        public IUserIdentityService UserIdentityService { get; }

        /// <summary>
        /// Cache to store data.
        /// </summary>
        public ICache Cache { get; }

        public IAuthenticate Authenticate => _coreAuth ??= new CoreAuth(DataSource, Auth, Options, UserIdentityService, this);
        public IDataCountry Countries => _coreCountry ??= new CoreCountry(DataSource, Auth, Options, UserIdentityService);
        public IDataState States => _coreState ??= new CoreState(DataSource, Auth, Options, UserIdentityService, Countries);
        public IDataCity Cities => _coreCity ??= new CoreCity(DataSource, Auth, Options, UserIdentityService, States, Countries);
        public IDataClient Clients => _coreClient ??= new CoreClient(DataSource, Auth, Options, UserIdentityService, Cities, States, Countries, Cache);
        public IDataUser Users => _coreUser ??= new CoreUser(DataSource, Auth, Options, UserIdentityService);
        public IDataStats Stats => _coreStats ??= new CoreStats(DataSource, Auth, Options, UserIdentityService, Cache);

        public External.Data.IDataClientBasic ClientsBasic => _coreClientBasic ??= new CoreClientBasic(DataSource, Auth, Options, UserIdentityService, Cache);
        #endregion

        #region Constructor
        public CoreMain(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService, ICache cache)
        {
            DataSource = dataSource;
            Auth = auth;
            Options = options;
            UserIdentityService = userIdentityService;
            Cache = cache;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            // Nothing to dispose
        }
        #endregion
    }
}

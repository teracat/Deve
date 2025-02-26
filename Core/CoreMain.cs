using Deve.Model;
using Deve.Authenticate;
using Deve.Criteria;
using Deve.Data;
using Deve.DataSource;
using Deve.Auth;
using Deve.Core.Shield;
using Deve.Internal.Model;
using Deve.Internal.Data;
using Deve.Internal.Criteria;

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
        private UserIdentity? _userIdentity;
        private User? _user;

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
        /// Information to identify the user.
        /// </summary>
        public UserIdentity? UserIdentity
        {
            get => _userIdentity;
            set
            {
                if (_userIdentity != value)
                {
                    //If the user Id has changed, we set the _user to null to force its data to be fetched again (if accessed)
                    if (_userIdentity is not null && value is not null && _userIdentity.Id != value.Id)
                    {
                        _user = null;
                    }

                    _userIdentity = value;
                }
            }
        }

        /// <summary>
        /// If needed, get the full User information.
        /// It will only get the User data from the DataSource if it's accessed.
        /// It can also be set and the UserIdentity will be updated.
        /// </summary>
        public User? User
        {
            get
            {
                if (_user is not null)
                {
                    return _user;
                }

                if (_userIdentity is null)
                {
                    return null;
                }

                var resUser = DataSource.Users.Get(_userIdentity.Id).Result;
                if (!resUser.Success)
                {
                    return null;
                }

                _user = resUser.Data;
                return _user;
            }
            set
            {
                _user = value;
                if (value is null)
                {
                    _userIdentity = null;
                }
                else
                {
                    _userIdentity = new UserIdentity(value);
                }
            }
        }

        public IAuthenticate Authenticate => _coreAuth ??= new CoreAuth(this);
        public IDataAll<Country, Country, CriteriaCountry> Countries => _coreCountry ??= new CoreCountry(this);
        public IDataAll<State, State, CriteriaState> States => _coreState ??= new CoreState(this);
        public IDataAll<City, City, CriteriaCity> Cities => _coreCity ??= new CoreCity(this);
        public IDataClient Clients => _coreClient ??= new CoreClient(this);
        public IDataAll<UserBase, UserPlainPassword, CriteriaUser> Users => _coreUser ??= new CoreUser(this);
        public IDataStats Stats => _coreStats ??= new CoreStats(this);

        public External.Data.IDataGet<ClientBasic, External.Model.Client, CriteriaClientBasic> ClientsBasic => _coreClientBasic ??= new CoreClientBasic(this);
        #endregion

        #region Constructor
        public CoreMain(IDataSource dataSource, IAuth auth, IDataOptions options)
        {
            DataSource = dataSource;
            Auth = auth;
            Options = options;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
        }
        #endregion
    }
}